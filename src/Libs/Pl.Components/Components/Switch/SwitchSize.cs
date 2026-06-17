namespace Pl.Components;

/// <summary>
/// Size variants for the Switch component
/// </summary>
public enum SwitchSize
{
    /// <summary>
    /// Size: h-5 w-9, thumb h-4 w-4
    /// </summary>
    Small,

    /// <summary>
    /// Size: h-6 w-11, thumb h-5 w-5
    /// </summary>
    Medium,

    /// <summary>
    /// Size: h-7 w-14, thumb h-6 w-6
    /// </summary>
    Large
}

internal static class SwitchSizeExtensions
{
    internal static string SwitchCss(this SwitchSize size) =>
        size switch
        {
            SwitchSize.Small => "h-5 w-9",
            SwitchSize.Medium => "h-6 w-11",
            SwitchSize.Large => "h-7 w-14",
            _ => "h-6 w-11"
        };

    internal static string ThumbCss(this SwitchSize size, bool value) =>
        CssUtil.Cn(
            size switch
            {
                SwitchSize.Small  => "h-4 w-4",
                SwitchSize.Medium => "h-5 w-5",
                SwitchSize.Large  => "h-6 w-6",
                _                 => "h-5 w-5"
            },
            value ? size switch
                {
                    SwitchSize.Small  => "translate-x-4",
                    SwitchSize.Medium => "translate-x-5",
                    SwitchSize.Large  => "translate-x-7",
                    _                 => "translate-x-5"
                }
                : "translate-x-0"
        );
}