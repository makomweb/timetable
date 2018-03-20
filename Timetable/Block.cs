using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;

namespace Timetable
{
    public abstract class Block : Item
    {
        public Subject Subject { get; }

        protected Block(Subject subject, TimeSpan begin, TimeSpan duration) : base(begin, duration)
        {
            Subject = subject;
        }

        public CalendarEvent ToCalendarEvent(DateTime date, string timezoneId)
        {
            var begin = GetStartTime(date, timezoneId).AsUtc;
            var end = begin.Add(Duration);

            return new CalendarEvent
            {
                Start = new CalDateTime(begin),
                End = new CalDateTime(end),
                Summary = Subject.Name
            };
        }
    }
}
