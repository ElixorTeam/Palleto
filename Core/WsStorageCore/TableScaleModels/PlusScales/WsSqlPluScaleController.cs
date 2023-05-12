// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusScales;

/// <summary>
/// SQL-помощник табличных записей таблицы PLUS_SCALES.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluScaleController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluScaleController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluScaleController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlAccessItemHelper AccessItem => WsSqlAccessItemHelper.Instance;
    private WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;
    private WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;

    #endregion

    #region Public and private methods

    public WsSqlPluScaleModel GetNewItem() => AccessItem.GetItemNewEmpty<WsSqlPluScaleModel>();

    public WsSqlPluScaleModel GetItem(Guid? uid) => AccessItem.GetItemNotNullableByUid<WsSqlPluScaleModel>(uid);

    public WsSqlPluScaleModel GetItem(ushort pluNumber)
    {
        WsSqlViewPluScaleModel viewPluScale = ContextCache.ViewPlusScalesDb.Find(x => Equals(x.PluNumber, (ushort)pluNumber));
        return AccessItem.GetItemNotNullableByUid<WsSqlPluScaleModel>(viewPluScale.Uid);
    }

    public List<WsSqlPluScaleModel> GetList() => ContextList.GetListNotNullablePlusScales(new());

    #endregion
}