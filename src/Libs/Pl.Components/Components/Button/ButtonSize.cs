namespace Pl.Components;

public enum ButtonSize
{
    Sm,
    Default,
    Lg,
    Icon,
    Full
}

internal static class ButtonSizeExtensions
{
    internal static string Css(this ButtonSize size) =>
        size switch
        {
            // Old palleto style (not shadcn)
            ButtonSize.Sm => "h-8 px-3 text-xs",
            ButtonSize.Lg => "h-10 px-8",
            ButtonSize.Icon => "size-9",
            ButtonSize.Full => "size-full",
            ButtonSize.Default => "h-9 px-4 py-2",
            _ => string.Empty
        };
}