using Ws.Shared.ValueTypes;

namespace Ws.Shared.Tests.ValueTypes;

public class ValueTypesTests
{
    [Theory]
    [MemberData(nameof(TestCases.GetWorkShiftTestCases), MemberType = typeof(TestCases))]
    public void Test_WorkShift(DateTime testDate, DateTime expectedStart, DateTime expectedEnd)
    {
        WorkShift workShift = new(testDate);

        workShift.Start.Should().Be(expectedStart);
        workShift.End.Should().Be(expectedEnd);
    }
}

#region Test cases

file static class TestCases
{
    public static IEnumerable<object[]> GetWorkShiftTestCases()
    {
        yield return
        [
            new DateTime(2023, 10, 30, 15, 0, 0),
            new DateTime(2023, 10, 30, 8, 0, 0),
            new DateTime(2023, 10, 31, 8, 0, 0)
        ];
        yield return
        [
            new DateTime(2023, 10, 30, 5, 0, 0),
            new DateTime(2023, 10, 29, 8, 0, 0),
            new DateTime(2023, 10, 30, 8, 0, 0)
        ];
        yield return
        [
            new DateTime(2023, 10, 30, 22, 0, 0),
            new DateTime(2023, 10, 30, 8, 0, 0),
            new DateTime(2023, 10, 31, 8, 0, 0)
        ];
        yield return
        [
            new DateTime(2023, 10, 30, 8, 0, 0),
            new DateTime(2023, 10, 30, 8, 0, 0),
            new DateTime(2023, 10, 31, 8, 0, 0)
        ];
        yield return
        [
            new DateTime(2023, 10, 30, 7, 59, 0),
            new DateTime(2023, 10, 29, 8, 0, 0),
            new DateTime(2023, 10, 30, 8, 0, 0)
        ];
        yield return
        [
            new DateTime(2023, 10, 30, 0, 0, 0),
            new DateTime(2023, 10, 29, 8, 0, 0),
            new DateTime(2023, 10, 30, 8, 0, 0)
        ];
    }
}

#endregion