// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.TemplatesResources;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPrintersResources;

/// <summary>
/// Item PrinterResource page.
/// </summary>
public sealed partial class ItemPrinterResources : RazorComponentItemBase<WsSqlPrinterResourceFkModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlPrinterModel> PrinterModels { get; set; }

    private List<WsSqlTemplateResourceModel> TemplateResourceModels { get; set; }
    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new List<Action>
        {
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlPrinterResourceFkModel>(IdentityId);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<WsSqlPrinterResourceFkModel>();
                PrinterModels = ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlPrinterModel>(WsSqlCrudConfigUtils
                    .GetCrudConfigComboBox());
                TemplateResourceModels = ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlTemplateResourceModel>(WsSqlCrudConfigUtils
                    .GetCrudConfigComboBox());
            }
        });
    }

    #endregion
}
