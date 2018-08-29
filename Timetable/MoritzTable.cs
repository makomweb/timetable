using System.Collections.Generic;

namespace Timetable
{
    public class MoritzTable : Table
    {
        public MoritzTable() : base(CreateWeekdays())
        {
        }

        public static BlockStartTime BlockStartTimes = new BlockStartTime(new[]
        {
            "07:50:00", "8:45:00", "9:50:00", "10:45:00", "11:55:00", "12:45:00", "13:50:00", "14:40:00"
        });

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Monday", "WPU", "WPU", "Sport", "Sport", "Kunst", "Ethik", "Biologie");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Ethik", "Musik", "Deutsch", "Deutsch/Geschichte/ITG", "ITG/WAT", "WAT", "Geographie");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Französisch", "Englisch", "Mathematik", "Mathematik", "Sport", "Kunst", "AG", "AG");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Bio/Chemie", "Bio/Chemie", "Französisch", "Französisch", "Deutsch", "Mathematik");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Englisch", "Englisch", "Deutsch", "Deutsch", "Mathematik", "Französich");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
