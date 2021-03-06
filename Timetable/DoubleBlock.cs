﻿using System;

namespace Timetable
{
    public class DoubleBlock : Block
    {
        public DoubleBlock(TimeSpan begin, string name) : this(begin, new Subject(name))
        {
        }

        public DoubleBlock(TimeSpan begin, Subject subject) : base(subject, begin, DefaultDuration)
        {
        }

        public static TimeSpan DefaultDuration = TimeSpan.FromMinutes(90);

        public override string Type => nameof(DoubleBlock);
    }
}
