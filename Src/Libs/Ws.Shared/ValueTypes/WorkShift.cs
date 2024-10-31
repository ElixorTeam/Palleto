namespace Ws.Shared.ValueTypes;

public readonly struct WorkShift
{
    public readonly DateTime Start;
    public readonly DateTime End;

    public WorkShift(DateTime dateTime)
    {
        Start = CalculateStartOfShift(dateTime);
        End = CalculateEndOfShift(dateTime);
    }

    public WorkShift()
    {
        Start = CalculateStartOfShift(DateTime.Now);
        End = CalculateEndOfShift(DateTime.Now);
    }
    private static DateTime CalculateStartOfShift(DateTime date)
    {
        if (date.Hour < 8)
            date = date.AddDays(-1);
        return new(date.Year, date.Month, date.Day, 8, 0, 0);
    }

    private static DateTime CalculateEndOfShift(DateTime date)
    {
        if (date.Hour >= 8)
            date = date.AddDays(1);
        return new(date.Year, date.Month, date.Day, 8, 0, 0);
    }
}
