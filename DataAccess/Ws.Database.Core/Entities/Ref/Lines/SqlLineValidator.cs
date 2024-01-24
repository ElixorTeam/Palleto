using Ws.Database.Core.Entities.Ref.Printers;
using Ws.Database.Core.Entities.Ref.Warehouses;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineValidator : SqlTableValidator<LineEntity>
{
    public SqlLineValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.PcName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Number)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(10000)
            .LessThanOrEqualTo(99999);
        RuleFor(item => item.Warehouse)
            .SetValidator(new SqlWarehouseValidator(isCheckIdentity));
        RuleFor(item => item.Printer)
            .SetValidator(new SqlPrinterValidator(isCheckIdentity)!);
    }
}