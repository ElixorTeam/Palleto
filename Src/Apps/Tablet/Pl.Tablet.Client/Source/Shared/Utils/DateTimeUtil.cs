namespace Pl.Tablet.Client.Source.Shared.Utils;

public static class DateTimeUtil
{
    public static bool TryParseStringDate(string? input, out DateTime date)
    {
        date = default;

        if (string.IsNullOrWhiteSpace(input)) return false;
        try
        {
            int day, month, year;

            switch (input.Length)
            {
                case 2:
                    day = int.Parse(input.Substring(0, 2));
                    month = DateTime.Now.Month;
                    year = DateTime.Now.Year;
                    break;

                case 4:
                    day = int.Parse(input.Substring(0, 2));
                    month = int.Parse(input.Substring(2, 2));
                    year = DateTime.Now.Year;
                    break;

                case 6:
                    day = int.Parse(input.Substring(0, 2));
                    month = int.Parse(input.Substring(2, 2));
                    year = int.Parse(input.Substring(4, 2)) + 2000;
                    break;

                default:
                    return false;
            }

            date = new(year, month, day);
            return true;
        }
        catch
        {
            return false;
        }
    }
}