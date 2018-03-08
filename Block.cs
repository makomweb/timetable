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
    }
}
