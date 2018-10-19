using System.Collections.Generic;

namespace Timetable
{
    public class JannisTableA : JannisTable
    {
        public JannisTableA() : base("Jannis - A", CreateWeekdays())
        {
        }

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", "Ethik", "Musik", "Bio", "Deutsch");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Mathe", "Englisch", "Kunst", "Geschichte");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", null, "Physik", "Sport");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Ph/Ch", "Französisch", "Deutsch");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Geographie", "Französisch", "Mathe", "Klassenleiterstunde");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
