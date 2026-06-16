using Pl.Admin.Models.Features.Devices.Printers.Commands;
using Pl.Admin.Models.Features.Devices.Printers.Queries;

namespace Pl.Admin.Models.Features.Devices.Printers;

public static class PrinterMapExtensions
{
    public static PrinterUpdateDto ToUpdateDto(this PrinterDto item)
    {
        return new()
        {
            Name = item.Name,
            Ip = item.Ip,
            Type = item.Type
        };
    }
}