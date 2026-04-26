using Pl.Admin.Models.Features.Devices.Arms.Commands;
using Pl.Admin.Models.Features.Devices.Arms.Queries;

namespace Pl.Admin.Models.Features.Devices.Arms;

public static class ArmMapper
{
    public static ArmUpdateDto DtoToUpdateDto(ArmDto item)
    {
        return new()
        {
            Name = item.Name,
            Type = item.Type,
            Number = item.Number,
            Counter = item.Counter,
            SystemKey = item.SystemKey,
            PrinterId = item.Printer.Id,
            WarehouseId = item.Warehouse.Id
        };
    }
}