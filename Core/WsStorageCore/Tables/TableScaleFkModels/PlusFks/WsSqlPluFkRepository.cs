// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusFks;

/// <summary>
/// SQL-контроллер таблицы PLUS_BUNDLES_FK.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluFkRepository : WsSqlTableRepositoryBase<WsSqlPluFkModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlPluRepository ContextPlu { get; } = new();

    #endregion

    #region Public and private methods

    public WsSqlPluFkModel GetNewItem()
    {
        WsSqlPluFkModel item = SqlCore.GetItemNewEmpty<WsSqlPluFkModel>();
        item.Plu = ContextPlu.GetNewItem();
        item.Parent = ContextPlu.GetNewItem();
        item.Category = null;
        return item;
    }

    public IEnumerable<WsSqlPluFkModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IEnumerable<WsSqlPluFkModel> items = SqlCore.GetEnumerableNotNullable<WsSqlPluFkModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder)
            items = items.OrderBy(item => item.Plu.Number);
        return items.ToList();
    }
    
    #endregion
}