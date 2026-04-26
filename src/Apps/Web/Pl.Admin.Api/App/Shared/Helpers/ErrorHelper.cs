using Microsoft.Extensions.Localization;
using Pl.Admin.Api.App.Shared.Exceptions;
using Pl.Admin.Api.App.Shared.Localization;
using Pl.Shared.Resources;

// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Api.App.Shared.Helpers;

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