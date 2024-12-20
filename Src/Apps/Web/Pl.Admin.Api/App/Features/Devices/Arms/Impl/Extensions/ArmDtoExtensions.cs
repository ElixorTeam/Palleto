using Pl.Database.Entities.Ref.Printers;
using Pl.Database.Entities.Ref.Warehouses;
using Pl.Admin.Models.Features.Devices.Arms.Commands;
using Pl.Database.Entities.Ref.Arms;

namespace Pl.Admin.Api.App.Features.Devices.Arms.Impl.Extensions;

internal static class ArmDtoExtensions
{
    public static ArmEntity ToEntity(this ArmCreateDto dto, WarehouseEntity warehouse, PrinterEntity printer)
    {
        return new()
        {
            Name = dto.Name,
            Number = dto.Number,
            SystemKey = dto.SystemKey,
            Type = dto.Type,
            Printer = printer,
            Warehouse = warehouse,
            Counter = 1
        };
    }

    public static void UpdateEntity(this ArmUpdateDto dto, ArmEntity entity, PrinterEntity printer, WarehouseEntity warehouse)
    {
        entity.Name = dto.Name;
        entity.Type = dto.Type;
        entity.Printer = printer;
        entity.Number = dto.Number;
        entity.SystemKey = dto.SystemKey;
        entity.Counter = dto.Counter;
        entity.Warehouse = warehouse;
    }
}