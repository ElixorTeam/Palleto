using Ws.Database.Entities.Ref.ProductionSites;

namespace Ws.DeviceControl.Api.App.Shared.Expressions;

internal static class CommonExpressions
{
    public static Expression<Func<ProductionSiteEntity, ProxyDto>> ProductionSite =>
        i => ProxyUtils.ProductionSite(i);
}