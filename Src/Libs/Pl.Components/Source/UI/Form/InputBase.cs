using Microsoft.AspNetCore.Components;

namespace Pl.Components.Source.UI.Form;

public abstract class InputBase<TValue> : PlComponentBase
{
    [Parameter] public override string? Id { get; set; }

    /// <summary>
    /// The current value of the input field.
    /// </summary>
    [Parameter] public virtual TValue? Value { get; set; }

    /// <summary>
    /// Callback to notify when the value of the input field changes.
    /// </summary>
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    /// Indicates whether the input field is disabled.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Specifies whether the input field should automatically receive focus when loads.
    /// </summary>
    [Parameter] public bool AutoFocus { get; set; }

    /// <summary>
    /// The placeholder text to display when the input field is empty.
    /// </summary>
    [Parameter] public string Placeholder { get; set; } = string.Empty;

    /// <summary>
    /// Specifies whether the input field is read-only.
    /// </summary>
    [Parameter] public bool ReadOnly { get; set; }

    protected async Task OnValueChanged() => await ValueChanged.InvokeAsync(Value);
}