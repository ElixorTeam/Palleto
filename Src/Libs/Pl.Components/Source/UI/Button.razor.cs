using Microsoft.AspNetCore.Components;
using Pl.Components.Source.Utils;

namespace Pl.Components.Source.UI;

public sealed partial class Button : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public ButtonVariantType Variant { get; set; } = ButtonVariantType.Default;
    [Parameter] public ButtonSizeType Size { get; set; } = ButtonSizeType.Default;
    [Parameter] public EventCallback OnClick { get; set; }
    [Parameter] public ButtonType Type { get; set; } = ButtonType.Button;
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> Attributes { get; set; } = new();

    private string ButtonClasses => Css.Class("inline-flex items-center justify-center whitespace-nowrap",
                                              "rounded-md text-sm font-medium ring-offset-background transition-colors",
                                              "focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring",
                                              "focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-5",
                                              VariantClasses, SizeClasses, Class);

    private string VariantClasses => Variant switch
    {
        ButtonVariantType.Default => "bg-primary text-primary-foreground hover:bg-primary/90",
        ButtonVariantType.Destructive => "bg-destructive text-destructive-foreground hover:bg-destructive/90",
        ButtonVariantType.Outline => "border border-input bg-background hover:bg-accent hover:text-accent-foreground",
        ButtonVariantType.Secondary => "bg-secondary text-secondary-foreground hover:bg-secondary/80",
        ButtonVariantType.Ghost => "hover:bg-accent hover:text-accent-foreground",
        ButtonVariantType.Link => "text-primary underline-offset-4 hover:underline",
        _ => string.Empty
    };

    private string SizeClasses => Size switch
    {
        ButtonSizeType.Default => "h-9 px-4 py-2",
        ButtonSizeType.Small => "h-8 rounded-md px-3 text-xs",
        ButtonSizeType.Large => "h-10 rounded-md px-8",
        ButtonSizeType.Full => "size-full",
        ButtonSizeType.Icon => "h-9 w-9",
        _ => string.Empty
    };

    private string HtmlType => Type switch
    {
        ButtonType.Reset => "reset",
        ButtonType.Submit => "submit",
        _ => "button"
    };
}

public enum ButtonVariantType
{
    Default,
    Destructive,
    Outline,
    Secondary,
    Ghost,
    Link
}

public enum ButtonSizeType
{
    Default,
    Small,
    Large,
    Full,
    Icon
}

public enum ButtonType
{
    Button,
    Reset,
    Submit
}