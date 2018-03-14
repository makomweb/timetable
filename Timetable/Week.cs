using System;

namespace Timetable
{
    public static class Week
    { 
        public static DateTime Start => GetStart(DateTime.Now, DayOfWeek.Monday);

        public static DateTime GetStart(DateTime now, DayOfWeek startOfWeek)
        {
            int diff = (7 + (now.DayOfWeek - startOfWeek)) % 7;
            return now.AddDays(-1 * diff).Date;
        }
    }
}
