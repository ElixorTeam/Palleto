using Microsoft.AspNetCore.Components;
using Pl.Components.Source.Utils;

namespace Pl.Components.Source.UI;

public sealed partial class Badge : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> Attributes { get; set; } = new();
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public BadgeType Type { get; set; } = BadgeType.Default;

    private string BadgeClasses => Css.Class("inline-flex items-center rounded-full border px-2.5 py-0.5",
                                             "text-xs font-semibold transition-colors focus:outline-none focus:ring-2",
                                             "focus:ring-ring focus:ring-offset-2", VariantClasses, Class);

    private string VariantClasses => Type switch
    {
        BadgeType.Default => "border-transparent bg-primary text-primary-foreground hover:bg-primary/80",
        BadgeType.Secondary => "border-transparent bg-secondary text-secondary-foreground hover:bg-secondary/80",
        BadgeType.Destructive => "border-transparent bg-destructive text-destructive-foreground hover:bg-destructive/80",
        BadgeType.Outline => "text-foreground",
        _ => string.Empty
    };
}

public enum BadgeType
{
    Default,
    Destructive,
    Outline,
    Secondary
}