namespace Pl.Exchange.Api.App.Features.Characteristics.Impl.Models;

// ReSharper disable once ClassNeverInstantiated.Global
internal sealed partial class GroupedCharacteristicValidator : AbstractValidator<GroupedCharacteristic>
{
    [GeneratedRegex("^Кор\\((\\d+)\\)$")]
    private static partial Regex MyRegex();

    public GroupedCharacteristicValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("UID - обязателен");

        RuleFor(dto => dto.BundleCount)
            .Must(count => count is > 0 and <= 100)
            .WithMessage("Кол-во пакетов - должно быть от 1 до 100");

        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Наименование - обязательно")
            .MaximumLength(64).WithMessage("Наименование - не должно превышать 64 символа")
            .Must((dto, name) => IsValidBundleName(name, dto.BundleCount))
            .WithMessage("Наименование - число в Кор() должно совпадать с количеством пакетов");

        RuleFor(dto => dto.BoxUid)
            .NotEqual(Guid.Empty).WithMessage("Короб - обязателен");
    }

    private static bool IsValidBundleName(string name, int bundleCount)
    {
        Match match = MyRegex().Match(name);
        if (!match.Success)
            return true;

        if (!int.TryParse(match.Groups[1].Value, out int number))
            return true;

        return number == bundleCount;
    }
}