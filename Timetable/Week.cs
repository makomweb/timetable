using System;

namespace Timetable
{
    public static class Week
    { 
        public static DateTime Start => GetStart(DateTime.Now, DayOfWeek.Monday);

        public static DateTime GetStart(DateTime now, DayOfWeek startOfWeek)
        {
            Ensure.That(now.Kind == DateTimeKind.Local, $"Argument '{nameof(now)}' must be of type local time!");

            int diff = (7 + (now.DayOfWeek - startOfWeek)) % 7;
            return now.AddDays(-1 * diff).Date;
        }
    }
}
