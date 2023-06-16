// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusWeighingsFks;

/// <summary>
/// SQL-контроллер таблицы PLUS_WEIGHINGS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluWeighingController : WsSqlTableControllerBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluWeighingController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluWeighingController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlPluWeighingModel GetNewItem() => SqlCoreItem.GetItemNewEmpty<WsSqlPluWeighingModel>();

    public WsSqlPluWeighingModel GetItem(Guid? uid) => SqlCoreItem.GetItemNotNullableByUid<WsSqlPluWeighingModel>(uid);

    public List<WsSqlPluWeighingModel> GetList() => ContextList.GetListNotNullablePlusWeighings(SqlCrudConfig);

    #endregion
}