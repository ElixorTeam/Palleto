namespace Pl.Components.Components;

/// <summary>
/// Defines the HTML button type attribute value.
/// </summary>
public enum ButtonType
{
    /// <summary>
    /// Type="submit"
    /// Submits the form data to the server when clicked.
    /// </summary>
    Submit,

    /// <summary>
    /// Type="reset"
    /// Resets all form controls to their initial values when clicked.
    /// </summary>
    Reset,

    /// <summary>
    /// Type="button"
    /// Does not submit or reset the form.
    /// Default for buttons outside forms or with custom click handlers.
    /// </summary>
    Button
}