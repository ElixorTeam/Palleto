using MassaK.Plugin.Abstractions.Enums;
using IDispatcher = Fluxor.IDispatcher;

namespace Pl.Desktop.Client.Source.Shared.Services.Devices.Scales;

public class ScalesMockService : IScalesService
{
    private readonly IDispatcher _dispatcher;
    private const string DefaultComPort = "COM6";

    public bool IsMock() => true;

    public ScalesMockService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
        Setup();
    }

    public void Setup(string comPort = DefaultComPort)
    {
        Task.Delay(300);
    }

    public void Connect()
    {
        Task.Delay(300);
        _dispatcher.Dispatch(new ChangeScalesStatusAction(MassaKStatus.Ready));
    }

    public void Disconnect()
    {
        Task.Delay(300);
        _dispatcher.Dispatch(new ChangeScalesStatusAction(MassaKStatus.Disabled));
    }

    public void Calibrate() => Task.Delay(300);

    public void StartPolling()
    {
        Task.Delay(300);
        _dispatcher.Dispatch(new ChangeWeightAction(9999, true));
    }

    public void StopPolling() => Task.Delay(300);
}