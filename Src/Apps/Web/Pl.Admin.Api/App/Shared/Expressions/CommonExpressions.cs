using Pl.Database.Entities.Ref.ProductionSites;

namespace Pl.Admin.Api.App.Shared.Expressions;

internal static class CommonExpressions
{
    public static Expression<Func<ProductionSiteEntity, ProxyDto>> ProductionSite =>
        i => ProxyUtils.ProductionSite(i);
}