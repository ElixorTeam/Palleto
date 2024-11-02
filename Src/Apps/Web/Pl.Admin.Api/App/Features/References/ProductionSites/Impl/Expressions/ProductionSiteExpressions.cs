using Pl.Admin.Api.App.Features.References.ProductionSites.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api.Models;
using Pl.Database.Entities.Ref.ProductionSites;
using Pl.Admin.Models.Features.References.ProductionSites.Queries;

namespace Pl.Admin.Api.App.Features.References.ProductionSites.Impl.Expressions;

internal static class ProductionSiteExpressions
{
    public static Expression<Func<ProductionSiteEntity, ProductionSiteDto>> ToDto =>
        productionSite => new()
        {
            Id = productionSite.Id,
            Name = productionSite.Name,
            Address = productionSite.Address,
            CreateDt = productionSite.CreateDt,
            ChangeDt = productionSite.ChangeDt
        };

    public static List<PredicateField<ProductionSiteEntity>> GetUqPredicates(UqProductionSiteProperties uq) =>
    [
        new(i => i.Name == uq.Name, "Name"),
    ];
}