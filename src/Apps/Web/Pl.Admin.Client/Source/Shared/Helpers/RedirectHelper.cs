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
        Link(uid, Routes.Templates, CheckPolicy(Policies.Support));

    public string ToResource(Guid uid) =>
        Link(uid, Routes.Resources, CheckPolicy(Policies.Support));

    public string ToArm(Guid uid) =>
        Link(uid, Routes.Arms, CheckPolicy(Policies.Support));

    public string ToPrinter(Guid uid) =>
        Link(uid, Routes.Printers, CheckPolicy(Policies.Support));

    public string ToPalletMan(Guid uid) =>
        Link(uid, Routes.PalletMen, CheckPolicy(Policies.Support));

    public string ToUser(Guid uid) =>
        Link(uid, Routes.Users, CheckPolicy(Policies.Support));

    public string ToWarehouse(Guid uid) =>
        Link(uid, Routes.Warehouses, CheckPolicy(Policies.Support));

    public string ToProductionSite(Guid uid) =>
        Link(uid, Routes.ProductionSites, CheckPolicy(Policies.Support));

    #endregion

    #region For All

    public string ToPlu(Guid uid) => Link(uid, Routes.Plus);

    public string ToBox(Guid uid) => Link(uid, Routes.Boxes);

    public string ToBrand(Guid uid) => Link(uid, Routes.Brands);

    public string ToBundle(Guid uid) => Link(uid, Routes.Bundles);

    public string ToClip(Guid uid) => Link(uid, Routes.Clips);

    public string ToLabel(Guid uid) => Link(uid, Routes.Labels);

    public string ToPallet(Guid uid) => Link(uid, Routes.Pallets);

    #endregion
}
