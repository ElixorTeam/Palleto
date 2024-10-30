using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace DeviceControl.Source.Pages.References1C.Plus;

public class PluValidator : AbstractValidator<PluDto>
{
    public PluValidator()
    {
        RuleFor(item => item.Template)
            .NotNull()
            .WithMessage("Шаблон не задан");

        RuleFor(item => item.StorageMethod)
            .Must(value => value is "Замороженное" or "Охлаждённое")
            .WithMessage("Способ хранения - должен быть ['Замороженное', 'Охлаждённое']");
    }
}