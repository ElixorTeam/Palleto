using Pl.Admin.Client.Source.Shared.Constants;
using Pl.Admin.Client.Source.Shared.Extensions;
// ReSharper disable once ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Helpers;

public sealed class RedirectHelper(IAuthorizationService authorizationService, ClaimsPrincipal user)
{
    #region Private

    private static string Link(Guid uid, string baseUrl) => Link(uid, baseUrl, true);

    private static string Link(Guid uid, string baseUrl, bool isActive) => !isActive || uid.IsEmpty() ? string.Empty : $"{baseUrl}?id={uid}";

    private bool CheckPolicy(string policyName) => authorizationService.ValidatePolicy(user, policyName);

    #endregion

    #region For Support

    public string ToTemplate(Guid uid) =>
        Link(uid, Urls.Templates, CheckPolicy(PolicyEnum.Support));

    public string ToArm(Guid uid) =>
        Link(uid, Urls.Arms, CheckPolicy(PolicyEnum.Support));

    public string ToPrinter(Guid uid) =>
        Link(uid, Urls.Printers, CheckPolicy(PolicyEnum.Support));

    #endregion

    #region For Admin

    public string ToWarehouse(Guid uid) =>
        Link(uid, Urls.Warehouses, CheckPolicy(PolicyEnum.Admin));

    public string ToProductionSite(Guid uid) =>
        Link(uid, Urls.ProductionSites, CheckPolicy(PolicyEnum.Admin));

    #endregion

    #region For All

    public string ToPlu(Guid uid) => Link(uid, Urls.Plus);

    public string ToBrand(Guid uid) => Link(uid, Urls.Brands);

    public string ToBundle(Guid uid) => Link(uid, Urls.Bundles);

    public string ToClip(Guid uid) => Link(uid, Urls.Clips);

    #endregion
}