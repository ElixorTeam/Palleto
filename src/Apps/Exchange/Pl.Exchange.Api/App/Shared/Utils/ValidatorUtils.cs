namespace Pl.Exchange.Api.App.Shared.Utils;

internal static class ValidatorUtils
{
    public static bool BeValidWeightDefault(decimal number) => BeValidWeight(number);

    public static bool BeValidWeight(decimal number, int min = 0, int max = 1)
    {
        if (number < min && number > max) return false;

        string numberString = number.ToString(CultureInfo.InvariantCulture);
        string[] parts = numberString.Split('.');
        string integerPart = parts[0];
        string decimalPart = parts.Length > 1 ? parts[1] : string.Empty;
        return integerPart.Length <= 1 && decimalPart.Length <= 3;
    }
}