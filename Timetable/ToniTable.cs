using System.Collections.Generic;

namespace Timetable
{
    public class ToniTable : Table
    {
        public ToniTable() : base("Toni", CreateWeekdays())
        {
        }

        public static BlockStartTime BlockStartTimes = new BlockStartTime(new[]
        {
            "07:45", "08:40", "09:45", "10:45", "11:50", "12:45", "13:30"
        });

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", "NaWi", "Musik", "Kunst", "Englisch", "Deutsch", "Mathematik");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "GeWi", "GeWi", "Mathematik", "Englisch", "Deutsch", "WPU", "WPU");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Eng", "Deutsch", "Sport", "Sport", "Mathematik", "GeWi");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Mathematik", "NaWi", "Deutsch", "Musik", "Kunst", "Lebenskunde");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Deutsch", "Englisch", "Sport", "Mathe", "NaWi", "NaWi");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
