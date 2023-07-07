// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Security.Claims;
using DeviceControl.Services;
using WsBlazorCore.Settings;
using WsStorageCore.Helpers;

namespace DeviceControl.Components.Common;

public partial class RazorComponentBase : LayoutComponentBase
{
    #region Public and private fields, properties, constructor

    #region Inject

    [Inject] protected DialogService DialogService { get; set; }
    [Inject] protected NotificationService NotificationService { get; set; }
    [Inject] protected UserService UserService { get; set; }

    #endregion

    #region Constants

    protected static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    protected static BlazorAppSettingsHelper BlazorAppSettings => BlazorAppSettingsHelper.Instance;

    #endregion

    #region Parameters
    
    [Parameter] public WsSqlTableBase? SqlItem { get; set; }

    #endregion
    protected ClaimsPrincipal? User { get; set; }

    public RazorComponentBase()
    {
        SqlItem = null;
    }

    #endregion

    protected override async Task OnInitializedAsync()
    {
        User = await UserService.GetUser();
        await base.OnInitializedAsync();
    }
}