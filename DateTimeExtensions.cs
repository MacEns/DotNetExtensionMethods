public static class DateTimeExtensions
{
    public static DateTime AddWeeks(this DateTime dateTime, int numberOfWeeks) => dateTime.AddDays(numberOfWeeks * 7);

    public static string ToString(this DateTime? dt, string format) => dt == null ? "n/a" : ((DateTime)dt).ToString(format);

    public static DateTime StartOfWeek(this DateTime date) => date.AddDays(-(date.DayOfWeek - Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)).Date;

    public static DateTime StartOfMonth(this DateTime date) => new(date.Year, date.Month, 1);

    public static bool IsWeekEnd(this DateTime date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    public static DateTime FirstDayOfMonth(this DateTime dt) => new(dt.Year, dt.Month, 1);

    public static DateTime LastDayOfMonth(this DateTime dt) => new(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

    public static DateTime ToCurrentYear(this DateTime dt)
    {
        var nextDate = new DateTime(DateTime.Today.Year, dt.Month, dt.Day);
        return DateTime.Today >= nextDate ? nextDate.AddYears(1) : nextDate;
    }

    public static DateTime RoundToSecond(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
}
