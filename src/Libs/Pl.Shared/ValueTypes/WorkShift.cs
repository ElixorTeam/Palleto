namespace Pl.Shared.ValueTypes;

public readonly struct WorkShift
{
    public readonly DateTime Start;
    public readonly DateTime End;

    public WorkShift()
    {
        Start = CalculateStartOfShift(DateTime.Now);
        End = CalculateEndOfShift(DateTime.Now);
    }

    public WorkShift(DateOnly date)
    {
        DateTime dateTime = date.ToDateTime(TimeOnly.MinValue).AddHours(8);
        Start = CalculateStartOfShift(dateTime);
        End = CalculateEndOfShift(dateTime);
    }

    public WorkShift(DateTime dateTime)
    {
        Start = CalculateStartOfShift(dateTime);
        End = CalculateEndOfShift(dateTime);
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
