using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Models.Enums;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesHybrid.Features.Labels.Layouts;

public sealed partial class ScaleStatusIcon: ComponentBase, IDisposable
{
    private DeviceStatusEnum DeviceStatus { get; set; } = DeviceStatusEnum.IsDisabled;
    
    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.Register<GetScaleStatusEvent>(this, GetStatus);
    }

    private void GetStatus(object recipient, GetScaleStatusEvent message)
    {
        DeviceStatus = message.Status switch
        {
            ScalesStatus.IsDisabled => DeviceStatusEnum.IsDisabled,
            ScalesStatus.IsForceDisconnected => DeviceStatusEnum.IsForceDisconnected,
            _ => DeviceStatusEnum.Connected
        };
        InvokeAsync(StateHasChanged);
    }

    private string GetScaleStatusStyle() => DeviceStatus switch
    {
        DeviceStatusEnum.IsDisabled => "bg-gray-300",
        DeviceStatusEnum.IsForceDisconnected => "bg-red-300 shadow-red-200",
        _ => "bg-green-300 shadow-green-200"
    };

    public void Dispose()
    {
        WeakReferenceMessenger.Default.Unregister<GetScaleStatusEvent>(this);
    }
}