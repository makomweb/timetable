using Timetable;

namespace Timetable.Persistence
{
    public class Table
    {
        public string[] BlockStartTimes { get; set; }

        public BlockStartTime StartTimes
        {
            get { return new BlockStartTime(BlockStartTimes); }
            set { BlockStartTimes = value.ToArray(); }
        }

        public Weekday[] Weekdays { get; set; }
    }
}
