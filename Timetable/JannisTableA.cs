﻿using System.Collections.Generic;

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
            var monday = dayFactory.CreateWeekday("Monday", "Deutsch", "Kunst", "Biologie", "Ethik");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Tuesday", "Französisch", "Chemie/Physik", "Englisch");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Wednesday", "Mathematik", "Physik", "Sport");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Thursday", "Geschichte", "Französisch", "Deutsch");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Friday", "Geographie", "Englisch", "Mathe", "Klassenleiterstunde");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
