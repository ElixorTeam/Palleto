using FluentValidation;
using Pl.Tablet.Client.Source.Shared.Utils;

namespace Pl.Tablet.Client.Source.Features.CreatePalletDialog;

public class BatchValidator : AbstractValidator<BatchCreateModel>
{
    public BatchValidator()
    {
        RuleFor(p => p.Plu.Number).Length(4).Matches("^[0-9]*$").WithName("Номер ПЛУ");
        RuleFor(p => p.Plu.Id).NotEqual(Guid.Empty).WithMessage("Такого ПЛУ не существует").WithName("Номер ПЛУ");
        RuleFor(p => p.Date).Must(date => DateTimeUtil.TryParseStringDate(date, out _))
            .WithMessage("Некорректная дата. Ожидается формат: dd, ddMM или ddMMyy.").WithName("Дата");
        RuleFor(p => p.Weight).LessThanOrEqualTo(9999).GreaterThan(0).WithName("Вес");
    }
}