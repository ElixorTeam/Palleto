// This is an independent project of an individual developer. Dear PVS-Studio, please check it
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPrinters;

public sealed partial class ItemPrinter : RazorComponentItemBase<WsSqlPrinterModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlPrinterTypeModel> PrinterTypeModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new List<Action>
        {
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlPrinterModel>(IdentityId);
                PrinterTypeModels = ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlPrinterTypeModel>(WsSqlCrudConfigUtils
                    .GetCrudConfigComboBox());
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<WsSqlPrinterModel>();
            }
        });
    }

    #endregion
}