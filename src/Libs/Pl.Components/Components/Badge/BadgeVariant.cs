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
            BadgeVariant.Default => "bg-primary text-primary-foreground",
            BadgeVariant.Secondary => "bg-secondary text-secondary-foreground",
            BadgeVariant.Destructive => "bg-destructive text-white focus-visible:ring-destructive/20 dark:bg-destructive/60 dark:focus-visible:ring-destructive/40",
            BadgeVariant.Outline => "border-border text-foreground",
            _ => string.Empty
        };
}