namespace Pl.Components;

public enum BadgeVariant
{
    /// <summary>
    /// Suitable for highlighting important items or new content.
    /// </summary>
    Default,

    /// <summary>
    /// For alternative or less prominent labels
    /// </summary>
    Secondary,

    /// <summary>
    /// For warnings or errors
    /// </summary>
    Destructive,

    /// <summary>
    /// Minimal style for subtle categorization or tags
    /// </summary>
    Outline
}

internal static class BadgeVariantExtensions
{
    internal static string Css(this BadgeVariant var) =>
        var switch
        {
            BadgeVariant.Default => "border-transparent bg-primary text-primary-foreground hover:bg-primary/80",
            BadgeVariant.Secondary => "border-transparent bg-secondary text-secondary-foreground hover:bg-secondary/80",
            BadgeVariant.Destructive => "border-transparent bg-destructive text-destructive-foreground hover:bg-destructive/80",
            BadgeVariant.Outline => "text-foreground",
            _ => string.Empty
        };
}