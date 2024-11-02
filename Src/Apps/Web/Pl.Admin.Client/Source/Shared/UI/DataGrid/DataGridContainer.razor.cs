using System.Drawing;
using Blazorise.DataGrid;
using Pl.Admin.Client.Source.Shared.UI.DataGrid.ContextMenu;

namespace Pl.Admin.Client.Source.Shared.UI.DataGrid;

[CascadingTypeParameter(nameof(TItem))]
public sealed partial class DataGridContainer<TItem> : ComponentBase
{
    [Parameter] public IEnumerable<TItem> Items { get; set; } = [];
    [Parameter] public RenderFragment? ColumnsContent { get; set; }
    [Parameter] public RenderFragment<ContextMenuContext<TItem>>? ContextMenuContent { get; set; }
    [Parameter] public EventCallback<TItem> OnItemSelect { get; set; }
    [Parameter] public int ItemsPerPage { get; set; } = 13;
    [Parameter] public bool IsGroupable { get; set; }
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public bool IsForcePagination { get; set; } = true;
    private TItem? SelectedItem { get; set; }
    private TItem? ContextMenuItem { get; set; }
    private bool IsContextMenuOpen { get; set; }
    private Point ContextMenuPos { get; set; }
    private DataGridContextMenu ContextMenuRef { get; set; } = default!;

    private async Task OnRowContextMenu(DataGridRowMouseEventArgs<TItem> eventArgs)
    {
        if (ContextMenuContent == null) return;
        IsContextMenuOpen = true;
        ContextMenuItem = eventArgs.Item;
        SelectedItem = eventArgs.Item;
        ContextMenuPos = eventArgs.MouseEventArgs.Client;
        await Task.Delay(20);
        await ContextMenuRef.DivRef.FocusAsync();
    }

    private void CloseContextMenu() => IsContextMenuOpen = false;

    private async Task HandleRowDoubleClick(TItem item)
    {
        SelectedItem = item;
        await OnItemSelect.InvokeAsync(item);
    }
}

public record ContextMenuContext<TItem>(TItem? Item, EventCallback CloseContextMenu);