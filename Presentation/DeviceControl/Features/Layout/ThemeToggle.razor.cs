using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DeviceControl.Features.Layout;

public sealed partial class ThemeToggle: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    private IJSObjectReference? Module { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./libs/theme-utils.js");
    }

    private async Task SetTheme(string theme) =>
        await Module!.InvokeVoidAsync("switchTheme", theme);
    

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (Module == null) return;
            await Module.DisposeAsync();
        }
        catch (Exception ex) when (ex is JSDisconnectedException or ArgumentNullException)
        {
            // pass error
        }
    }
}