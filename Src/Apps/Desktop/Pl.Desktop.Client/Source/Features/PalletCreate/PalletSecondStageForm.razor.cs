using FluentValidation;
using Pl.Desktop.Client.Source.Shared.Localization;

namespace Pl.Desktop.Client.Source.Features.PalletCreate;

public sealed partial class PalletSecondStageForm : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public EventCallback OnCancelAction { get; set; }

    private void SetCurrentDate() => FormModel.CreateDt = DateTime.Now;
}

public class PalletSecondStageFormValidator : AbstractValidator<PalletCreateModel>
{
    public PalletSecondStageFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.PalletWeight).GreaterThan(0).LessThanOrEqualTo(99).WithName(wsDataLocalizer["ColTrayWeight"]);
        RuleFor(item => item.Count).GreaterThanOrEqualTo(1).LessThanOrEqualTo(240).WithName(wsDataLocalizer["ColCount"]);
        RuleFor(item => item.Kneading).GreaterThanOrEqualTo((short)1).LessThanOrEqualTo((short)999).WithName(wsDataLocalizer["ColKneading"]);
    }
}