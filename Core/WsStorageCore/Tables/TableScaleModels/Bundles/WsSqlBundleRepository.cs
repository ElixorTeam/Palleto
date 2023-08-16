// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Bundles;

/// <summary>
/// SQL-контроллер таблицы BUNDLES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBundleRepository : WsSqlTableRepositoryBase<WsSqlBundleModel>
{
    #region Public and private methods

    public WsSqlBundleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlBundleModel>();

    public WsSqlBundleModel GetItemByPlu(WsSqlPluModel plu)
    {
        if (plu.IsNew)
            return new();
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFkIdentityFilter(nameof(WsSqlPluBundleFkModel.Plu), plu);
        return SqlCore.GetItemByCrud<WsSqlPluBundleFkModel>(sqlCrudConfig).Bundle;
    }
    
    public WsSqlBundleModel GetItemByUid1C(Guid uid1C)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(new() { Name = nameof(WsSqlTable1CBase.Uid1C), Value = uid1C });
        return SqlCore.GetItemByCrud<WsSqlBundleModel>(sqlCrudConfig);
    }
    
    public IEnumerable<WsSqlBundleModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Name));
        return SqlCore.GetEnumerableNotNullable<WsSqlBundleModel>(sqlCrudConfig);
    }

    #endregion
}