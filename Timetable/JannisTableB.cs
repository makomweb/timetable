using System.Collections.Generic;

namespace Timetable
{
    public class JannisTableB : JannisTable
    {
        public JannisTableB() : base(CreateWeekdays())
        {
        }

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", null, "Deutsch", "Englisch", "Ethik");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", null, "Informatik", "Französich", "Mathe");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Englisch", "Musik", "Sport", "Französisch");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Deutsch", "Chemie/Physik", "Geschichte", "Ethik");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Sport", "Biologie", "Mathematik");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
