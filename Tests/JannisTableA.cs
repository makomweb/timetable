using System.Collections.Generic;
using Timetable;

namespace Tests
{
    public class JannisTableA : Table
    {
        public JannisTableA() : base(CreateWeekdays())
        {
        }

        private static BlockStartTime BlockStartTimes = new BlockStartTime(new[] { "08:00:00", "10:00:00", "12:00:00", "13:45:00" });

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", "Deutsch", "Biologie", "Englisch");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Mathe", "Französisch", "Chemie/Physik", "Deutsch");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Kunst", "Sport", "Musik");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Deutsch", "Informatik", "Ethik", "Klassenleiterstunde");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Erdkunde", "Französisch", "Mathe");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
