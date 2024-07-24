using System.Net;
using ScalesDesktop.Source.Shared.Services.Stores;
using TscZebra.Plugin;
using TscZebra.Plugin.Abstractions;
using TscZebra.Plugin.Abstractions.Enums;
using TscZebra.Plugin.Abstractions.Exceptions;

namespace ScalesDesktop.Source.Shared.Services;

public class PrinterService(Fluxor.IDispatcher dispatcher): IDisposable
{
    private IZplPrinter Printer { get; set; } = PrinterFactory.Create(IPAddress.Parse("127.0.0.1"), 9100, PrinterTypes.Tsc);

    public void Setup(IPAddress ip, int port, PrinterTypes types)
    {
        Printer.OnStatusChanged -= OnPrinterStatusChanged;
        Printer.Disconnect();
        Printer = PrinterFactory.Create(ip, port, types);
        Printer.OnStatusChanged += OnPrinterStatusChanged;
    }

    public async Task ConnectAsync()
    {
        try
        {
            await Printer.ConnectAsync();
            dispatcher.Dispatch(new ChangePrinterStatusAction(PrinterStatus.Ready));
            Printer.StartStatusPolling(10);
        }
        catch (PrinterConnectionException)
        {
            dispatcher.Dispatch(new ChangePrinterStatusAction(PrinterStatus.Disconnected));
        }
    }

    public async Task RequestStatusAsync()
    {
        try
        {
            dispatcher.Dispatch(new ChangePrinterStatusAction(await Printer.RequestStatusAsync()));
        }
        catch
        {
            dispatcher.Dispatch(new ChangePrinterStatusAction(PrinterStatus.Disconnected));
        }
    }

    public Task PrintZplAsync(string zpl) => Printer.PrintZplAsync(zpl);

    public void Disconnect()
    {
        Printer.Disconnect();
        dispatcher.Dispatch(new ChangePrinterStatusAction(PrinterStatus.Disconnected));
    }

    private void OnPrinterStatusChanged(object? sender, PrinterStatus e) =>
        dispatcher.Dispatch(new ChangePrinterStatusAction(e));

    public void Dispose()
    {
        Printer.OnStatusChanged -= OnPrinterStatusChanged;
        Printer.Dispose();
        GC.SuppressFinalize(this);
    }
}