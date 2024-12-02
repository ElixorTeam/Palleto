using FluentValidation.TestHelper;
using Pl.Print.Features.Barcodes;
using Pl.Print.Features.Barcodes.Models;

namespace Pl.Print.Tests.Shared;

public class BarcodeVarValidatorTests
{
    [Theory]
    [MemberData(nameof(TestCases.BarcodeVariablesSuccessTestCases), MemberType = typeof(TestCases))]
    public void Test_Success_BarcodeVar(string property, string formatStr)
    {
        // Arrange
        BarcodeVar barcodeVar = new(property, formatStr);

        TestValidationResult<BarcodeVar> isValid = new BarcodeVarValidator().TestValidate(barcodeVar);

        isValid.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestCases.BarcodeVariablesInvalidTestCases), MemberType = typeof(TestCases))]
    public void Test_Invalid_BarcodeVar(string property, string formatStr)
    {
        // Arrange
        BarcodeVar barcodeVar = new(property, formatStr);

        TestValidationResult<BarcodeVar> isValid = new BarcodeVarValidator().TestValidate(barcodeVar);

        isValid.IsValid.Should().BeFalse();
    }
}

#region Test cases

file static class TestCases
{
    public static IEnumerable<object[]> BarcodeVariablesSuccessTestCases()
    {
        yield return [nameof(BarcodeBuilder.PluGtin), "{0:D14}"];
        yield return [nameof(BarcodeBuilder.Kneading), "{0:D3}"];
        yield return [nameof(BarcodeBuilder.PluEan13), "{0:D13}"];
        yield return [nameof(BarcodeBuilder.WeightNet), "{0:D5}"];
        yield return [nameof(BarcodeBuilder.ArmNumber), "{0:D5}"];
        yield return [nameof(BarcodeBuilder.ArmCounter), "{0:D6}"];
        yield return [nameof(BarcodeBuilder.BundleCount), "{0:D2}"];
        yield return [nameof(BarcodeBuilder.ExpirationDay), "{0:D3}"];

        yield return [nameof(BarcodeBuilder.ProductDt), "{0:yyMM}"];
        yield return [nameof(BarcodeBuilder.ExpirationDt), "{0:yyMMdd}"];

        yield return ["01", "{0:C}"];
        yield return ["012", "#({0:C})"];
        yield return ["01212", "({0:C})"];
    }

    public static IEnumerable<object[]> BarcodeVariablesInvalidTestCases()
    {
        yield return [nameof(BarcodeBuilder.PluGtin), "{0:D13}"];
        yield return [nameof(BarcodeBuilder.Kneading), "{0:D2}"];
        yield return [nameof(BarcodeBuilder.PluEan13), "{0:D12}"];
        yield return [nameof(BarcodeBuilder.WeightNet), "{0:D2}"];
        yield return [nameof(BarcodeBuilder.ArmNumber), "{0:D4}"];
        yield return [nameof(BarcodeBuilder.ArmCounter), "{0:D5}"];
        yield return [nameof(BarcodeBuilder.BundleCount), "{0:D1}"];
        yield return [nameof(BarcodeBuilder.ExpirationDay), "{0:D2}"];

        yield return [nameof(BarcodeBuilder.ProductDt), "{0:dd MMMM yyyy}"];
        yield return [nameof(BarcodeBuilder.ExpirationDt), "{0:yyyy-MM-dd}"];
        yield return [nameof(BarcodeBuilder.ExpirationDt), "{0:yyyy-MM-dd HH:mm:ss}"];

        yield return [nameof(BarcodeBuilder.PluGtin), "{0:C}"];
        yield return ["test", "{0:C}"];
        yield return ["(12321)", "({0:C})"];
        yield return ["test123", "#({0:C})"];
    }
}

#endregion