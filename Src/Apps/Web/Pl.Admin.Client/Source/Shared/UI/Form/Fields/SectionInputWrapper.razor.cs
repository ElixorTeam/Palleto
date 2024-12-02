using Microsoft.JSInterop;

namespace Pl.Admin.Client.Source.Shared.UI.Form.Fields;

public sealed partial class SectionInputWrapper : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    [Parameter] public string Label { get; set; } = string.Empty;
    [Parameter] public string Path { get; set; } = string.Empty;
    [Parameter] public string HtmlFor { get; set; } = string.Empty;
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public string ValueToCopy { get; set; } = string.Empty;
    [Parameter] public SectionInputSizeType Size { get; set; } = SectionInputSizeType.Default;
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private async Task SaveToClipboard(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return;

        try
        {
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", value);
            ToastService.ShowInfo(Localizer["ToastCopyToClipboard"]);
        }
        catch
        {
            ToastService.ShowError("Ваш браузер не потдерживает копирование");
        }
    }
}

public enum SectionInputSizeType
{
    Default,
    Large
}