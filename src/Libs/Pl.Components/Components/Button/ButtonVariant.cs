namespace Pl.Components.Components;

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