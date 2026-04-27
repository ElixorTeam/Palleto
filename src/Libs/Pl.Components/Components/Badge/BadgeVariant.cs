namespace Pl.Components.Components;

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