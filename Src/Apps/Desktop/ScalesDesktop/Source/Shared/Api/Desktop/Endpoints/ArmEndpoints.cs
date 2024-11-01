using ScalesDesktop.Source.Shared.Services.Devices;
using Ws.Desktop.Models.Features.Arms.Output;

namespace ScalesDesktop.Source.Shared.Api.Desktop.Endpoints;

public class ArmEndpoints(IDesktopApi desktopApi, IPrinterService printerService)
{
    public ParameterlessEndpoint<ArmDto> ArmEndpoint { get; } = new(
        desktopApi.GetCurrentArm,
        options: new()
        {
            DefaultStaleTime = TimeSpan.FromHours(1),
            OnSuccess = data =>
            {
                desktopApi.UpdateArm(new() { Version = VersionTracking.CurrentVersion });
                printerService.Setup(data.Result.Printer.Ip, 9100, data.Result.Printer.Type);
            }
        });

    public void UpdateArmCounter(uint counter) =>
        ArmEndpoint.UpdateQueryData(new(), q => q.Data! with { Counter = counter });
}