// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Helpers;

/// <summary>
/// SQL-помощник методов доступа к табличным записям.
/// Базовый слой доступа к БД.
/// </summary>
public sealed class WsSqlAccessItemHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlAccessItemHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlAccessItemHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private static WsSqlAccessCoreHelper AccessCore => WsSqlAccessCoreHelper.Instance;

    #endregion

    #region Public and public methods

    public T GetItemNewEmpty<T>() where T : WsSqlTableBase, new() => AccessCore.GetItemNewEmpty<T>();

    public T? GetItemNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNullable<T>(sqlCrudConfig);

    public T GetItemNotNullable<T>(SqlCrudConfigModel sqlCrudConfig) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNotNullable<T>(sqlCrudConfig);

    public T GetItemNotNullable<T>(object? value) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNotNullable<T>(value);

    public T? GetItemNullable<T>(SqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNullable<T>(identity);

    public T GetItemNotNullable<T>(SqlFieldIdentityModel identity) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNotNullable<T>(identity);

    public T? GetItemNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNullableByUid<T>(uid);

    public T GetItemNotNullableByUid<T>(Guid? uid) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNotNullableByUid<T>(uid);

    public T? GetItemNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNullableById<T>(id);

    public T GetItemNotNullableById<T>(long? id) where T : WsSqlTableBase, new() =>
        AccessCore.GetItemNotNullableById<T>(id) ?? new();

    public bool IsItemExists<T>(T? item) where T : WsSqlTableBase, new() =>
        AccessCore.IsItemExists(item);

    public SqlCrudResultModel ExecQueryNative(string query, List<SqlParameter> parameters) =>
        AccessCore.ExecQueryNative(query, parameters);

    public SqlCrudResultModel ExecQueryNative(string query, SqlParameter parameter) =>
        AccessCore.ExecQueryNative(query, parameter);

    public SqlCrudResultModel Save<T>(T? item) where T : WsSqlTableBase =>
        AccessCore.Save<T>(item);

    public SqlCrudResultModel Save<T>(T? item, SqlFieldIdentityModel? identity) where T : WsSqlTableBase =>
        AccessCore.Save(item, identity);

    public SqlCrudResultModel SaveOrUpdate<T>(T? item) where T : WsSqlTableBase =>
        AccessCore.SaveOrUpdate(item);

    public SqlCrudResultModel UpdateForce<T>(T? item) where T : WsSqlTableBase =>
        AccessCore.UpdateForce(item);

    public SqlCrudResultModel Delete<T>(T? item) where T : WsSqlTableBase =>
        AccessCore.Delete(item);

    public SqlCrudResultModel Mark<T>(T? item) where T : WsSqlTableBase =>
        AccessCore.Mark(item);

    public WsSqlAppModel GetItemAppOrCreateNew(string appName) => AccessCore.GetItemAppOrCreateNew(appName);

    #endregion
}