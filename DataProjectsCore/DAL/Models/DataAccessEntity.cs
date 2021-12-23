﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using DataShareCore.DAL.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProjectsCore.DAL.Models
{
    public class DataAccessEntity
    {
        #region Public and private fields and properties

        public CoreSettingsEntity CoreSettings { get; set; }
        private readonly object _locker = new();

        // https://github.com/nhibernate/fluent-nhibernate/wiki/Database-configuration
        private ISessionFactory? _sessionFactory;
        private ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory != null)
                    return _sessionFactory;
                lock (_locker)
                {
                    if (CoreSettings == null)
                        throw new ArgumentException("CoreSettings is null!");
                    if (!CoreSettings.Trusted && (string.IsNullOrEmpty(CoreSettings.Username) || string.IsNullOrEmpty(CoreSettings.Password)))
                        throw new ArgumentException("CoreSettings.Username or CoreSettings.Password is null!");
                    MsSqlConfiguration config = MsSqlConfiguration.MsSql2012.ConnectionString(GetConnectionString());
                    //config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>().DefaultSchema(CoreSettings.Schema).ShowSql();
                    //config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>().DefaultSchema(CoreSettings.Schema);
                    config.Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>();
                    FluentConfiguration configuration = Fluently.Configure().Database(config);
                    AddConfigurationMappings(configuration, CoreSettings);
                    //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaUpdate(cfg).Execute(false, true));
                    //configuration.ExposeConfiguration(cfg => new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Create(false, true));
                    configuration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
                    _sessionFactory = configuration.BuildSessionFactory();
                    return _sessionFactory;
                }
            }
        }

        // SYSTEM Tables CRUD.
        public CrudController? AccessesCrud = null;
        public CrudController? AppsCrud = null;
        public CrudController? LogsCrud = null;
        public CrudController? LogTypesCrud = null;
        public TableSystemModels.HostCrud? HostsCrud = null;

        // SCALES Tables CRUD.
        public CrudController? Crud = null;
        public CrudController? BarcodeTypesCrud = null;
        public CrudController? ContragentsCrud = null;
        public CrudController? ErrorsCrud = null;
        public CrudController? LabelsCrud = null;
        public CrudController? NomenclaturesCrud = null;
        public CrudController? OrdersCrud = null;
        public CrudController? OrderStatusesCrud = null;
        public CrudController? OrderTypesCrud = null;
        public CrudController? PlusCrud = null;
        public CrudController? PrintersCrud = null;
        public CrudController? PrinterResourcesCrud = null;
        public CrudController? PrinterTypesCrud = null;
        public CrudController? ProductionFacilitiesCrud = null;
        public CrudController? ProductSeriesCrud = null;
        public CrudController? ScalesCrud = null;
        public CrudController? TaskCrud = null;
        public CrudController? TaskTypeCrud = null;
        public CrudController? TemplatesCrud = null;
        public CrudController? WeithingFactsCrud = null;
        public CrudController? WorkshopsCrud = null;
        public TableScaleModels.TemplateResourceCrud? TemplateResourcesCrud = null;
        
        // Scales datas CRUD.
        public CrudController? DeviceCrud = null;
        public CrudController? WeithingFactSummaryCrud = null;
        public CrudController? LogSummaryCrud = null;

        // DWH tables CRUD.
        public CrudController? BrandCrud = null;
        public CrudController? InformationSystemCrud = null;
        public CrudController? NomenclatureCrud = null;
        public CrudController? NomenclatureGroupCrud = null;
        public CrudController? NomenclatureLightCrud = null;
        public CrudController? NomenclatureTypeCrud = null;
        public CrudController? StatusCrud = null;

        public bool IsDisabled => !GetSession().IsConnected;
        public bool IsOpen => GetSession().IsOpen;
        public bool IsConnected => GetSession().IsConnected;
        public bool IsDirty => GetSession().IsDirty();

        #endregion

        #region Constructor and destructor

        public DataAccessEntity(CoreSettingsEntity appSettings)
        {
            CoreSettings = appSettings;

            if (string.Equals(CoreSettings.Db, "ScalesDB", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(CoreSettings.Db, "SCALES", StringComparison.InvariantCultureIgnoreCase))
            {
                Crud = new CrudController(this, SessionFactory);

                // SYSTEM tables CRUD.
                AccessesCrud = new CrudController(this, SessionFactory);
                AppsCrud = new CrudController(this, SessionFactory);
                HostsCrud = new TableSystemModels.HostCrud(this, SessionFactory);
                LogsCrud = new CrudController(this, SessionFactory);
                LogTypesCrud = new CrudController(this, SessionFactory);
                
                // SCALES tables CRUD.
                BarcodeTypesCrud = new CrudController(this, SessionFactory);
                ContragentsCrud = new CrudController(this, SessionFactory);
                ErrorsCrud = new CrudController(this, SessionFactory);
                LabelsCrud = new CrudController(this, SessionFactory);
                NomenclaturesCrud = new CrudController(this, SessionFactory);
                OrdersCrud = new CrudController(this, SessionFactory);
                OrderStatusesCrud = new CrudController(this, SessionFactory);
                OrderTypesCrud = new CrudController(this, SessionFactory);
                PlusCrud = new CrudController(this, SessionFactory);
                PrinterResourcesCrud = new CrudController(this, SessionFactory);
                PrintersCrud = new CrudController(this, SessionFactory);
                PrinterTypesCrud = new CrudController(this, SessionFactory);
                ProductionFacilitiesCrud = new CrudController(this, SessionFactory);
                ProductSeriesCrud = new CrudController(this, SessionFactory);
                ScalesCrud = new CrudController(this, SessionFactory);
                TaskCrud = new CrudController(this, SessionFactory);
                TaskTypeCrud = new CrudController(this, SessionFactory);
                TemplateResourcesCrud = new TableScaleModels.TemplateResourceCrud(this, SessionFactory);
                TemplatesCrud = new CrudController(this, SessionFactory);
                WeithingFactsCrud = new CrudController(this, SessionFactory);
                WorkshopsCrud = new CrudController(this, SessionFactory);

                // Datas CRUD.
                DeviceCrud = new CrudController(this, SessionFactory);
                LogSummaryCrud = new CrudController(this, SessionFactory);
                WeithingFactSummaryCrud = new CrudController(this, SessionFactory);
            }
            else if (string.Equals(CoreSettings.Db, "VSDWH", StringComparison.InvariantCultureIgnoreCase))
            {
                // DWH tables CRUD.
                BrandCrud = new CrudController(this, SessionFactory);
                InformationSystemCrud = new CrudController(this, SessionFactory);
                NomenclatureCrud = new CrudController(this, SessionFactory);
                NomenclatureGroupCrud = new CrudController(this, SessionFactory);
                NomenclatureLightCrud = new CrudController(this, SessionFactory);
                NomenclatureTypeCrud = new CrudController(this, SessionFactory);
                StatusCrud = new CrudController(this, SessionFactory);
            }
        }

        // This code have exception: 
        // SqlException: A connection was successfully established with the server, but then an error occurred during the login process. 
        // (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)
        private MsSqlConfiguration GetConnection() => CoreSettings.Trusted
            ? MsSqlConfiguration.MsSql2012.ConnectionString(c => c
                .Server(CoreSettings.Server).Database(CoreSettings.Db).TrustedConnection())
            : MsSqlConfiguration.MsSql2012.ConnectionString(c => c
                .Server(CoreSettings.Server).Database(CoreSettings.Db).Username(CoreSettings.Username).Password(CoreSettings.Password));

        private string GetConnectionString() => CoreSettings.Trusted
            ? $"Data Source={CoreSettings.Server};Initial Catalog={CoreSettings.Db};Persist Security Info=True;Trusted Connection=True;TrustServerCertificate=True;"
            : $"Data Source={CoreSettings.Server};Initial Catalog={CoreSettings.Db};Persist Security Info=True;User ID={CoreSettings.Username};Password={CoreSettings.Password};TrustServerCertificate=True;";

        private void AddConfigurationMappings(FluentConfiguration configuration, CoreSettingsEntity coreSettings)
        {
            if (configuration == null || coreSettings == null || string.IsNullOrEmpty(coreSettings.Db))
                return;

            if (string.Equals(coreSettings.Db, "ScalesDB", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(coreSettings.Db, "SCALES", StringComparison.InvariantCultureIgnoreCase))
            {
                AddConfigurationMappingsForScale(configuration);
            }
            else if (string.Equals(coreSettings.Db, "VSDWH", StringComparison.InvariantCultureIgnoreCase))
            {
                AddConfigurationMappingsForDwh(configuration);
            }
        }

        private void AddConfigurationMappingsForScale(FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.AccessMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.AppMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.HostMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.LogMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableSystemModels.LogTypeMap>());

            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.BarcodeTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ContragentMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ErrorMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.LabelMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.NomenclatureMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrderMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrderTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.OrganizationMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PluMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterResourceMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.PrinterTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ProductionFacilityMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ProductSeriesMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.ScaleMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TaskMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TaskTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TemplateMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.TemplateResourceMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.WeithingFactMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableScaleModels.WorkshopMap>());
        }

        private void AddConfigurationMappingsForDwh(FluentConfiguration configuration)
        {
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.BrandMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.InformationSystemMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureGroupMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureLightMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.NomenclatureTypeMap>());
            configuration.Mappings(m => m.FluentMappings.Add<TableDwhModels.StatusMap>());
        }

        #endregion

        #region Public and private methods - Share

        public ISession GetSession() => SessionFactory.OpenSession();

        #endregion

        #region Public and private methods - CRUD share

        public T ActionGetIdEntity<T>(BaseIdEntity entity, ShareEnums.DbTableAction tableAction) where T : BaseIdEntity, new()
        {
            T result = tableAction switch
            {
                ShareEnums.DbTableAction.New => new T(),
                ShareEnums.DbTableAction.Edit => (T)entity,
                ShareEnums.DbTableAction.Copy => (T)((T)entity).Clone(),
                ShareEnums.DbTableAction.Delete => (T)entity,
                ShareEnums.DbTableAction.Mark => (T)entity,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };

            if (tableAction == ShareEnums.DbTableAction.New || tableAction == ShareEnums.DbTableAction.Copy)
            {
                int nextId = 0;
                nextId = ActionGetIdEntityForScales<T>(nextId);
                if (nextId == 0)
                    nextId = ActionGetIdEntityForDwh<T>(nextId);
                result.Id = nextId + 1;
            }
            return result;
        }

        private int ActionGetIdEntityForScales<T>(int nextId) where T : BaseIdEntity, new()
        {
            if (typeof(T) == typeof(TableSystemModels.HostEntity))
            {
                if (HostsCrud != null)
                    nextId = HostsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.BarcodeTypeEntity))
            {
                if (BarcodeTypesCrud != null)
                    nextId = BarcodeTypesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.ContragentEntity))
            {
                if (ContragentsCrud != null)
                    nextId = ContragentsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.LabelEntity))
            {
                if (LabelsCrud != null)
                    nextId = LabelsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.NomenclatureEntity))
            {
                if (NomenclaturesCrud != null)
                    nextId = NomenclaturesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderEntity))
            {
                if (OrdersCrud != null)
                    nextId = OrdersCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderStatusEntity))
            {
                if (OrderStatusesCrud != null)
                    nextId = OrderStatusesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.OrderTypeEntity))
            {
                if (OrderTypesCrud != null)
                    nextId = OrderTypesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.PluEntity))
            {
                if (PlusCrud != null)
                    nextId = PlusCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductionFacilityEntity))
            {
                if (ProductionFacilitiesCrud != null)
                    nextId = ProductionFacilitiesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.ProductSeriesEntity))
            {
                if (ProductSeriesCrud != null)
                    nextId = ProductSeriesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.ScaleEntity))
            {
                if (ScalesCrud != null)
                    nextId = ScalesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateResourceEntity))
            {
                if (TemplateResourcesCrud != null)
                    nextId = TemplateResourcesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.TemplateEntity))
            {
                if (TemplatesCrud != null)
                    nextId = TemplatesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.WeithingFactEntity))
            {
                if (WeithingFactsCrud != null)
                    nextId = WeithingFactsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.WorkshopEntity))
            {
                if (WorkshopsCrud != null)
                    nextId = WorkshopsCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterEntity))
            {
                if (PrintersCrud != null)
                    nextId = PrintersCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterResourceEntity))
            {
                if (PrinterResourcesCrud != null)
                    nextId = PrinterResourcesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableScaleModels.PrinterTypeEntity))
            {
                if (PrinterTypesCrud != null)
                    nextId = PrinterTypesCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            return nextId;
        }

        private int ActionGetIdEntityForDwh<T>(int nextId) where T : BaseIdEntity, new()
        {
            if (typeof(T) == typeof(TableDwhModels.BrandEntity))
            {
                if (BrandCrud != null)
                    nextId = BrandCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.InformationSystemEntity))
            {
                if (InformationSystemCrud != null)
                    nextId = InformationSystemCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureEntity))
            {
                if (NomenclatureCrud != null)
                    nextId = NomenclatureCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureGroupEntity))
            {
                if (NomenclatureGroupCrud != null)
                    nextId = NomenclatureGroupCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureLightEntity))
            {
                if (NomenclatureLightCrud != null)
                    nextId = NomenclatureLightCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.NomenclatureTypeEntity))
            {
                if (NomenclatureTypeCrud != null)
                    nextId = NomenclatureTypeCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }
            else if (typeof(T) == typeof(TableDwhModels.StatusEntity))
            {
                if (StatusCrud != null)
                    nextId = StatusCrud.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc)).Id;
            }

            return nextId;
        }

        public T ActionGetUidEntity<T>(BaseUidEntity entity, ShareEnums.DbTableAction tableAction) where T : BaseUidEntity, new()
        {
            T? result = tableAction switch
            {
                ShareEnums.DbTableAction.New => new T(),
                ShareEnums.DbTableAction.Edit => (T)entity,
                ShareEnums.DbTableAction.Copy => (T)((T)entity).Clone(),
                ShareEnums.DbTableAction.Delete => (T)entity,
                ShareEnums.DbTableAction.Mark => (T)entity,
                _ => throw new ArgumentOutOfRangeException(nameof(tableAction), tableAction, null)
            };
            if (tableAction == ShareEnums.DbTableAction.New || tableAction == ShareEnums.DbTableAction.Copy)
            {
                if (typeof(T) == typeof(TableSystemModels.AccessEntity))
                {
                    _ = AccessesCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(TableSystemModels.AppEntity))
                {
                    _ = AppsCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(TableSystemModels.LogEntity))
                {
                    _ = LogsCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(TableSystemModels.LogTypeEntity))
                {
                    _ = LogTypesCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(DataShareCore.DAL.DataModels.LogSummaryEntity))
                {
                    _ = LogSummaryCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                else if (typeof(T) == typeof(DataModels.WeithingFactSummaryEntity))
                {
                    //_ = WeithingFactSummaryCrud?.GetEntity(null, new FieldOrderEntity(ShareEnums.DbField.Uid, ShareEnums.DbOrderDirection.Desc)).Uid;
                }
                result.Uid = Guid.NewGuid();
            }
            return result;
        }

        public void ActionDeleteEntity<T>(T entity) where T : IBaseEntity
        {
            Crud.DeleteEntity(accessEntity);
            // SYSTEM.
            if (entity is TableSystemModels.AccessEntity accessEntity)
                AccessesCrud?.DeleteEntity(accessEntity);
            else if (entity is TableSystemModels.AppEntity appEntity)
                AppsCrud?.DeleteEntity(appEntity);
            else if (entity is TableSystemModels.HostEntity hostsEntity)
                HostsCrud?.DeleteEntity(hostsEntity);
            else if (entity is TableSystemModels.LogEntity logEntity)
                LogsCrud?.DeleteEntity(logEntity);
            else if (entity is TableSystemModels.LogTypeEntity logTypeEntity)
                LogTypesCrud?.DeleteEntity(logTypeEntity);
            // SCALES.
            else if (entity is TableScaleModels.LabelEntity labelsEntity)
                LabelsCrud?.DeleteEntity(labelsEntity);
            else if (entity is TableScaleModels.BarcodeTypeEntity barCodeTypesEntity)
                BarcodeTypesCrud?.DeleteEntity(barCodeTypesEntity);
            else if (entity is TableScaleModels.ContragentEntity contragentsEntity)
                ContragentsCrud?.DeleteEntity(contragentsEntity);
            else if (entity is DataShareCore.DAL.DataModels.LogSummaryEntity logSummaryEntity)
                LogSummaryCrud?.DeleteEntity(logSummaryEntity);
            else if (entity is TableScaleModels.NomenclatureEntity nomenclatureEntity)
                NomenclaturesCrud?.DeleteEntity(nomenclatureEntity);
            else if (entity is TableScaleModels.OrderEntity ordersEntity)
                OrdersCrud?.DeleteEntity(ordersEntity);
            else if (entity is TableScaleModels.OrderStatusEntity orderStatusEntity)
                OrderStatusesCrud?.DeleteEntity(orderStatusEntity);
            else if (entity is TableScaleModels.OrderTypeEntity orderTypesEntity)
                OrderTypesCrud?.DeleteEntity(orderTypesEntity);
            else if (entity is TableScaleModels.PluEntity pluEntity)
                PlusCrud?.DeleteEntity(pluEntity);
            else if (entity is TableScaleModels.ProductionFacilityEntity productionFacilityEntity)
                ProductionFacilitiesCrud?.DeleteEntity(productionFacilityEntity);
            else if (entity is TableScaleModels.ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud?.DeleteEntity(productSeriesEntity);
            else if (entity is TableScaleModels.ScaleEntity scalesEntity)
                ScalesCrud?.DeleteEntity(scalesEntity);
            else if (entity is TableScaleModels.TaskEntity taskEntity)
                TaskCrud?.DeleteEntity(taskEntity);
            else if (entity is TableScaleModels.TaskTypeEntity taskTypeEntity)
                TaskTypeCrud?.DeleteEntity(taskTypeEntity);
            else if (entity is TableScaleModels.TemplateEntity templatesEntity)
                TemplatesCrud?.DeleteEntity(templatesEntity);
            else if (entity is TableScaleModels.TemplateResourceEntity templateResourcesEntity)
                TemplateResourcesCrud?.DeleteEntity(templateResourcesEntity);
            else if (entity is TableScaleModels.WeithingFactEntity weithingFactEntity)
                WeithingFactsCrud?.DeleteEntity(weithingFactEntity);
            else if (entity is DataModels.WeithingFactSummaryEntity weithingFactSummaryEntity)
                WeithingFactSummaryCrud?.DeleteEntity(weithingFactSummaryEntity);
            else if (entity is TableScaleModels.WorkshopEntity workshopEntity)
                WorkshopsCrud?.DeleteEntity(workshopEntity);
            else if (entity is TableScaleModels.PrinterEntity zebraPrinterEntity)
                PrintersCrud?.DeleteEntity(zebraPrinterEntity);
            else if (entity is TableScaleModels.PrinterTypeEntity zebraPrinterTypeEntity)
                PrinterTypesCrud?.MarkedEntity(zebraPrinterTypeEntity);
            else if (entity is TableScaleModels.PrinterResourceEntity zebraPrinterResourceRefEntity)
                PrinterResourcesCrud?.DeleteEntity(zebraPrinterResourceRefEntity);
            // DWH.
            else if (entity is TableDwhModels.BrandEntity brandEntity)
                BrandCrud?.DeleteEntity(brandEntity);
            else if (entity is TableDwhModels.InformationSystemEntity informationSystemEntity)
                InformationSystemCrud?.DeleteEntity(informationSystemEntity);
            else if (entity is TableDwhModels.NomenclatureEntity dwhNomenclatureEntity)
                NomenclatureCrud?.DeleteEntity(dwhNomenclatureEntity);
            else if (entity is TableDwhModels.NomenclatureGroupEntity nomenclatureGroupEntity)
                NomenclatureGroupCrud?.DeleteEntity(nomenclatureGroupEntity);
            else if (entity is TableDwhModels.NomenclatureLightEntity nomenclatureLightEntity)
                NomenclatureLightCrud?.DeleteEntity(nomenclatureLightEntity);
            else if (entity is TableDwhModels.NomenclatureTypeEntity nomenclatureTypeEntity)
                NomenclatureTypeCrud?.DeleteEntity(nomenclatureTypeEntity);
            else if (entity is TableDwhModels.StatusEntity statusEntity)
                StatusCrud?.DeleteEntity(statusEntity);
        }

        public void ActionMarkedEntity<T>(T entity) where T : IBaseEntity
        {
            // SYSTEM.
            if (entity is TableSystemModels.AccessEntity accessEntity)
                AccessesCrud?.MarkedEntity(accessEntity);
            else if (entity is TableSystemModels.AppEntity appEntity)
                AppsCrud?.MarkedEntity(appEntity);
            else if (entity is TableSystemModels.HostEntity hostsEntity)
                HostsCrud?.MarkedEntity(hostsEntity);
            else if (entity is TableSystemModels.LogEntity logEntity)
                LogsCrud?.MarkedEntity(logEntity);
            else if (entity is TableSystemModels.LogTypeEntity logTypeEntity)
                LogTypesCrud?.MarkedEntity(logTypeEntity);
            // SCALES.
            else if (entity is TableScaleModels.BarcodeTypeEntity barCodeTypesEntity)
                BarcodeTypesCrud?.MarkedEntity(barCodeTypesEntity);
            else if (entity is TableScaleModels.ContragentEntity contragentsEntity)
                ContragentsCrud?.MarkedEntity(contragentsEntity);
            else if (entity is TableScaleModels.LabelEntity labelsEntity)
                LabelsCrud?.MarkedEntity(labelsEntity);
            else if (entity is DataShareCore.DAL.DataModels.LogSummaryEntity logSummaryEntity)
                LogSummaryCrud?.MarkedEntity(logSummaryEntity);
            else if (entity is TableScaleModels.NomenclatureEntity nomenclatureEntity)
                NomenclaturesCrud?.MarkedEntity(nomenclatureEntity);
            else if (entity is TableScaleModels.OrderEntity ordersEntity)
                OrdersCrud?.MarkedEntity(ordersEntity);
            else if (entity is TableScaleModels.OrderStatusEntity orderStatusEntity)
                OrderStatusesCrud?.MarkedEntity(orderStatusEntity);
            else if (entity is TableScaleModels.OrderTypeEntity orderTypesEntity)
                OrderTypesCrud?.MarkedEntity(orderTypesEntity);
            else if (entity is TableScaleModels.PluEntity pluEntity)
                PlusCrud?.MarkedEntity(pluEntity);
            else if (entity is TableScaleModels.ProductionFacilityEntity productionFacilityEntity)
                ProductionFacilitiesCrud?.MarkedEntity(productionFacilityEntity);
            else if (entity is TableScaleModels.ProductSeriesEntity productSeriesEntity)
                ProductSeriesCrud?.MarkedEntity(productSeriesEntity);
            else if (entity is TableScaleModels.ScaleEntity scalesEntity)
                ScalesCrud?.MarkedEntity(scalesEntity);
            else if (entity is TableScaleModels.TaskEntity taskEntity)
                TaskCrud?.MarkedEntity(taskEntity);
            else if (entity is TableScaleModels.TaskTypeEntity taskTypeEntity)
                TaskTypeCrud?.MarkedEntity(taskTypeEntity);
            else if (entity is TableScaleModels.TemplateEntity templatesEntity)
                TemplatesCrud?.MarkedEntity(templatesEntity);
            else if (entity is TableScaleModels.TemplateResourceEntity templateResourcesEntity)
                TemplateResourcesCrud?.MarkedEntity(templateResourcesEntity);
            else if (entity is TableScaleModels.WeithingFactEntity weithingFactEntity)
                WeithingFactsCrud?.MarkedEntity(weithingFactEntity);
            else if (entity is DataModels.WeithingFactSummaryEntity weithingFactSummaryEntity)
                WeithingFactSummaryCrud?.MarkedEntity(weithingFactSummaryEntity);
            else if (entity is TableScaleModels.WorkshopEntity workshopEntity)
                WorkshopsCrud?.MarkedEntity(workshopEntity);
            else if (entity is TableScaleModels.PrinterEntity zebraPrinterEntity)
                PrintersCrud?.MarkedEntity(zebraPrinterEntity);
            else if (entity is TableScaleModels.PrinterTypeEntity zebraPrinterTypeEntity)
                PrinterTypesCrud?.MarkedEntity(zebraPrinterTypeEntity);
            else if (entity is TableScaleModels.PrinterResourceEntity zebraPrinterResourceRefEntity)
                PrinterResourcesCrud?.MarkedEntity(zebraPrinterResourceRefEntity);
            // DWH.
            else if (entity is TableDwhModels.BrandEntity brandEntity)
                BrandCrud?.MarkedEntity(brandEntity);
            else if (entity is TableDwhModels.InformationSystemEntity informationSystemEntity)
                InformationSystemCrud?.MarkedEntity(informationSystemEntity);
            else if (entity is TableDwhModels.NomenclatureEntity dwhNomenclatureEntity)
                NomenclatureCrud?.MarkedEntity(dwhNomenclatureEntity);
            else if (entity is TableDwhModels.NomenclatureGroupEntity nomenclatureGroupEntity)
                NomenclatureGroupCrud?.MarkedEntity(nomenclatureGroupEntity);
            else if (entity is TableDwhModels.NomenclatureLightEntity nomenclatureLightEntity)
                NomenclatureLightCrud?.MarkedEntity(nomenclatureLightEntity);
            else if (entity is TableDwhModels.NomenclatureTypeEntity nomenclatureTypeEntity)
                NomenclatureTypeCrud?.MarkedEntity(nomenclatureTypeEntity);
            else if (entity is TableDwhModels.StatusEntity statusEntity)
                StatusCrud?.MarkedEntity(statusEntity);
        }

        #endregion
    }
}
