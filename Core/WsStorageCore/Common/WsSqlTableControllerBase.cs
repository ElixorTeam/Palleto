// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Common;

/// <summary>
/// Базовый класс контроллера таблицы.
/// </summary>
public class WsSqlTableControllerBase
{
    #region Public and private fields, properties, constructor

    protected WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    protected WsSqlContextListHelper ContextList => WsSqlContextListHelper.Instance;

    protected WsSqlContextItemHelper ContextItem => WsSqlContextItemHelper.Instance;

    protected WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;

    protected WsSqlCrudConfigModel SqlCrudConfig => new(new List<WsSqlFieldFilterModel>(),
            WsSqlEnumIsMarked.ShowAll, false, false, true, false);

    #endregion
}