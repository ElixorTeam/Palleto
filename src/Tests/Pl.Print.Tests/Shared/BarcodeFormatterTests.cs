using BarcodeVarUtils=Pl.Print.Features.Barcodes.Utils.BarcodeVarUtils;

namespace Pl.Print.Tests.Shared;

public class BarcodeFormatterTests
{
    [Theory]
    [MemberData(nameof(TestCases.FormatSuccessTestCases), MemberType = typeof(TestCases))]
    public void Test_Success_Format(object? obj, string mask, string expected)
    {
        bool isSuccess = BarcodeVarUtils.TryFormat(obj, mask, out string? result);
        isSuccess.Should().BeTrue();
        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TestCases.FormatInvalidTestCases), MemberType = typeof(TestCases))]
    public void Test_Failed_Format(object? obj, string mask)
    {
        bool isSuccess = BarcodeVarUtils.TryFormat(obj, mask, out string? result);
        isSuccess.Should().BeFalse();
        result.Should().BeNull();
    }
}

#region Test cases

file static class TestCases
{
    public static IEnumerable<object[]> FormatSuccessTestCases()
    {
        yield return ["01", "#({0:C})", "#(01)"];
        yield return [1m, "{0:D6}", "001000"];
        yield return [172.2m, "{0:D6}", "172200"];
        yield return [72m, "{0:D5}", "72000"];
        yield return [1.23m, "{0:D5}", "01230"];
        yield return [(ushort)5, "{0:D5}", "00005"];
        yield return [(uint)10, "{0:D7}", "0000010"];
        yield return [(uint)10, "{0:D7}", "0000010"];
        yield return ["4607100238874", "{0:D13}", "4607100238874"];
        yield return ["14607100238871", "{0:D14}", "14607100238871"];
        yield return ["14607100238871", "{0:D20}", "00000014607100238871"];
        yield return [DateTime.Now, "{0:mmyyMMdd}", DateTime.Now.ToString("mmyyMMdd")];
        yield return [DateTime.Now, "{0:yyMMddHHmmss}", DateTime.Now.ToString("yyMMddHHmmss")];
    }

    public static IEnumerable<object[]> FormatInvalidTestCases()
    {
        yield return ["(46071002388741)", "{0:С}"];
        yield return ["46071002388741", "{0:D5}"];
        yield return [172.2m, "{0:D5}"];
        yield return [-10, "{0:D7}"];
        yield return [null!, "{0:D7}"];
        yield return [DateTime.Now, "{0:T}"];
        yield return [DateTime.Now, "{0:MMMM}"];
        yield return [DateTime.Now, "{0:M}"];
    }
}

#endregion