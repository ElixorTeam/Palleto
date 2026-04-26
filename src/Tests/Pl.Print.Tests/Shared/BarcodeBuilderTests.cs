using Pl.Print.Features.Barcodes;
using Pl.Print.Features.Barcodes.Models;
using Pl.Print.Shared.ValueTypes;

namespace Pl.Print.Tests.Shared;

public class BarcodeBuilderTests
{
    [Theory]
    [MemberData(nameof(TestCases.GetBarcodeVariablesTestCases), MemberType = typeof(TestCases))]
    public void Test_Barcodes_Build(List<BarcodeVar> variables, string expectedBarcode)
    {
        string barcodeClean = string.Concat(expectedBarcode.Where(char.IsDigit));
        string barcodeZpl = string.Concat(expectedBarcode.Where("0123456789#".Contains));
        string barcodeFriendly = string.Concat(expectedBarcode.Where("0123456789()".Contains));

        BarcodeResult barcodeResult = TestCases.BarcodeBuilder.Build(variables);

        barcodeResult.Zpl.Should().Be(barcodeZpl);
        barcodeResult.Clean.Should().Be(barcodeClean);
        barcodeResult.Friendly.Should().Be(barcodeFriendly);
    }
}

#region Test cases

file static class TestCases
{
    public static readonly BarcodeBuilder BarcodeBuilder = new()
    {
        ArmNumber = 96446,
        ArmCounter = 507602,
        PluGtin = "14607100238871",
        PluEan13 = "4607100238874",
        PluNumber = 331,
        ProductDt = new(2024, 9, 19, 12, 55, 0),
        ExpirationDt = new(2024, 10, 19, 12, 55, 0),
        ExpirationDay = 56,
        Kneading = 332,
        BundleCount = 20,
        WeightNet = 21.5m
    };

    public static IEnumerable<object[]> GetBarcodeVariablesTestCases()
    {
        yield return
        [
            new List<BarcodeVar>
            {
                new("01", "({0:C})"),
                new(nameof(BarcodeBuilder.PluGtin), "{0:D14}"),
                new("3103", "({0:C})"),
                new(nameof(BarcodeBuilder.WeightNet), "{0:D6}"),
                new("11", "({0:C})"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:yyMMdd}"),
                new("17", "({0:C})"),
                new(nameof(BarcodeBuilder.ExpirationDt), "{0:yyMMdd}"),
                new("37", "({0:C})"),
                new(nameof(BarcodeBuilder.BundleCount), "{0:D2}"),
                new("10", "#({0:C})"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:MMdd}"),
                new("250", "#({0:C})"),
                new(nameof(BarcodeBuilder.ArmNumber), "{0:D5}"),
                new(nameof(BarcodeBuilder.ArmCounter), "{0:D6}"),
            },
            "(01)14607100238871(3103)021500(11)240919(17)241019(37)20#(10)0919#(250)96446507602"
        ];

        yield return
        [
            new List<BarcodeVar>
            {
                new("233", "{0:C}"),
                new(nameof(BarcodeBuilder.ArmNumber), "{0:D5}"),
                new(nameof(BarcodeBuilder.BundleCount), "{0:D2}"),
                new(nameof(BarcodeBuilder.ArmCounter), "{0:D6}"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:yyMMddHHmmss}"),
                new(nameof(BarcodeBuilder.PluNumber), "{0:D3}"),
                new(nameof(BarcodeBuilder.Kneading), "{0:D8}"),
            },
            "233964462050760224091912550033100000332"
        ];

        yield return
        [
            new List<BarcodeVar>
            {
                new("233", "{0:C}"),
                new(nameof(BarcodeBuilder.ArmNumber), "{0:D5}"),
                new(nameof(BarcodeBuilder.BundleCount), "{0:D2}"),
                new(nameof(BarcodeBuilder.ArmCounter), "{0:D6}"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:yyMMddHHmmss}"),
                new(nameof(BarcodeBuilder.PluNumber), "{0:D3}"),
                new(nameof(BarcodeBuilder.Kneading), "{0:D8}"),
            },
            "233964462050760224091912550033100000332"
        ];

        yield return
        [
            new List<BarcodeVar>
            {
                new("234", "{0:C}"),
                new(nameof(BarcodeBuilder.ArmNumber), "{0:D5}"),
                new(nameof(BarcodeBuilder.ArmCounter), "{0:D6}"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:yyMMdd}"),
            },
            "23496446507602240919"
        ];
        yield return
        [
            new List<BarcodeVar>
            {
                new("298", "{0:C}"),
                new(nameof(BarcodeBuilder.ArmNumber), "{0:D5}"),
                new(nameof(BarcodeBuilder.ArmCounter), "{0:D8}"),
                new(nameof(BarcodeBuilder.ProductDt), "{0:yyMMddHHmmss}"),
                new(nameof(BarcodeBuilder.PluNumber), "{0:D3}"),
                new(nameof(BarcodeBuilder.WeightNet), "{0:D5}"),
                new(nameof(BarcodeBuilder.Kneading), "{0:D3}"),
            },
            "298964460050760224091912550033121500332"
        ];
    }
}

#endregion