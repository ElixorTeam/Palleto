using Pl.Desktop.Client.Source.Shared.Services.Stores;
using TscZebra.Plugin.Abstractions.Enums;
using IDispatcher = Fluxor.IDispatcher;

namespace Pl.Desktop.Client.Source.Shared.Services.Devices;

public class MockPrinterService(IDispatcher dispatcher) : IPrinterService
{
    public bool IsMock() => true;

    public void Setup(IPAddress ip, int port, PrinterTypes types)
    {
        Task.Delay(300);
        dispatcher.Dispatch(new ChangePrinterStatusAction(PrinterStatus.Ready));
    }

    public async Task ConnectAsync()
    {
        await Task.Delay(300);
        dispatcher.Dispatch(new ChangePrinterStatusAction(PrinterStatus.Ready));
    }

    public async Task RequestStatusAsync()
    {
        await Task.Delay(300);
        dispatcher.Dispatch(new ChangePrinterStatusAction(PrinterStatus.Ready));
    }

    public Task PrintZplAsync(string zpl) => Task.Delay(200);

    public void Disconnect()
    {
        Task.Delay(100);
        dispatcher.Dispatch(new ChangePrinterStatusAction(PrinterStatus.Disconnected));
    }
}