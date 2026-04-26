namespace Pl.Admin.Models.Features.References1C.Plus.Commands.Update;

public sealed class PluUpdateValidator : AbstractValidator<PluUpdateDto>
{
    public PluUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.TemplateId).NotEmpty().WithName(wsDataLocalizer["ColTemplate"]);
    }
}