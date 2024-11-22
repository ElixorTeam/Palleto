using Pl.Admin.Client.Source.Shared.Constants;
using Pl.Admin.Client.Source.Shared.Extensions;

namespace Pl.Admin.Client.Source.Shared.Helpers;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class RedirectHelper(IAuthorizationService authorizationService, NavigationManager navigationManager, ClaimsPrincipal user)
{
    public string ToAbsoluteUrl(string relativePath) => new Uri(new(navigationManager.BaseUri), relativePath).AbsoluteUri;

    #region Private

    private static string Link(Guid uid, string baseUrl) => Link(uid, baseUrl, true);

    private static string Link(Guid uid, string baseUrl, bool isActive) => !isActive || uid.IsEmpty() ? string.Empty : $"{baseUrl}/{uid}";

    private bool CheckPolicy(string policyName) => authorizationService.ValidatePolicy(user, policyName);

    #endregion

    #region For Support

    public string ToTemplate(Guid uid) =>
        Link(uid, Urls.Templates, CheckPolicy(PolicyEnum.Support));

    public string ToResource(Guid uid) =>
        Link(uid, Urls.Resources, CheckPolicy(PolicyEnum.Support));

    public string ToArm(Guid uid) =>
        Link(uid, Urls.Arms, CheckPolicy(PolicyEnum.Support));

    public string ToPrinter(Guid uid) =>
        Link(uid, Urls.Printers, CheckPolicy(PolicyEnum.Support));

    public string ToPalletMan(Guid uid) =>
        Link(uid, Urls.PalletMen, CheckPolicy(PolicyEnum.Support));

    public string ToUser(Guid uid) =>
        Link(uid, Urls.Users, CheckPolicy(PolicyEnum.Support));

    public string ToWarehouse(Guid uid) =>
        Link(uid, Urls.Warehouses, CheckPolicy(PolicyEnum.Support));

    public string ToProductionSite(Guid uid) =>
        Link(uid, Urls.ProductionSites, CheckPolicy(PolicyEnum.Support));


    #endregion

    #region For All

    public string ToPlu(Guid uid) => Link(uid, Urls.Plus);

    public string ToBox(Guid uid) => Link(uid, Urls.Boxes);

    public string ToBrand(Guid uid) => Link(uid, Urls.Brands);

    public string ToBundle(Guid uid) => Link(uid, Urls.Bundles);

    public string ToClip(Guid uid) => Link(uid, Urls.Clips);

    public string ToLabel(Guid uid) => Link(uid, Urls.Labels);

    public string ToPallet(Guid uid) => Link(uid, Urls.Pallets);

    #endregion
}
