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
            var monday = dayFactory.CreateWeekday("Montag", null, "Deutsch", "Englisch", "Kunst");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Dienstag", "Französisch", "Informatik", "Mathe");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Mittwoch", "Englisch", "Musik", "Sport", "Französisch");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Donnerstag", "Chemie/Physik", "Geschichte", "Ethik");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Freitag", "Sport", "Biologie", "Mathematik");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
