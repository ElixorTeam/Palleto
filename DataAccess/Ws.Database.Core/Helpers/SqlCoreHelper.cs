using System.Reflection;
using Microsoft.Extensions.Configuration;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Transform;
using Ws.Database.Core.Listeners;
using Ws.Domain.Models.Common;

namespace Ws.Database.Core.Helpers;

public sealed class SqlCoreHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618// Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlCoreHelper _instance;
#pragma warning restore CS8618// Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlCoreHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private static SqlSettingsModels SqlSettingsModels { get; set; } = new();
    private ISessionFactory SessionFactory { get; set; } = null!;
    private Configuration SqlConfiguration { get; set; } = new();

    public void SetSessionFactory()
    {
        SetSqlConfiguration();
        AddConfigurationMappings();
        SessionFactory = SqlConfiguration.BuildSessionFactory();
    }

    private static SqlSettingsModels LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .AddJsonFile("sqlconfig.json", optional: false, reloadOnChange: false)
            .Build();

        SqlSettingsModels sqlSettingsModels = new();
        sqlConfiguration.GetSection("SqlSettings").Bind(sqlSettingsModels);
        return sqlSettingsModels;
    }

    private void SetSqlConfiguration()
    {
        SqlSettingsModels = LoadJsonConfig();
        SqlConfiguration = new();
        SqlConfiguration.DataBaseIntegration(db => {
            db.ConnectionString = SqlSettingsModels.GetConnectionString();
            db.Dialect<MsSql2012Dialect>();
            db.Driver<SqlClientDriver>();
            db.LogSqlInConsole = SqlSettingsModels.IsShowSql;
        });
        SqlConfiguration.EventListeners.PreInsertEventListeners =
            [new SqlCreateDtListener()];
        SqlConfiguration.EventListeners.PreUpdateEventListeners =
            [new SqlChangeDtListener()];
    }

    private void AddConfigurationMappings()
    {
        ModelMapper mapper = new();
        mapper.AddMappings(Assembly.GetExecutingAssembly().GetTypes());
        HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        SqlConfiguration.AddMapping(mapping);
    }
    
    #endregion
    
    #region Public and private methods - Base

    private void ExecuteSelectCore(Action<ISession> action)
    {
        using ISession session = SessionFactory.OpenSession();
        session.FlushMode = FlushMode.Manual;
        
        try
        {
            action(session);
        }
        catch (Exception)
        {
            return;
        }
    }

    private void ExecuteTransactionCore(Action<ISession> action)
    {
        using ISession session = SessionFactory.OpenSession();
        session.FlushMode = FlushMode.Commit;
        
        using ITransaction transaction = session.BeginTransaction();
        try
        {
            action(session);
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }
    }

    #endregion

    #region GetItem

    public T GetItemById<T>(object id) where T : EntityBase, new()
    {
        T? item = null;
        ExecuteSelectCore(session => {
            item = session.Get<T>(id);
        });
        return item ?? new();
    }
    
    public T GetItem<T>(QueryOver<T> query) where T : EntityBase, new()
    {
        T? item = null;
        ExecuteSelectCore(session => {
            ICriteria criteria = query.DetachedCriteria.GetExecutableCriteria(session);
            item = criteria.UniqueResult<T>();
        });
        return item ?? new();
    }

    #endregion

    #region GetList
    
    public IEnumerable<T> GetEnumerable<T>(QueryOver<T>? query = null) where T : EntityBase, new()
    {
        IEnumerable<T> items = Enumerable.Empty<T>();
        
        ExecuteSelectCore(session => {
            ICriteria criteria = query != null ? 
                query.DetachedCriteria.GetExecutableCriteria(session) : 
                session.CreateCriteria<T>();
            items = criteria.List<T>();
        });
        
        return items;
    }

    public IEnumerable<TObject> GetEnumerableBySql<TObject>(string sqlQuery)
    {
        IEnumerable<TObject> items = Enumerable.Empty<TObject>();
        
        ExecuteSelectCore(session => {
            ISQLQuery query = session.CreateSQLQuery(sqlQuery);
            query.SetResultTransformer(Transformers.AliasToBean<TObject>()); 
            items = query.Enumerable<TObject>();
        });
        
        return items;
    }
    
    #endregion
    
    #region CRUD

    public T SaveOrUpdate<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.SaveOrUpdate(item));
        return item;
    }

    public T Save<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.Save(item));
        return item;
    }

    public T Update<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.Update(item));
        return item;
    }

    public void Delete<T>(T item) where T : EntityBase
    {
        ExecuteTransactionCore(session => session.Delete(item));
    }

    #endregion
}