using TscZebra.Plugin.Abstractions.Enums;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Impl;

public class ArmService : IArmService
{
    public OutputDto<ArmValue>? GetByName(string armName)
    {
        using var context = new WsDbContext();
        ArmValue? arm = context.Lines
            .Where(i => i.PcName == armName)
            .Select(i => new ArmValue
            {
                Id = i.Id,
                Counter = (uint)Math.Abs(i.Counter),
                Name = i.Name,
                PcName = i.PcName,
                Warehouse = i.Warehouse.Name,
                PrinterValue = new()
                {
                    Ip = i.Printer.Ip,
                    Name = i.Printer.Name,
                    Type = i.Printer.Type,
                }
            })
            .FirstOrDefault();

        return arm is not null ? new (arm) : null;
    }
}