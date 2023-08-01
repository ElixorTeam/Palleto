// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Components.Nested.PrintersResources;

public sealed partial class ItemPrinterResources : ItemBase<WsSqlPrinterResourceFkModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlPrinterModel> PrinterModels { get; set; }

    private List<WsSqlTemplateResourceModel> TemplateResourceModels { get; set; }

    #endregion

    #region Public and private methods

    // protected override void SetSqlItemCast()
    // {
    //     SqlItemCast = ContextManager.SqlCore.GetItemNotNullable<WsSqlPrinterResourceFkModel>(Id);
    //     if (SqlItemCast.IsNew)
    //         SqlItemCast = SqlItemNewEmpty<WsSqlPrinterResourceFkModel>();
    //     PrinterModels = ContextManager.SqlCore.GetListNotNullable<WsSqlPrinterModel>(WsSqlCrudConfigFabric
    //         .GetCrudConfig();
    //     TemplateResourceModels = ContextManager.SqlCore.GetListNotNullable<WsSqlTemplateResourceModel>(WsSqlCrudConfigFabric
    //         .GetCrudConfigComboBox());
    // }

    #endregion
}