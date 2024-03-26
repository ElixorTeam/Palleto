using System.Linq.Expressions;
using DeviceControl2.Source.Shared.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl2.Source.Widgets.Section.FormFields;

public abstract class SectionFieldBase<TValue> : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool IsCopyable { get; set; }
    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Placeholder { get; set; } = string.Empty;
    [Parameter] public string HtmlId { get; set; } = $"field-{Guid.NewGuid()}";
    [Parameter] public Expression<Func<TValue>>? For { get; set; }
    [Parameter] public string Path { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        Placeholder = string.IsNullOrEmpty(Placeholder) ? Localizer["SectionFormInputEmpty"] : Placeholder;
    }
    
    protected async Task OnValueChanged() => await ValueChanged.InvokeAsync(Value);
}