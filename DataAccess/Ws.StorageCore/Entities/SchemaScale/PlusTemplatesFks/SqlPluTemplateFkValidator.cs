using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

public sealed class SqlPluTemplateFkValidator : SqlTableValidator<SqlPluTemplateFkEntity>
{

    public SqlPluTemplateFkValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Template)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlTemplateValidator(isCheckIdentity));
    }
}