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

        public string ToIcs()
        {
            //create a new stringbuilder instance
            StringBuilder sb = new StringBuilder();

            //start the calendar item
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:timetable");
            sb.AppendLine("CALSCALE:GREGORIAN");
            sb.AppendLine("METHOD:PUBLISH");

            //create a time zone if needed, TZID to be used in the event itself
            sb.AppendLine("BEGIN:VTIMEZONE");
            sb.AppendLine("TZID:Europe/Germany");
            sb.AppendLine("BEGIN:STANDARD");
            sb.AppendLine("TZOFFSETTO:+0100");
            sb.AppendLine("TZOFFSETFROM:+0100");
            sb.AppendLine("END:STANDARD");
            sb.AppendLine("END:VTIMEZONE");

            var startOfWeek = Week.Start;

            // loop over the days of the week.
            foreach (DayOfWeek d in Enum.GetValues(typeof(DayOfWeek)))
            {
                var weekday = GetWeekday(d);
                if (weekday == null)
                {
                    continue;
                }

                var e = weekday.ToIcs(startOfWeek);
                sb.AppendLine(e);
            }

            //end calendar item
            sb.AppendLine("END:VCALENDAR");

            //create a string from the stringbuilder
            return sb.ToString();
        }

        private Calendar GetCalendar()
        {
            var calendar = new Calendar();

            var weekStart = Week.Start;
            foreach (var d in Days)
            {
                var e = CreateEvents(d, weekStart);
                calendar.Events.AddRange(e);
            }

            return calendar;
        }

        private static IEnumerable<CalendarEvent> CreateEvents(Weekday weekDay, DateTime startOfWeek)
        {
            var result = new List<CalendarEvent>();

            var date = weekDay.GetDate(startOfWeek);
            foreach (var block in weekDay.Items.OfType<Block>())
            {
                var begin = block.GetStartTime(date);
                var end = begin.Add(block.Duration);
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
