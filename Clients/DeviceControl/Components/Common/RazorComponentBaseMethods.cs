// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;

namespace DeviceControl.Components.Common;

public partial class RazorComponentBase
{
    #region Public and private methods - Actions

    protected bool SqlItemValidate<T>(T? item) where T : WsSqlTableBase, new()
    {
        bool result = item is not null;
        string detailAddition = string.Empty;
        if (result)
            result = WsSqlValidationUtils.IsValidation(item, ref detailAddition);
        switch (result)
        {
            case false:
                {
                    NotificationMessage msg = new()
                    {
                        Severity = NotificationSeverity.Warning,
                        Summary = WsLocaleCore.Action.ActionDataControl,
                        Detail = detailAddition,
                        Duration = BlazorAppSettingsHelper.DelayError
                    };
                    NotificationService.Notify(msg);
                    return false;
                }
            default:
                return true;
        }
    }

    protected TItem SqlItemNewEmpty<TItem>() where TItem : WsSqlTableBase, new()
    {
        return ContextManager.SqlCore.GetItemNewEmpty<TItem>();
    }

    protected void SqlItemSave<T>(T? item) where T : WsSqlTableBase, new()
    {
        if (item is null || !SqlItemValidate(item)) 
            return;
        if (item.IsNew)
            ContextManager.SqlCore.Save(item);
        else 
            ContextManager.SqlCore.Update(item);
    }

    protected void SqlItemsSave<T>(List<T>? items) where T : WsSqlTableBase, new()
    {
        if (items is null) return;

        foreach (T item in items)
            SqlItemSave(item);
    }

    #endregion
}