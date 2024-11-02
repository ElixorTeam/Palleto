using FluentValidation;

namespace Pl.Mobile.Client.Source.Features.AuthSuspense;

public class AuthModelValidator : AbstractValidator<AuthFormModel>
{
    public AuthModelValidator()
    {
        RuleFor(p => p.Password).Length(4).Matches("^[0-9]*$").Must(value => ushort.TryParse(value, out _)).WithName("Пароль");
    }
}