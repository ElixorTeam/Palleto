namespace Pl.Components;

/// <summary>
/// Defines the visual style variant for a Toast component.
/// </summary>
public enum ToastVariant
{
    /// <summary>
    /// Default toast style for general messages. No automatic icon.
    /// </summary>
    Default,

    /// <summary>
    /// Success toast style with green accent and circle-check icon.
    /// </summary>
    Success,

    /// <summary>
    /// Informational toast style with blue accent and info icon.
    /// </summary>
    Info,

    /// <summary>
    /// Warning toast style with amber accent and triangle-alert icon.
    /// </summary>
    Warning,

    /// <summary>
    /// Destructive toast style for errors with red accent and circle-x icon.
    /// </summary>
    Destructive
}

internal static class ToastVariantExtensions
{
    internal static string IconName(this ToastVariant var) =>
        var switch
        {
            ToastVariant.Success => "check-circle",
            ToastVariant.Info => "information-circle",
            ToastVariant.Warning => "exclamation-triangle",
            ToastVariant.Destructive => "no-symbol",
            _ => string.Empty
        };

    internal static string Css(this ToastVariant var) =>
        var switch
        {
            ToastVariant.Success => "border-alert-success/30 bg-alert-success-bg text-alert-success-foreground",
            ToastVariant.Info => "border-alert-info/30 bg-alert-info-bg text-alert-info-foreground",
            ToastVariant.Warning => "border-alert-warning/30 bg-alert-warning-bg text-alert-warning-foreground",
            ToastVariant.Destructive => "destructive group border-destructive bg-destructive text-destructive-foreground",
            _ => "border bg-background text-foreground"
        };

    internal static string IconCss(this ToastVariant var) =>
        var switch
        {
            ToastVariant.Success => "text-alert-success",
            ToastVariant.Info => "text-alert-info",
            ToastVariant.Warning => "text-alert-warning",
            ToastVariant.Destructive => "text-destructive-foreground",
            _ => string.Empty
        };
}