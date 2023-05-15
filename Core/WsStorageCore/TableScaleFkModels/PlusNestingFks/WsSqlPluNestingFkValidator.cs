// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Валидатор таблицы "PLUS_NESTING_FK".
/// </summary>
public sealed class WsSqlPluNestingFkValidator : WsSqlTableValidator<WsSqlPluNestingFkModel>
{
    public WsSqlPluNestingFkValidator() : base(true, true)
    {
        RuleFor(item => item.PluBundle)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluBundleFkValidator());
        RuleFor(item => item.BundleCount)
            .NotNull();
        RuleFor(item => item.Box)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlBoxValidator());
        RuleFor(item => item.WeightMax)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightNom)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightMin)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightMax)
            .GreaterThanOrEqualTo(item => item.WeightMin)
            .GreaterThanOrEqualTo(item => item.WeightNom)
            .When(item => item.WeightMax > 0 && item is { WeightNom: > 0, WeightMin: > 0 });
        RuleFor(item => item.WeightNom)
            .GreaterThanOrEqualTo(item => item.WeightMin)
            .LessThanOrEqualTo(item => item.WeightMax)
            .When(item => item.WeightMax > 0 && item is { WeightNom: > 0, WeightMin: > 0 });
        RuleFor(item => item.WeightMin)
            .LessThanOrEqualTo(item => item.WeightMax)
            .LessThanOrEqualTo(item => item.WeightNom)
            .When(item => item.WeightMax > 0 && item is { WeightNom: > 0, WeightMin: > 0 });
    }
}