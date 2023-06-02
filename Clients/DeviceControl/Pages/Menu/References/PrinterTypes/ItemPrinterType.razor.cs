// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsStorageCore.TableScaleModels.PrintersTypes;

namespace DeviceControl.Pages.Menu.References.PrinterTypes;

public sealed partial class ItemPrinterType : ItemBase<WsSqlPrinterTypeModel>
{
    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlPrinterTypeModel>(IdentityId);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<WsSqlPrinterTypeModel>();
    }

    #endregion
}