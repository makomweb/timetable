using System;

namespace Timetable.Models
{
    public abstract class Item
    {
        public TimeSpan Begin { get; }

        public TimeSpan Duration { get; }

        protected Item(TimeSpan begin, TimeSpan duration)
        {
            Begin = begin;
            Duration = duration;
        }

        public abstract string Type { get; }
    }
}
