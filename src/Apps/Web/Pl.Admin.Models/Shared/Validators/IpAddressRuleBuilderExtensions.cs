using FluentValidation;

namespace Pl.Admin.Models.Shared.Validators;

public static class IpAddressRuleBuilderExtensions
{
    /// <summary>
    /// Treats <see cref="IPAddress.None"/> as empty and uses the same localized
    /// message as <c>NotEmpty()</c>.
    /// </summary>
    public static IRuleBuilderOptions<T, IPAddress> NotEmptyIp<T>(this IRuleBuilder<T, IPAddress> ruleBuilder) =>
        ruleBuilder
            .Must(ip => ip is not null && !IPAddress.None.Equals(ip))
            .WithMessage(_ => ValidatorOptions.Global.LanguageManager.GetString("NotEmptyValidator"));
}
