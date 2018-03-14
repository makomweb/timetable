﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timetable
{
    public class Table
    {
        private IEnumerable<Weekday> _days;

        public Table(IEnumerable<Weekday> days)
        {
            _days = days;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(_days);
        }

        public Weekday GetWeekday(DayOfWeek dayOfWeek)
        {
            return _days.FirstOrDefault(d => d.IsSame(dayOfWeek));
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

            var startOfWeek = WeekStart;

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

        private static DateTime WeekStart => StartOfWeek(DateTime.Now, DayOfWeek.Monday);

        public static DateTime StartOfWeek(DateTime now, DayOfWeek startOfWeek)
        {
            int diff = (7 + (now.DayOfWeek - startOfWeek)) % 7;
            return now.AddDays(-1 * diff).Date;
        }
    }
}