namespace Pl.Components;

/// <summary>
/// Defines the orientation options for a field.
/// </summary>
public enum FieldOrientation
{
    /// <summary>
    /// Label stacked above control (mobile-first, default).
    /// </summary>
    Vertical,

    /// <summary>
    /// Label beside control with horizontal alignment.
    /// </summary>
    Horizontal,

    /// <summary>
    /// Automatically switches from vertical to horizontal at medium breakpoint.
    /// Uses container queries with @md prefix.
    /// </summary>
    Responsive,

    /// <summary>
    /// Label beside control on the end/right side (flex-row-reverse).
    /// Useful for checkboxes, switches, and radio buttons where the control appears first.
    /// </summary>
    HorizontalEnd,

    /// <summary>
    /// Label stacked below the control (flex-col-reverse).
    /// </summary>
    VerticalEnd
}