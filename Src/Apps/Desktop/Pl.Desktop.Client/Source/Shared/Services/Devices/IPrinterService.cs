using TscZebra.Plugin.Abstractions.Enums;

namespace Pl.Desktop.Client.Source.Shared.Services.Devices;

public interface IPrinterService
{
    public void Setup(IPAddress ip, int port, PrinterTypes types);

    public Task ConnectAsync();

    public void Disconnect();

    public Task RequestStatusAsync();

    public Task PrintZplAsync(string zpl);

    public bool IsMock();
}