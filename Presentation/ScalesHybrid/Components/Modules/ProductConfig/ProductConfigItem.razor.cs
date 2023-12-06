using Microsoft.AspNetCore.Components;

namespace ScalesHybrid.Components.Modules.ProductConfig;

public sealed partial class ProductConfigItem: ComponentBase
{
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public EventCallback OnClickAction { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
}