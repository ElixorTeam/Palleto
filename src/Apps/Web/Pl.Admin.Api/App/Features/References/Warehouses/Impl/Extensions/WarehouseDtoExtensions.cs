using Pl.Database.Entities.Ref.ProductionSites;
using Pl.Database.Entities.Ref.Warehouses;
using Pl.Admin.Models.Features.References.Warehouses.Commands;

namespace Pl.Admin.Api.App.Features.References.Warehouses.Impl.Extensions;

internal static class WarehouseDtoExtensions
{
    public static WarehouseEntity ToEntity(this WarehouseCreateDto dto, ProductionSiteEntity productionSiteEntity)
    {
        return new()
        {
            Name = dto.Name,
            Uid1C = dto.Id1C,
            ProductionSite = productionSiteEntity
        };
    }


    public static void UpdateEntity(this WarehouseUpdateDto dto, WarehouseEntity entity)
    {
        entity.Name = dto.Name;
        entity.Uid1C = dto.Id1C;
    }
}