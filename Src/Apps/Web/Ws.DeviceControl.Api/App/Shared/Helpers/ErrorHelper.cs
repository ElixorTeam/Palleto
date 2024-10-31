using Microsoft.Extensions.Localization;
using Ws.DeviceControl.Api.App.Shared.Exceptions;
using Ws.DeviceControl.Api.App.Shared.Localization;
using Ws.Shared.Resources;

// ReSharper disable ClassNeverInstantiated.Global

namespace Ws.DeviceControl.Api.App.Shared.Helpers;

public sealed class ErrorHelper(
    IStringLocalizer<ApplicationResources> localizer,
    IStringLocalizer<WsDataResources> wsDataLocalizer)
{
    public string Localize(ApiInternalLocalizingException e)
    {
        string localizeErrorKey = e.ErrorType.GetDescription();
        return string.IsNullOrWhiteSpace(e.PropertyName) ? localizer[localizeErrorKey] :
            string.Format(localizer[$"{localizeErrorKey}ByField"], wsDataLocalizer[$"Col{e.PropertyName}"]);
    }
}