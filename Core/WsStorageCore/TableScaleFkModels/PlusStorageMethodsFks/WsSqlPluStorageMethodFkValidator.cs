// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusStorageMethodsFks;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS_FK".
/// </summary>
public sealed class WsSqlPluStorageMethodFkValidator : WsSqlTableValidator<WsSqlPluStorageMethodFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluStorageMethodFkValidator() : base(false, false)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator());
        RuleFor(item => item.Method)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluStorageMethodValidator());
        RuleFor(item => item.Resource)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlTemplateResourceValidator());
    }
}