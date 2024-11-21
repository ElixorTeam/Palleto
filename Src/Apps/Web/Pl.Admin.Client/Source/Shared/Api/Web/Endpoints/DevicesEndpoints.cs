using Phetch.Core;
using Pl.Admin.Models;
using Pl.Admin.Models.Features.Devices.Arms.Queries;
using Pl.Admin.Models.Features.Devices.Printers.Queries;
using Pl.Admin.Models.Features.Print.Labels;
using Pl.Admin.Models.Features.Print.Pallets;

// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Web.Endpoints;

public class DevicesEndpoints(IWebApi webApi)
{
    # region Arm

    public Endpoint<Guid, ArmDto[]> ArmsEndpoint { get; } = new(
        webApi.GetArmsByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, ArmDto> ArmEndpoint { get; } = new(
        webApi.GetArmByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, LabelDto[]> ArmLabels { get; } = new(
        webApi.GetLabelsWorkShiftByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, PalletDto[]> ArmPallets { get; } = new(
        webApi.GetPalletsWorkShiftByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<ArmAnalyticsArg, AnalyticDto[]> ArmAnalytics { get; } = new(
        arg => webApi.GetArmAnalytic(arg.ArmId, arg.Date),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddArm(Guid productionSiteId, ArmDto arm)
    {
        ArmsEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [arm] : query.Data.Prepend(arm).ToArray());
        ArmEndpoint.UpdateQueryData(arm.Id, _ => arm);
    }

    public void UpdateArm(Guid productionSiteId, ArmDto arm)
    {
        ArmsEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [arm] : query.Data.ReplaceItemBy(arm, p => p.Id == arm.Id).ToArray());
        ArmEndpoint.UpdateQueryData(arm.Id, _ => arm);
    }

    public void DeleteArm(Guid productionSiteId, Guid armId)
    {
        ArmsEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [] : query.Data.Where(x => x.Id != armId).ToArray());
        ArmEndpoint.Invalidate(armId);
    }

    # endregion

    # region ArmPlu

    public Endpoint<Guid, PluArmDto[]> ArmPluEndpoint { get; } = new(
        webApi.GetArmPlus,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddArmPlu(Guid armId, Guid pluId) =>
        ArmPluEndpoint.UpdateQueryData(armId, query =>
            query.Data?.Select(item =>
            {
                if (item.Id == pluId) item = item with { IsActive = true };
                return item;
            }).ToArray() ?? []);

    public void DeleteArmPlu(Guid armId, Guid pluId) =>
        ArmPluEndpoint.UpdateQueryData(armId, query =>
            query.Data?.Select(item =>
            {
                if (item.Id == pluId) item = item with { IsActive = false };
                return item;
            }).ToArray() ?? []);

    # endregion

    # region Printer

    public Endpoint<Guid, PrinterDto[]> PrintersEndpoint { get; } = new(
        webApi.GetPrintersByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public Endpoint<Guid, PrinterDto> PrinterEndpoint { get; } = new(
        webApi.GetPrinterByUid,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddPrinter(Guid productionSiteId, PrinterDto printer)
    {
        PrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [printer] : query.Data.Prepend(printer).ToArray());
        PrinterEndpoint.UpdateQueryData(printer.Id, _ => printer);
        AddProxyPrinter(productionSiteId, new(printer.Id, printer.Name));
    }

    public void UpdatePrinter(Guid productionSiteId, PrinterDto printer)
    {
        PrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [printer] : query.Data.ReplaceItemBy(printer, p => p.Id == printer.Id).ToArray());
        PrinterEndpoint.UpdateQueryData(printer.Id, _ => printer);
        UpdateProxyPrinter(productionSiteId, new(printer.Id, printer.Name));
    }

    public void DeletePrinter(Guid productionSiteId, Guid printerId)
    {
        PrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [] : query.Data.Where(x => x.Id != printerId).ToArray());
        PrinterEndpoint.Invalidate(printerId);
        DeleteProxyPrinter(productionSiteId, printerId);
    }

    # endregion

    # region ProxyPrinter

    public Endpoint<Guid, ProxyDto[]> ProxyPrintersEndpoint { get; } = new(
        webApi.GetProxyPrintersByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProxyPrinter(Guid productionSiteId, ProxyDto proxyPrinter) =>
        ProxyPrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [proxyPrinter] : query.Data.Prepend(proxyPrinter).ToArray());

    public void UpdateProxyPrinter(Guid productionSiteId, ProxyDto proxyPrinter) =>
        ProxyPrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [proxyPrinter] : query.Data.ReplaceItemBy(proxyPrinter, p => p.Id == proxyPrinter.Id).ToArray());

    public void DeleteProxyPrinter(Guid productionSiteId, Guid proxyPrinterId) =>
        ProxyPrintersEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? [] : query.Data.Where(x => x.Id != proxyPrinterId).ToArray());

    # endregion
}

public record ArmAnalyticsArg(Guid ArmId, DateOnly Date);