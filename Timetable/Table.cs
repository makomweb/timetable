using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            foreach (var d in Days)
            {
                var e = CreateEvents(d, weekStart);
                calendar.Events.AddRange(e);
            }
        }

        private static IEnumerable<CalendarEvent> CreateEvents(Weekday weekDay, DateTime startOfWeek)
        {
            var result = new List<CalendarEvent>();

            var date = weekDay.GetDate(startOfWeek);
            foreach (var block in weekDay.Items.OfType<Block>())
            {
                var begin = block.GetStartTime(date).ToUniversalTime();
                var end = begin.Add(block.Duration).ToUniversalTime();
                var ev = new CalendarEvent
                {
                    Start = new CalDateTime(begin),
                    End = new CalDateTime(end),
                    Summary = block.Subject.Name
                };
                result.Add(ev);
            }

            return result;
        }
    }
}
