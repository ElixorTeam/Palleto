namespace Pl.Components;

/// <summary>
/// Defines the position where toasts are displayed on the screen.
/// </summary>
public enum ToastPosition
{
    /// <summary>
    /// Top-right corner of the viewport.
    /// </summary>
    TopRight,

    /// <summary>
    /// Top-left corner of the viewport.
    /// </summary>
    TopLeft,

    /// <summary>
    /// Top-center of the viewport.
    /// </summary>
    TopCenter,

    /// <summary>
    /// Bottom-right corner of the viewport (default).
    /// </summary>
    BottomRight,

    /// <summary>
    /// Bottom-left corner of the viewport.
    /// </summary>
    BottomLeft,

    /// <summary>
    /// Bottom-center of the viewport.
    /// </summary>
    BottomCenter
}

internal static class ToastPositionExtensions
{
    internal static string Css(this ToastPosition pos) =>
        pos switch
        {
            ToastPosition.TopRight => "top-0 right-0",
            ToastPosition.TopLeft => "top-0 left-0",
            ToastPosition.TopCenter => "top-0 left-1/2 -translate-x-1/2",
            ToastPosition.BottomRight => "bottom-0 right-0",
            ToastPosition.BottomLeft => "bottom-0 left-0",
            ToastPosition.BottomCenter => "bottom-0 left-1/2 -translate-x-1/2",
            _ => string.Empty
        };
}
