using Microsoft.JSInterop;

namespace Pl.Components;

public static class JsRuntimeExtensions
{
    public static async Task<IJSObjectReference> ImportJsModuleAsync(this IJSRuntime jsRuntime, string moduleName)
        => await jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/Pl.Components/js/{moduleName}.js");
}