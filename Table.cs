using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Timetable
{
    public class Table
    {
        private DataTable _table;
        
        public Table(DataTable table)
        {
            _table = table;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(_table);
        }

        public IEnumerable<object> At(int index)
        {
            return _table.Rows[index].ItemArray;
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
            for (var i = 0; i < 5; i++)
            {
                var dayRow = At(i);
                var e = CreateEvents(startOfWeek, dayRow);
                sb.AppendLine(e);
            }

            //end calendar item
            sb.AppendLine("END:VCALENDAR");

            //create a string from the stringbuilder
            return sb.ToString();
        }

        private static string CreateEvents(DateTime startOfWeek, IEnumerable<object> dayRow)
        {
            var weekDayString = dayRow.First() as string;
            var weekDay = ToDayOfWeek(weekDayString);
            var day = startOfWeek;

            switch (weekDay)
            {
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Tuesday:
                    day = startOfWeek.AddDays(1);
                    break;
                case DayOfWeek.Wednesday:
                    day = startOfWeek.AddDays(2);
                    break;
                case DayOfWeek.Thursday:
                    day = startOfWeek.AddDays(3);
                    break;
                case DayOfWeek.Friday:
                    day = startOfWeek.AddDays(4);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown day '{weekDay}'!");
            }
            
            var sb = new StringBuilder();

            var blocks = dayRow.OfType<Block>();
            foreach (var block in blocks)
            {
                var ics = block.ToIcs(day);
                sb.AppendLine(ics);
            }

            return sb.ToString();
        }

        private static DateTime WeekStart => StartOfWeek(DateTime.Now, DayOfWeek.Monday);

        public static DateTime StartOfWeek(DateTime now, DayOfWeek startOfWeek)
        {
            int diff = (7 + (now.DayOfWeek - startOfWeek)) % 7;
            return now.AddDays(-1 * diff).Date;
        }

        private static DayOfWeek ToDayOfWeek(string day)
        {
            switch (day)
            {
                case "Montag": return DayOfWeek.Monday;
                case "Dienstag": return DayOfWeek.Tuesday;
                case "Mittwoch": return DayOfWeek.Wednesday;
                case "Donnerstag": return DayOfWeek.Thursday;
                case "Freitag": return DayOfWeek.Friday;
                default:
                    throw new InvalidOperationException($"Unknown day '{day}'!");
            }
        }
    }
}
