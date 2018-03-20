using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Timetable
{
    public class Table
    {
        public IEnumerable<Weekday> Days { get; private set; }

        public Table(IEnumerable<Weekday> days)
        {
            Days = days;
        }

        public Weekday GetWeekday(DayOfWeek dayOfWeek)
        {
            return Days.FirstOrDefault(d => d.IsSame(dayOfWeek));
        }

        public void Externalize(Calendar calendar)
        {
            var weekStart = Week.Start;
            foreach (var day in Days)
            {
                var events = CreateEvents(day, weekStart);
                calendar.Events.AddRange(events);
            }
        }

        private static IEnumerable<CalendarEvent> CreateEvents(Weekday weekDay, DateTime startOfWeek)
        {
            var events = new List<CalendarEvent>();

            var date = weekDay.GetDate(startOfWeek);
            foreach (var block in weekDay.Items.OfType<Block>())
            {
                var begin = block.GetStartTime(date, "Europe/Germany").AsUtc;
                var end = begin.Add(block.Duration);
                var ev = new CalendarEvent
                {
                    Start = new CalDateTime(begin),
                    End = new CalDateTime(end),
                    Summary = block.Subject.Name
                };
                events.Add(ev);
            }

            return events;
        }
    }
}
