namespace Pl.Components.Components;

public abstract class PlComponentBase : ComponentBase
{
    /// <summary>
    /// Optional CSS class names. If given, these will be included in the class attribute of the component
    /// </summary>
    [Parameter] public string? Class { get; set; }

    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created element
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public virtual IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Gets the computed CSS classes for the component
    /// </summary>
    protected abstract string CssClass { get; }
}