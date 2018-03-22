using Ical.Net.DataTypes;
using System;

namespace Timetable
{
    public abstract class Item
    {
        public TimeSpan Begin { get; }

        public TimeSpan Duration { get; }

        protected Item(TimeSpan begin, TimeSpan duration)
        {
            Begin = begin;
            Duration = duration;
        }

        public abstract string Type { get; }

        public CalDateTime GetStartTime(DateTime date, string timezoneId)
        {
            var cdt = new CalDateTime(date.Year, date.Month, date.Day, Begin.Hours, Begin.Minutes, Begin.Seconds, timezoneId);
            return new CalDateTime(cdt.AsUtc);
        }
    }
}
