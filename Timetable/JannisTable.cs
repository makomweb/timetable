﻿using System.Collections.Generic;

namespace Timetable
{
    public abstract class JannisTable : Table
    {
        protected JannisTable(string id, IEnumerable<Weekday> weekdays) : base(id, weekdays)
        {
        }

        public static BlockStartTime BlockStartTimes = new BlockStartTime(new[]
        {
            "08:00:00", "10:00:00", "12:00:00", "13:45:00"
        });
    }
}
