namespace Pl.Components;

public enum AlertVariant
{
    Default,
    Warning,
    Destructive,
}

internal static class AlertVariantExtensions
{
    internal static string Css(this AlertVariant var) =>
        var switch
        {
            AlertVariant.Warning => "border-warning/40 bg-warning/20",
            AlertVariant.Destructive => "border-destructive/40 bg-destructive/20",
            _ => "bg-card text-card-foreground"
        };
}