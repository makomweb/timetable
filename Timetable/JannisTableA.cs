using System.Collections.Generic;

namespace Timetable
{
    public class JannisTableA : JannisTable
    {
        public JannisTableA() : base(CreateWeekdays())
        {
        }

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", null, "Französich", "Englisch", "Deutsch");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Mathe", "Erdkunde", "Chemie/Physik");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Geschichte", "Musik", "Sport", "Kunst");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Deutsch", "Informatik", "Ethik", "Klassenleiterstunde");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Biologie", "Französisch", "Mathe");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
