﻿using System;

namespace Timetable
{
    public class Break : Item
    {
        public Break(TimeSpan begin, TimeSpan duration) : base(begin, duration)
        {
        }

        public override string Type => nameof(Break);
    }
}
