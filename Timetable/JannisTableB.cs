using System.Collections.Generic;

namespace Timetable
{
    public class JannisTableB : JannisTable
    {
        public JannisTableB() : base("Jannis - B", CreateWeekdays())
        {
        }

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", "Chemie", "Mathe", "Deutsch", "Geschichte");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Französisch", "Englisch", "Geographie");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Chemie/Physik", "Französich", "Sport", "Kunst");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Musik", "Mathematik", "Deutsch");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Sport", "Biologie", "Ethik");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
