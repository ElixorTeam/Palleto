namespace Pl.Components;

/// <summary>
/// Defines the size options for a Spinner component.
/// </summary>
public enum SpinnerSize
{
    /// <summary>
    /// Size: 16x16 px
    /// </summary>
    Small,

    /// <summary>
    /// Size: 24x24 px
    /// </summary>
    Default,

    /// <summary>
    /// Size: 40x40 px
    /// </summary>
    Large
}

internal static class SpinnerSizeExtensions
{
    internal static string GetCss(this SpinnerSize size) =>
        size switch
        {
            SpinnerSize.Small => "h-4 w-4",
            SpinnerSize.Large => "h-10 w-10",
            _ => "h-6 w-6"
        };
}