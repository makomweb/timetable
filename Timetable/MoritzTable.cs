using System.Collections.Generic;

namespace Timetable
{
    public class MoritzTable : Table
    {
        public MoritzTable() : base(CreateWeekdays())
        {
        }

        private static BlockStartTime BlockStartTimes = new BlockStartTime(new[]
        {
            "07:50:00", "8:45:00", "9:50:00", "10:45:00", "11:55:00", "12:45:00", "13:50:00", "14:40:00"
        });

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", "Französisch", "Französisch", "Deutsch", "Deutsch", "Bio/Physik/Chemie", "Englisch");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Mathe", "Deutsch", "Bio/Physik/Chemie", "Bio/Physik/Chemie", "Informatik", "Englisch", "Ethik");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Mathe", "Geschichte", "Englisch", "Französisch", "WAT", "Kunst"); // zusätzlich alle 2 Wochen "Kunst AG"
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Kunst WPU", "Kunst WPU", "Sport", "Sport", "Französisch", "Musik");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Mathe", "Mathe", "Ethik", "Deutsch", "Klassenrat", "Sport", "Geographie");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
