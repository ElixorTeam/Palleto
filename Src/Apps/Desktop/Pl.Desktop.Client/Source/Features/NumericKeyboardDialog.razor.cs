using Pl.Desktop.Client.Source.Shared.Localization;

namespace Pl.Desktop.Client.Source.Features;

public sealed partial class NumericKeyboardDialog : ComponentBase, IDialogContentComponent<NumericKeyboardDialogContent>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;
    [Parameter] public int MaxDigitCount { get; set; } = 3;
    [Parameter] public NumericKeyboardDialogContent Content { get; set; } = new();

    private string Number { get; set; } = string.Empty;
    private List<CalculatorControl> CalculatorControls { get; set; } = [];

    protected override void OnInitialized()
    {
        Number = Content.Kneading.ToString();
        CalculatorControls = GetControls();
    }

    private List<CalculatorControl> GetControls() => [
        new() { Title = "1", CalculatorAction = () => SetNumber(1) },
        new() { Title = "2", CalculatorAction = () => SetNumber(2) },
        new() { Title = "3", CalculatorAction = () => SetNumber(3) },
        new() { Title = "4", CalculatorAction = () => SetNumber(4) },
        new() { Title = "5", CalculatorAction = () => SetNumber(5) },
        new() { Title = "6", CalculatorAction = () => SetNumber(6) },
        new() { Title = "7", CalculatorAction = () => SetNumber(7) },
        new() { Title = "8", CalculatorAction = () => SetNumber(8) },
        new() { Title = "9", CalculatorAction = () => SetNumber(9) },
        new() { Title = "C", CalculatorAction = ClearNumber },
        new() { Title = "0", CalculatorAction = () => SetNumber(0) },
        new() { Title = Localizer["BtnEnter"], CalculatorAction = SubmitInput }
    ];

    private void HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Code is "Enter" or "NumpadEnter")
            SubmitInput();
    }

    private async void SubmitInput()
    {
        _ = ushort.TryParse(Number, out ushort resultInt);
        await Dialog.CloseAsync(ushort.Max(resultInt, 1));
    }

    private void SetNumber(int newDigit)
    {
        if (Number.Length >= MaxDigitCount) return;
        Number += newDigit;
        StateHasChanged();
    }

    private void ClearNumber()
    {
        Number = string.Empty;
        StateHasChanged();
    }
}

internal class CalculatorControl
{
    public string Title { get; init; } = string.Empty;
    public Action CalculatorAction { get; init; } = () => { };
}

public record NumericKeyboardDialogContent
{
    public int Kneading { get; init; }
}