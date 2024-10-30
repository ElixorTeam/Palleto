using Ws.Database.Entities.Ref.ProductionSites;

namespace Ws.DeviceControl.Api.App.Shared.Expressions;

internal static class ProductionSiteCommonExpressions
{
    public static Expression<Func<ProductionSiteEntity, ProxyDto>> ToProxy =>
        productionSite => new(productionSite.Id, productionSite.Name);
}