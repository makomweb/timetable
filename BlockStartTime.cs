using System;
using System.Collections.Generic;

namespace Timetable
{
    public class BlockStartTime
    {
        private List<TimeSpan> _startTimes = new List<TimeSpan>();

        public BlockStartTime(IEnumerable<string> startTimes)
        {
            foreach (var t in startTimes)
            {
                var ts = new TimeSpan(int.Parse(t.Split(':')[0]), int.Parse(t.Split(':')[1]), 0);

                _startTimes.Add(ts);
            }
        }

        public TimeSpan For(int index)
        {
            return _startTimes[index];
        }

        public string BlockType
        {
            get
            {
                var first = _startTimes[0];
                var second = _startTimes[1];
                var span = second.Subtract(first);
                if (span >= DoubleBlock.DefaultDuration)
                {
                    return nameof(DoubleBlock);
                }
                return nameof(RegularBlock);
            }
        }
    }
}
