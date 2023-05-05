// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusTemplatesFks;

/// <summary>
/// Table validation "PLUS_TEMPLATES_FK".
/// </summary>
public sealed class WsSqlPluTemplateFkValidator : WsSqlTableValidator<WsSqlPluTemplateFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluTemplateFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator());
        RuleFor(item => item.Template)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlTemplateValidator());
    }
}