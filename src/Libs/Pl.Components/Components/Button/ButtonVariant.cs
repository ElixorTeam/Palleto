namespace Pl.Components;

/// <summary>
/// Defines the visual style variant for a Button component.
/// </summary>
public enum ButtonVariant
{
    /// <summary>
    /// For submit, save, confirm actions
    /// </summary>
    Default,

    /// <summary>
    /// For delete, remove, cancel actions (dangerous)
    /// </summary>
    Destructive,

    /// <summary>
    /// For secondary actions or form controls
    /// </summary>
    Outline,

    /// <summary>
    /// For alternative actions or less prominent CTAs.
    /// </summary>
    Secondary,

    /// <summary>
    /// For tertiary actions, toolbars, or navigation.
    /// </summary>
    Ghost,

    /// <summary>
    /// For inline actions, navigation
    /// </summary>
    Link
}

internal static class ButtonVariantExtensions
{
    internal static string Css(this ButtonVariant variant) =>
        variant switch
        {
            ButtonVariant.Default => "bg-primary text-primary-foreground hover:bg-primary/80",
            ButtonVariant.Outline => "border-border bg-background hover:bg-muted hover:text-foreground dark:border-input dark:bg-input/30 dark:hover:bg-input/50",
            ButtonVariant.Secondary => "bg-secondary text-secondary-foreground hover:bg-[color-mix(in_oklch,var(--secondary),var(--foreground)_5%)]",
            ButtonVariant.Ghost => "hover:bg-muted hover:text-foreground dark:hover:bg-muted/50",
            ButtonVariant.Destructive => "bg-destructive/10 text-destructive hover:bg-destructive/20 focus-visible:border-destructive/40 focus-visible:ring-destructive/20 dark:bg-destructive/20 dark:hover:bg-destructive/30 dark:focus-visible:ring-destructive/40",
            ButtonVariant.Link => "text-primary underline-offset-4 hover:underline",
            _ => string.Empty
        };
}