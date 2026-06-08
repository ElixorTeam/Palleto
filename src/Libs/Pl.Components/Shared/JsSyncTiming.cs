namespace Pl.Components.Components;

public enum JsSyncTiming
{
    /// <summary>
    /// Fires <c>ValueChanged</c> on every keystroke, batched via requestAnimationFrame in JavaScript.
    /// </summary>
    Immediate,

    /// <summary>
    /// Fires <c>ValueChanged</c> only when the element loses focus (default).
    /// For single-line inputs, also fires when the user presses Enter.
    /// Zero C# interop calls during typing.
    /// </summary>
    OnBlur,

    /// <summary>
    /// Fires <c>ValueChanged</c> after typing pauses for <c>DebounceInterval</c> milliseconds.
    /// Debounce timer runs in JavaScript.
    /// </summary>
    Debounced
}

internal static class JsSyncTimingExtensions
{
    internal static string ToJsValue(this JsSyncTiming timing) =>
        timing switch
        {
            JsSyncTiming.OnBlur => "onblur",
            JsSyncTiming.Debounced => "debounced",
            JsSyncTiming.Immediate => "immediate",
            _ => "onchange"
        };
}