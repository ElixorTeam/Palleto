using Ws.Database.Entities.Ref.ProductionSites;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Impl.Extensions;

internal static class ProductionSiteDtoExtensions
{
    public static ProductionSiteEntity ToEntity(this ProductionSiteCreateDto dto)
    {
        return new()
        {
            Name = dto.Name,
            Address = dto.Address,
        };
    }

    public static void UpdateEntity(this ProductionSiteUpdateDto dto, ProductionSiteEntity entity)
    {
        entity.Name = dto.Name;
        entity.Address = dto.Address;
    }
}