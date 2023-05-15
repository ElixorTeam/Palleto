// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Bundles;

/// <summary>
/// SQL-помощник табличных записей таблицы BUNDLES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlBundleController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlBundleController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlBundleController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlBundleModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlBundleModel>();

    public WsSqlBundleModel GetItem(WsSqlPluModel plu) => ContextItem.GetItemPluBundleFkNotNullable(plu).Bundle;

    public List<WsSqlBundleModel> GetList() => ContextList.GetListNotNullableBundles(new());

    #endregion
}