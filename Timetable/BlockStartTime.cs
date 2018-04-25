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
                var ts = FromString(t);

                _startTimes.Add(ts);
            }
        }

        public static TimeSpan FromString(string time)
        {
            var parts = time.Split(':');

            switch (parts.Length)
            {
                case 2: return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), 0);
                case 3: return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
                default:
                    throw new NotSupportedException($"Input string {time} does not contain 2 or 3 time parts (hour, minute and optional seconds)!");
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

        public TimeSpan BlockDuration
        {
            get
            {
                switch (BlockType)
                {
                    case "RegularBlock":return RegularBlock.DefaultDuration;
                    case "DoubleBlock": return DoubleBlock.DefaultDuration;
                    default: throw new NotSupportedException($"Blocktype '{BlockType}' is not supported!");
                }
            }
        }
    }
}
