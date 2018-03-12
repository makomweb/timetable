using System.Collections.Generic;
using Timetable;

namespace Tests
{
    public class JannisTableB : Table
    {
        public JannisTableB() : base(CreateWeekdays())
        {
        }

        private static BlockStartTime BlockStartTimes = new BlockStartTime(new[] { "08:00:00", "10:00:00", "12:00:00", "13:45:00" });

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", null, "Deutsch", "Englisch", "Kunst");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Französisch", "Informatik", "Mathe");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Englisch", "Musik", "Sport", "Französisch");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Chemie/Physik", "Geschichte", "Ethik");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Sport", "Biologie", "Mathematik");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
