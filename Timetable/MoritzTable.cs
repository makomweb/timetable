using System.Collections.Generic;

namespace Timetable
{
    public class MoritzTable : Table
    {
        public MoritzTable() : base("Moritz", CreateWeekdays())
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
            var monday = dayFactory.CreateWeekday("Monday", "WPU (Kunst)", "WPU (Kunst)", "Sport", "Sport", "Kunst", "Klassenrat", "Bio/Ch/Ph");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Geschichte", "Musik", "Deutsch", "Deutsch", "ITG/WAT", "ITG/WAT", "Geographie");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Französisch", "Englisch", "Mathematik", "Mathematik", "Sport", "Ethik", "AG", "AG");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Bio/Ch/Ph", "Bio/Ch/Ph", "Französisch", "Französisch", "Ethik", "Mathematik");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Englisch", "Englisch", "Deutsch", "Deutsch", "Mathematik", "Französich");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
