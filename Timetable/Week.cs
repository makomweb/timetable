using System;

namespace Timetable
{
    public static class Week
    { 
        public static DateTime Start => GetStart(DateTime.UtcNow, DayOfWeek.Monday);

        public static DateTime GetStart(DateTime now, DayOfWeek startOfWeek)
        {
            Ensure.That(now.Kind == DateTimeKind.Utc, $"Argument '{nameof(now)}' must be of kind UTC!");

            int diff = (7 + (now.DayOfWeek - startOfWeek)) % 7;
            return now.AddDays(-1 * diff).Date;
        }
    }
}
