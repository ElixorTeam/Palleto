using FluentValidation;
using ScalesDesktop.Source.Shared.Services.Endpoints;

namespace ScalesDesktop.Source.Features.PalletCreate;

public sealed partial class PalletFirstStageForm : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private ArmEndpoints ArmEndpoints { get; set; } = default!;
    [Inject] private PluEndpoints PluEndpoints { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public EventCallback OnCancelAction { get; set; }

    private void OnPluSelected() => FormModel.Nesting =
        FormModel.Plu!.Nestings.Find(x => x.Id == Guid.Empty);
}

public class PalletPluStageFormValidator : AbstractValidator<PalletCreateModel>
{
    public PalletPluStageFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer, IStringLocalizer<ApplicationResources> Localizer)
    {
        RuleFor(item => item.Plu).NotNull().WithName(wsDataLocalizer["ColPlu"]);
        RuleFor(item => item.Nesting).NotNull().WithName(Localizer["ColNestingPerBox"]);;
    }
}