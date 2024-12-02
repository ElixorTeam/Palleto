using System.Collections.ObjectModel;

namespace Pl.Print.Features.Barcodes.Shared;

internal partial class BarcodeFormatter : ICustomFormatter, IFormatProvider
{
    #region Private

    [GeneratedRegex(@"^D(\d+)$")]
    private static partial Regex IsDFormat();

    [GeneratedRegex(@"^(?!.*(\b(?:yy|MM|dd|HH|ss|mm)\b).*\1)(?=(?:yy|MM|dd|HH|ss|mm)+$).+$")]
    private static partial Regex IsDateTimeFormat();

    private static readonly ReadOnlyCollection<Type> SupportTypes =
        new([typeof(uint), typeof(ushort), typeof(decimal), typeof(string)]);

    #endregion

    #region Public

    public static readonly BarcodeFormatter Default = new();

    public object? GetFormat(Type? formatType) => formatType == typeof(ICustomFormatter) ? this : null;

    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        if (arg == null || string.IsNullOrWhiteSpace(format))
            throw new FormatException();

        switch (arg)
        {
            case string str when format.Equals("C", StringComparison.OrdinalIgnoreCase):
            {
                if (!str.IsDigitsOnly())
                    throw new FormatException();
                return str;
            }
            case DateTime time when IsDateTimeFormat().Match(format).Success:
            {
                string result = time.ToString(format, CultureInfo.CurrentCulture);
                return result.IsDigitsOnly() ? result : throw new FormatException(result);
            }
        }

        if (!SupportTypes.Contains(arg.GetType()))
            throw new FormatException("is not in the correct format.");

        Match match = IsDFormat().Match(format);

        if (!match.Success)
            throw new FormatException($"{arg} is not a valid format.");

        int length = int.Parse(match.Groups[1].Value);

        string numberStr;

        if (arg is decimal value)
            numberStr = new(value.ToString("00.000").Where(char.IsDigit).ToArray());
        else
            numberStr = Convert.ToString(arg) ??
                        throw new FormatException($"Lenght of  {arg.GetType()} not suppoerted for BarcodeFormatter");

        string digits = new(numberStr.Where(char.IsDigit).ToArray());

        if (numberStr.Length != digits.Length)
            throw new FormatException(nameof(format));

        if (length > 24 || length < digits.Length)
            throw new FormatException(nameof(format));

        string digitsNew = digits.PadLeft(length, '0');

        return digitsNew.Length >= digits.Length ? digitsNew :
            throw new FormatException($"{digits}:{format} is not in the correct format.");
    }

    #endregion
}