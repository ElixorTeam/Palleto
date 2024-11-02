using FluentValidation;

namespace Pl.Tablet.Client.Source.Features.CreatePalletDialog;

public class PluStageValidator : AbstractValidator<PalletCreateModel>
{
    public PluStageValidator()
    {
        RuleFor(p => p.DefaultPlu.Id).NotEqual(Guid.Empty).When(p => p.Mono).WithMessage("Такого ПЛУ не существует").WithName("Номер ПЛУ");
        RuleFor(p => p.DefaultPlu.Number).Length(4).Matches("^[0-9]*$").When(p => p.Mono).WithName("Номер ПЛУ");
        RuleFor(p => p.DefaultPlu.Number).Empty().When(p => !p.Mono).WithName("Номер ПЛУ");
    }
}