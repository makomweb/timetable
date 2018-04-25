using Ical.Net.CalendarComponents;
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

        public static Block Create(TimeSpan begin, string subject, string blockType)
        {
            return Create(begin, new Subject(subject), blockType);
        }

        public static Block Create(TimeSpan begin, Subject subject, string blockType)
        {
            switch (blockType)
            {
                case "RegularBlock": return new RegularBlock(begin, subject);
                case "DoubleBlock": return new DoubleBlock(begin, subject);
                default: throw new NotSupportedException($"Blocktype '{blockType}' is not supported!");
            }
        }

        public CalendarEvent ToCalendarEvent(DateTime date, string timezoneId)
        {
            var begin = GetStartTime(date, timezoneId);
            var end = begin.Add(Duration);

            return new CalendarEvent
            {
                Start = begin,
                End = end,
                Summary = Subject.Name
            };
        }
    }
}
