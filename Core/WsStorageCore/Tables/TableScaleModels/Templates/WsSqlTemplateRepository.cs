// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Views.ViewRefModels.PluLines;

namespace WsStorageCore.Tables.TableScaleModels.Templates;

/// <summary>
/// SQL-контроллер таблицы Templates.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlTemplateRepository : WsSqlTableRepositoryBase<WsSqlTemplateModel>
{
    #region Public and private methods

    public WsSqlTemplateModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlTemplateModel>();
    
    public List<WsSqlTemplateModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTemplateModel.Title) });
        List<WsSqlTemplateModel> list = SqlCore.GetListNotNullable<WsSqlTemplateModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Title).ToList();
        return list;
    }
    
    #endregion
}