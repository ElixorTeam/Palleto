namespace Pl.Components;

public enum BadgeVariant
{
    /// <summary>
    /// Suitable for highlighting important items or new content.
    /// CSS vars --primary | --primary-foreground
    /// </summary>
    Default,

    /// <summary>
    /// For alternative or less prominent labels
    /// CSS vars --secondary | --secondary-foreground
    /// </summary>
    Secondary,

    /// <summary>
    /// For warnings or errors
    /// CSS vars --destructive and --destructive-foreground
    /// </summary>
    Destructive,

    /// <summary>
    /// Minimal style for subtle categorization or tags
    /// CSS vars --primary for text color
    /// </summary>
    Outline
}

internal static class BadgeVariantExtensions
{
    internal static string GetCss(this BadgeVariant var) =>
        var switch
        {
            BadgeVariant.Default => "border-transparent bg-primary text-primary-foreground hover:bg-primary/80",
            BadgeVariant.Secondary => "border-transparent bg-secondary text-secondary-foreground hover:bg-secondary/80",
            BadgeVariant.Destructive => "border-transparent bg-destructive text-destructive-foreground hover:bg-destructive/80",
            BadgeVariant.Outline => "text-foreground",
            _ => string.Empty
        };
}