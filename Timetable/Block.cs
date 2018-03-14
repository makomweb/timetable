using System;
using System.Text;

namespace Timetable
{
    public abstract class Block : Item
    {
        public Subject Subject { get; }

        protected Block(Subject subject, TimeSpan begin, TimeSpan duration) : base(begin, duration)
        {
            Subject = subject;
        }

        public string ToIcs(DateTime day)
        {
            return CreateEvent(day, this);
        }

        private static string CreateEvent(DateTime day, Block block)
        {
            var begin = block.GetStartTime(day);
            var end = begin.Add(block.Duration);
            return CreateEvent(begin, end, block.Subject.Name);
        }

        private static string CreateEvent(DateTime start, DateTime end, string summary, string location = null, string description = null)
        {
            var sb = new StringBuilder();

            sb.AppendLine("BEGIN:VEVENT");

#if true
            //with time zone specified
            sb.AppendLine("DTSTART;TZID=Europe/Germany:" + start.ToString("yyyyMMddTHHmm00"));
            sb.AppendLine("DTEND;TZID=Europe/Germany:" + end.ToString("yyyyMMddTHHmm00"));
#else
            //or without
            sb.AppendLine("DTSTART:" + start.ToString("yyyyMMddTHHmm00"));
            sb.AppendLine("DTEND:" + end.ToString("yyyyMMddTHHmm00"));
#endif

            sb.AppendLine("SUMMARY:" + summary + "");
            sb.AppendLine("LOCATION:" + location + "");
            sb.AppendLine("DESCRIPTION:" + description + "");
            sb.AppendLine("PRIORITY:3");
            sb.AppendLine("END:VEVENT");

            return sb.ToString();
        }
    }
}
