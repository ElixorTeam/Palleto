namespace Pl.Components;

/// <summary>
/// Defines the visual style variant for a Button component.
/// </summary>
public enum ButtonVariant
{
    /// <summary>
    /// For submit, save, confirm actions
    /// CSS vars --primary | --primary-foreground.
    /// /// </summary>
    Default,

    /// <summary>
    /// For delete, remove, cancel actions (dangerous)
    /// CSS vars --destructive | --destructive-foreground.
    /// </summary>
    Destructive,

    /// <summary>
    /// For secondary actions or form controls
    /// CSS vars --border | --input
    /// </summary>
    Outline,

    /// <summary>
    /// For alternative actions or less prominent CTAs.
    /// CSS vars --secondary | --secondary-foreground
    /// </summary>
    Secondary,

    /// <summary>
    /// For tertiary actions, toolbars, or navigation.
    /// CSS vars --accent (background on hover)
    /// </summary>
    Ghost,

    /// <summary>
    /// For inline actions, navigation
    /// CSS vars --primary for text color.
    /// </summary>
    Link
}

internal static class ButtonVariantExtensions
{
    internal static string Css(this ButtonVariant variant) =>
        variant switch
        {
            ButtonVariant.Default => "bg-primary text-primary-foreground hover:bg-primary/90",
            ButtonVariant.Destructive => "bg-destructive text-destructive-foreground hover:bg-destructive/90",
            ButtonVariant.Outline => "border border-input bg-background hover:bg-accent hover:text-accent-foreground",
            ButtonVariant.Secondary => "bg-secondary text-secondary-foreground hover:bg-secondary/80",
            ButtonVariant.Ghost => "hover:bg-accent hover:text-accent-foreground",
            ButtonVariant.Link => "text-primary underline-offset-4 hover:underline",
            _ => string.Empty
        };
}