using Microsoft.JSInterop;

namespace Pl.Components;

public sealed class JsModuleBinder<TConfig>(IJSRuntime jsRuntime) : IAsyncDisposable
    where TConfig : IEquatable<TConfig>
{
    private IJSObjectReference? _module;
    private TConfig? _config;
    private string? _instanceId;
    private bool _initialized;
    private bool _disposed;

    /// <summary>
    /// Attaches a JS module to an element on this instance. Call once per component.
    /// </summary>
    public async Task SetupAsync(
        string moduleName,
        ElementReference element,
        object dotNetRef,
        TConfig config)
    {
        if (_initialized || _disposed)
            return;

        _instanceId = Guid.NewGuid().ToString("N");

        await InvokeSafeAsync(async () =>
        {
            _module = await jsRuntime.ImportJsModuleAsync(moduleName);
            await _module.InvokeVoidAsync("initialize", element, dotNetRef, _instanceId, config);
            _config = config;
            _initialized = true;
        });
    }

    public Task SyncConfigAsync(TConfig config)
    {
        if (!_initialized || _module is null || (_config is not null && _config.Equals(config)))
            return Task.CompletedTask;

        _config = config;
        return InvokeSafeAsync(() => _module.InvokeVoidAsync("updateConfig", _instanceId, config).AsTask());
    }

    public async ValueTask DisposeAsync()
    {
        if (!_initialized || _disposed)
            return;

        _disposed = true;

        if (_module is null)
            return;

        await InvokeSafeAsync(async () =>
        {
            await _module.InvokeVoidAsync("dispose", _instanceId);
            await _module.DisposeAsync();
            _module = null;
            _initialized = false;
        });
    }

    private async Task InvokeSafeAsync(Func<Task> invoke)
    {
        if (_disposed)
            return;

        try
        {
            await invoke();
        }
        catch (Exception ex) when (ex is JSDisconnectedException or TaskCanceledException or ObjectDisposedException)
        {
            // Expected during circuit disconnect
        }
        catch (InvalidOperationException)
        {
            // JS interop not available during prerendering
        }
    }
}
