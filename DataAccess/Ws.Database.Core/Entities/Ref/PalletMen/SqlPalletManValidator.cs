﻿using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PalletMen;

public sealed class SqlPalletManValidator : SqlTableValidator<PalletManEntity>
{
    public SqlPalletManValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Uid1C)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Surname)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Patronymic)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Password)
            .NotNull().NotEmpty().Length(4).Matches("^[0-9]+$").WithMessage("Пароль должен содержать только цифры.");
    }
}