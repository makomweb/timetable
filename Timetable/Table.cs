using Ical.Net;
using Ical.Net.CalendarComponents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Timetable
{
    public abstract class Table
    {
        public IEnumerable<Weekday> Days { get; private set; }

        public Table(IEnumerable<Weekday> days)
        {
            Days = days;
        }

        public void Externalize(Calendar calendar, string timezoneId)
        {
            var weekStart = Week.Start;
            foreach (var day in Days)
            {
                var events = CreateEvents(day, weekStart, timezoneId);
                calendar.Events.AddRange(events);
            }
        }

        private static IEnumerable<CalendarEvent> CreateEvents(Weekday weekDay, DateTime startOfWeek, string timezoneId)
        {
            var events = new List<CalendarEvent>();

            var date = weekDay.GetDate(startOfWeek);
            foreach (var block in weekDay.Items.OfType<Block>())
            {
                var ev = block.ToCalendarEvent(date, timezoneId);
                events.Add(ev);
            }

            return events;
        }
    }
}
