namespace Pl.Exchange.Api.App.Features.Plus.Dto;

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class PluDtoValidator : AbstractValidator<PluDto>
{
    public PluDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("UID - обязателен");

        #region Description

        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Наименование - обязательно")
            .MaximumLength(100).WithMessage("Наименование - не должно превышать 100 символов");

        RuleFor(dto => dto.FullName)
            .NotEmpty().WithMessage("Полное наименование - обязательно")
            .MaximumLength(200).WithMessage("Полное наименование - не должно превышать 200 символов");

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Описание - обязательно")
            .MaximumLength(200).WithMessage("Описание - не должно превышать 200 символов");

        #endregion

        #region Itf/Ean

        RuleFor(dto => dto.Ean13)
            .Length(13).WithMessage("Ean13 - должен состоять из 13 символов");

        RuleFor(dto => dto.Itf14)
            .Empty().When(item => item.IsWeight).WithMessage("У весовой ПЛУ Itf14 - должен быть пустой")
            .Length(14).When(item => !item.IsWeight).WithMessage("Itf14 - должен состоять из 14 символов");

        #endregion

        #region Fk

        RuleFor(dto => dto.BoxUid)
            .NotEqual(Guid.Empty).WithMessage("Коробка - обязательна");

        RuleFor(dto => dto.BrandUid)
            .NotEqual(Guid.Empty).WithMessage("Бренд - обязателен");

        // RuleFor(dto => dto.ClipUid)
        //     .NotEqual(Guid.Empty).WithMessage("Клипса - обязательна");
        // RuleFor(dto => dto.BundleUid)
        //     .NotEqual(Guid.Empty).WithMessage("Пакет - обязателен");

        #endregion

        #region Other

        RuleFor(dto => dto.Weight)
            .Must(x => ValidatorUtils.BeValidWeight(x, 0, 2))
            .When(x => !x.IsWeight).WithMessage("Вес - должен быть в [0, 2]")
            .Equal(0).When(x => x.IsWeight).WithMessage("Вес - у весовой ПЛУ должен быть 0");

        RuleFor(dto => dto.Number)
            .Must(number => number is > 0 and < 1000)
            .WithMessage("Номер - должен быть в диапазоне [1, 999]");

        RuleFor(dto => dto.BundleCount)
            .Must(count => count is > 0 and <= 100)
            .WithMessage("Кол-во пакетов должно быть от 1 до 100");

        RuleFor(dto => dto.ShelfLifeDays)
            .Must(days => days is > 0 and < 1000)
            .WithMessage("Срок годности - должны быть в диапазоне [1, 1000]");

        RuleFor(dto => dto.StorageMethod)
            .Must(value => value is "Замороженное" or "Охлаждённое")
            .WithMessage("Способ хранения - должен быть ['Замороженное', 'Охлаждённое']");

        #endregion
    }
}