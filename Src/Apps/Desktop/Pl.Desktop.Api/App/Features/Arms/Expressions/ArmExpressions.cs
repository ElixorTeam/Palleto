using Pl.Database.Entities.Ref.Arms;
using Pl.Desktop.Models.Features.Arms.Output;

namespace Pl.Desktop.Api.App.Features.Arms.Expressions;

internal static class ArmExpressions
{
    public static Expression<Func<ArmEntity, ArmDto>> ToDto => arm =>
        new()
        {
            Id = arm.Id,
            Counter = (uint)arm.Counter,
            Name = arm.Name,
            SystemKey = arm.SystemKey,
            Warehouse = new(arm.Warehouse.Id, arm.Warehouse.Name),
            Type = arm.Type,
            Printer = new()
            {
                Ip = arm.Printer.Ip,
                Name = arm.Printer.Name,
                Type = arm.Printer.Type,
            }
        };
}