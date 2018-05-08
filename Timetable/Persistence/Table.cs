using Timetable;

namespace Timetable.Persistence
{
    public class Table
    {
        public string[] BlockStartTimes { get; set; }

        public BlockStartTime StartTimes => new BlockStartTime(BlockStartTimes);

        public Weekday[] Weekdays { get; set; }
    }
}
