using Microsoft.AspNetCore.Components;
using TailwindMerge;

namespace Pl.Components.Source.UI;

public abstract class PlComponentBase : ComponentBase
{
    [Inject] private TwMerge TwMerge { get; set; } = default!;

    /// <summary>
    /// The value will be used as the HTML global id attribute.
    /// </summary>
    [Parameter] public virtual string? Id { get; set; }

    /// <summary>
    /// Optional CSS class names. If given, these will be included in the class attribute of the component.
    /// </summary>
    [Parameter] public string Class { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public virtual IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Merges a list of Tailwind CSS classes, resolving conflicting styles by prioritizing the last class in the list.
    /// </summary>
    protected string? ClassMerge(params string?[] classes) => TwMerge.Merge(classes);
}