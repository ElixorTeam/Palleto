using Ws.StorageCore.Entities.SchemaRef.Lines;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace Ws.StorageCore.Entities.SchemaRef.PlusLines;

public sealed class SqlPluLineValidator : SqlTableValidator<SqlPluLineEntity>
{
    public SqlPluLineValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Line)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlLineValidator(isCheckIdentity));
    }
}