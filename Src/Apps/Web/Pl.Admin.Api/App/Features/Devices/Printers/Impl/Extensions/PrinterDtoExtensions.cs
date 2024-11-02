using Pl.Database.Entities.Ref.Printers;
using Pl.Database.Entities.Ref.ProductionSites;
using Pl.Admin.Models.Features.Devices.Printers.Commands;

namespace Pl.Admin.Api.App.Features.Devices.Printers.Impl.Extensions;

internal static class PrinterDtoExtensions
{
    public static PrinterEntity ToEntity(this PrinterCreateDto dto, ProductionSiteEntity productionSiteEntity)
    {
        return new()
        {
            Name = dto.Name,
            Ip = dto.Ip,
            Type = dto.Type,
            ProductionSite = productionSiteEntity
        };
    }


    public static void UpdateEntity(this PrinterUpdateDto dto, PrinterEntity entity)
    {
        entity.Name = dto.Name;
        entity.Ip = dto.Ip;
        entity.Type = dto.Type;
    }
}