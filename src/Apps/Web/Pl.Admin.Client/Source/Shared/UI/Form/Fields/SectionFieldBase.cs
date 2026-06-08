using System.Linq.Expressions;

namespace Pl.Admin.Client.Source.Shared.UI.Form.Fields;

public abstract class SectionFieldBase<TValue> : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the current value of the textarea.
    /// </summary>
    [Parameter]
    public TValue? Value { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the textarea value changes.
    /// </summary>
    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets an expression that identifies the bound value for EditForm integration.
    /// Automatically provided by <c>@bind-Value</c>.
    /// </summary>
    [Parameter]
    public Expression<Func<TValue?>>? ValueExpression { get; set; }

    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool IsCopyable { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string HtmlId { get; set; } = $"field-{Guid.NewGuid()}";
    [Parameter] public Expression<Func<TValue>>? For { get; set; }
    [Parameter] public string Path { get; set; } = string.Empty;
    [Parameter] public string? ValueToCopy { get; set; }

    protected override void OnInitialized() =>
        Placeholder = string.IsNullOrWhiteSpace(Placeholder) ? Localizer["InputDefaultPlaceholder"] : Placeholder;

    protected async Task OnValueChanged() => await ValueChanged.InvokeAsync(Value);
}