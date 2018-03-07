using System;

namespace Timetable.Models
{
    public class RegularBlock : Block
    {
        public RegularBlock(TimeSpan begin, string name) : this(begin, new Subject(name))
        {
        }

        public RegularBlock(TimeSpan begin, Subject subject) : base(subject, begin, DefaultDuration)
        {
        }

        public static TimeSpan DefaultDuration = TimeSpan.FromMinutes(45);

        public override string Type => nameof(RegularBlock);
    }
}
