using System.Linq.Expressions;
using Pl.Print.Features.Barcodes.Models;
using BarcodeFormatter=Pl.Print.Features.Barcodes.Shared.BarcodeFormatter;

namespace Pl.Print.Features.Barcodes.Utils;

public static partial class BarcodeVarUtils
{
    #region Public

    public static readonly List<BarcodeVarInfo> BarcodeVarInfos = GenerateVariablesInfo();

    public static List<string> BarcodeVarConstantsFormats => ["{0:C}", "({0:C})", "#({0:C})"];

    public static bool TryFormat(object? value, string format, [NotNullWhen(true)] out string? result)
    {
        try
        {
            result = string.Format(BarcodeFormatter.Default, format, value);
            return AllowedChars().IsMatch(result);
        }
        catch
        {
            result = null;
            return false;
        }
    }

    #endregion

    #region Private

    [GeneratedRegex(@"^(?=.*\d)([0-9#]*\([0-9#]+\)[0-9#]*|[0-9#]+)$")]
    private static partial Regex AllowedChars();

    [Pure]
    private static List<BarcodeVarInfo> GenerateVariablesInfo()
    {
        BarcodeBuilder barcodeBuilder = new()
        {
            ArmNumber = 12345,
            ArmCounter = 123456,
            PluGtin = "14607100238871",
            PluEan13 = "4607100238874",
            PluNumber = 301,
            ProductDt = DateTime.Now,
            ExpirationDt = DateTime.Now,
            ExpirationDay = 364,
            Kneading = 105,
            BundleCount = 20,
            WeightNet = 20.135m
        };
        return
        [
            CreateVariable(() => barcodeBuilder.ArmNumber, "{0:D5}"),
            CreateVariable(() => barcodeBuilder.ArmCounter, "{0:D6}"),
            CreateVariable(() => barcodeBuilder.PluNumber, "{0:D3}"),
            CreateVariable(() => barcodeBuilder.PluGtin, "{0:D14}"),
            CreateVariable(() => barcodeBuilder.PluEan13, "{0:D13}"),
            CreateVariable(() => barcodeBuilder.Kneading, "{0:D3}"),
            CreateVariable(() => barcodeBuilder.WeightNet, "{0:D5}"),
            CreateVariable(() => barcodeBuilder.BundleCount, "{0:D2}"),
            CreateVariable(() => barcodeBuilder.ExpirationDay, "{0:D3}"),
            CreateVariable(() => barcodeBuilder.ProductDt, "{0:yyMMdd}", true),
            CreateVariable(() => barcodeBuilder.ExpirationDt, "{0:yyMMdd}", true)
        ];
    }

    [Pure]
    private static BarcodeVarInfo CreateVariable<T>(Expression<Func<T>> propertyExpression, string mask, bool isRepeatable = false)
    {
        List<Type> typesWhiteList = [typeof(uint), typeof(ushort), typeof(string), typeof(decimal), typeof(DateTime)];

        if (propertyExpression.Body is not MemberExpression memberExpression)
            throw new ArgumentException("Barcode variable expression is not a property");

        string propertyName = memberExpression.Member.Name;

        T propertyValue = propertyExpression.Compile().Invoke()!;

        if (typesWhiteList.All(i => i != typeof(T)))
            throw new ArgumentException($"Barcode variable type not supported: {typeof(T)}");

        return new(typeof(T), propertyName, mask, propertyValue, isRepeatable);
    }

    #endregion
}