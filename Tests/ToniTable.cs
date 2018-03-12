﻿using System.Collections.Generic;
using Timetable;

namespace Tests
{
    public class ToniTable : Table
    {
        public ToniTable() : base(CreateWeekdays())
        {
        }

        private static BlockStartTime BlockStartTimes = new BlockStartTime(new[] { "07:45", "08:40", "09:45", "10:45", "11:50", "12:45" });

        private static IEnumerable<Weekday> CreateWeekdays()
        {
            var weekdays = new List<Weekday>();

            var dayFactory = DayFactory.Create(BlockStartTimes);
            var monday = dayFactory.CreateWeekday("Montag", "Deutsch", "Musik", "Mathe", "Kunst", "Sachkunde", "Lebenskunde");
            weekdays.Add(monday);

            var tuesday = dayFactory.CreateWeekday("Dienstag", "Sport", "Sport", "Englisch", "Sachkunde", "Mathe", "Deutsch");
            weekdays.Add(tuesday);

            var wednesday = dayFactory.CreateWeekday("Mittwoch", "Mathe", "Sachkunde", "Deutsch", "Deutsch", "Englisch");
            weekdays.Add(wednesday);

            var thursday = dayFactory.CreateWeekday("Donnerstag", "Deutsch", "Mathe", "Kunst", "Lebenskunde", "Sachkunde", "Musik");
            weekdays.Add(thursday);

            var friday = dayFactory.CreateWeekday("Freitag", "Sachkunde", "Englisch", "Sport", "Mathe", "Deutsch", "Deutsch");
            weekdays.Add(friday);

            return weekdays;
        }
    }
}
