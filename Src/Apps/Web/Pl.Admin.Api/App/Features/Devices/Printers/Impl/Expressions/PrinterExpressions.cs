using Pl.Admin.Api.App.Features.Devices.Printers.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api.Models;
using Pl.Database.Entities.Ref.Printers;
using Pl.Admin.Models.Features.Devices.Printers.Queries;

namespace Pl.Admin.Api.App.Features.Devices.Printers.Impl.Expressions;

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
            ProductionSite = ProxyUtils.ProductionSite(printer.ProductionSite)
        };

    public static Expression<Func<PrinterEntity, ProxyDto>> ToProxy =>
        printer => ProxyUtils.Printer(printer);

    public static List<PredicateField<PrinterEntity>> GetUqPredicates(UqPrinterProperties uqProperties) =>
    [
        new(i => i.Ip == uqProperties.Ip, "Ip"),
        new(i => i.Name == uqProperties.Name, "Name"),
    ];
}