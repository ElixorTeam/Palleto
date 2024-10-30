using Ws.Database.Entities.Ref.Printers;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Expressions;

public static class PrinterExpressions
{
    public static Expression<Func<PrinterEntity, PrinterDto>> ToDto =>
        printer => new()
        {
            Id = printer.Id,
            Name = printer.Name,
            Ip = printer.Ip,
            Type = printer.Type,
            CreateDt = printer.CreateDt,
            ChangeDt = printer.ChangeDt,
            ProductionSite = new(printer.ProductionSite.Id, printer.ProductionSite.Name)
        };

    public static Expression<Func<PrinterEntity, ProxyDto>> ToProxy =>
        printer => new(printer.Id, printer.Name + " | " + printer.Ip);

    public static List<PredicateField<PrinterEntity>> GetUqPredicates(UqPrinterProperties uqProperties) =>
    [
        new(i => i.Ip == uqProperties.Ip, "Ip"),
        new(i => i.Name == uqProperties.Name, "Name"),
    ];
}