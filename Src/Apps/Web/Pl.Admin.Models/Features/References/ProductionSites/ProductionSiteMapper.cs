using Pl.Admin.Models.Features.References.ProductionSites.Commands;
using Pl.Admin.Models.Features.References.ProductionSites.Queries;

namespace Pl.Admin.Models.Features.References.ProductionSites;

public static class ProductionSiteMapper
{
    public static ProductionSiteUpdateDto DtoToUpdateDto(ProductionSiteDto item)
    {
        return new()
        {
            Name = item.Name,
            Address = item.Address,
        };
    }
}