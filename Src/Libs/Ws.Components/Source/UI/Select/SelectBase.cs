using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Ws.Components.Source.UI.Select;

public abstract class SelectBase<TItem, TValue> : ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }
    [Parameter] public Func<TItem, string> ItemDisplayName { get; set; } = item => item?.ToString() ?? string.Empty;
    [Parameter] public bool IgnoreWidth { get; set; }
    [Parameter] public string HtmlId { get; set; } = $"id-{Guid.NewGuid()}";

    internal Dictionary<Guid, SelectItem<TItem, TValue>> SelectItems { get; private set; } = new();
    internal double TriggerWidth { get; private set; }
    public bool IsDropdownOpened { get; set; }
    public string SearchString { get; set; } = string.Empty;

    private DotNetObjectReference<SelectBase<TItem, TValue>> DotNetObjRef { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        DotNetObjRef = DotNetObjectReference.Create(this);
        await InitializeDropdownAsync();
    }

    public async Task ToggleDropdown()
    {
        IsDropdownOpened = !IsDropdownOpened;
        await Task.Yield();
        if (IsDropdownOpened && TriggerWidth <= 10)
            await SetDropdownWidthAsync();
    }

    internal async Task FocusNextItemAsync(Guid id)
    {
        List<Guid> keys = SelectItems.Keys.ToList();
        int currentIndex = keys.IndexOf(id);
        if (currentIndex == -1 || currentIndex >= keys.Count - 1) return;

        Guid nextId = keys[currentIndex + 1];
        await SelectItems[nextId].TryFocusAsync();
    }

    internal async Task FocusPreviousItemAsync(Guid id)
    {
        List<Guid> keys = SelectItems.Keys.ToList();
        int currentIndex = keys.IndexOf(id);
        if (currentIndex <= 0) return;

        Guid previousId = keys[currentIndex - 1];
        await SelectItems[previousId].TryFocusAsync();
    }

    internal void SetSearchingValue(string search)
    {
        SearchString = search;
        StateHasChanged();
    }

    internal void Reset()
    {
        SearchString = string.Empty;
        SelectItems = SelectItems.Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
        StateHasChanged();
    }

    internal Task TriggerValueChanged() => ValueChanged.InvokeAsync(Value);

    internal void Register(SelectItem<TItem, TValue> item) => SelectItems.Add(item.Id, item);

    internal void Unregister(SelectItem<TItem, TValue> item) => SelectItems.Remove(item.Id);

    internal async Task SetValueDirect(TValue? value)
    {
        Value = value;
        await TriggerValueChanged();
    }

    protected internal abstract Task SetValue(TItem item, bool withClose = true);
    protected internal abstract bool IsItemSelected(TItem item);

    private async Task SetDropdownWidthAsync()
    {
        double width = await JsRuntime.InvokeAsync<double>("getElementWidthById", HtmlId);
        if (Math.Abs(width - TriggerWidth) < 5) return;
        TriggerWidth = width;
        StateHasChanged();
    }

    private async Task InitializeDropdownAsync()
    {
        await SetDropdownWidthAsync();
        await JsRuntime.InvokeVoidAsync("addDotNetEventListener", "resize", DotNetObjRef, "HandleResize");
    }

    [JSInvokable("HandleResize")]
    public async Task HandleResize() => await SetDropdownWidthAsync();

    public async ValueTask DisposeAsync()
    {
        try
        {
            await JsRuntime.InvokeVoidAsync("removeDotNetEventListener", "resize", DotNetObjRef, "HandleResize");
            DotNetObjRef.Dispose();
        }
        catch
        {
            // pass
        }
    }
}