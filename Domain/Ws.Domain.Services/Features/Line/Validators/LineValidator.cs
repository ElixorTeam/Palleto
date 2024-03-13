using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Line.Validators;

internal abstract class LineValidator : AbstractValidator<LineEntity>
{
    protected LineValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.PcName)
            .NotEmpty();
        RuleFor(item => item.Number)
            .NotEmpty()
            .GreaterThanOrEqualTo(10000)
            .LessThanOrEqualTo(99999);
        // RuleFor(item => item.Warehouse)
        //     .SetValidator(new SqlWarehouseValidator(isCheckIdentity));
        // RuleFor(item => item.Printer)
        //     .SetValidator(new SqlPrinterValidator(isCheckIdentity)!);
    }
}