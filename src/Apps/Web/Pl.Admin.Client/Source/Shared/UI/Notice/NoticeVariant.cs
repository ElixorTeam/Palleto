namespace Pl.Admin.Client.Source.Shared.UI.Notice;

public enum NoticeVariant
{
    Warning,
    Danger,
}

internal static class NoticeVariantExtensions
{
    internal static string ContainerCss(this NoticeVariant variant) =>
        variant switch
        {
            NoticeVariant.Danger => "border-alert-danger/40 bg-alert-danger-bg",
            _ => "border-alert-warning/40 bg-alert-warning-bg",
        };

    internal static string IconContainerCss(this NoticeVariant variant) =>
        variant switch
        {
            NoticeVariant.Danger => "border-alert-danger/30 bg-background/80 text-alert-danger",
            _ => "border-alert-warning/30 bg-background/80 text-alert-warning",
        };

    internal static string DefaultIconName(this NoticeVariant variant) =>
        variant switch
        {
            NoticeVariant.Warning => "exclamation-triangle",
            NoticeVariant.Danger => "no-symbol",
            _ => "exclamation-triangle",
        };
}
