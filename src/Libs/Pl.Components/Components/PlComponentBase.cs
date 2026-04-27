namespace Pl.Components.Components;

public abstract class LmsComponentBase : ComponentBase
{
    /// <summary>
    /// Optional CSS class names. If given, these will be included in the class attribute of the component.
    /// </summary>
    [Parameter] public string Class { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public virtual IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
}