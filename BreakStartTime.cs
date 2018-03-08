using System;

namespace Timetable
{
    public class BreakStartTime
    {
        private readonly BlockStartTime _blockStartTime;
        private readonly TimeSpan _blockDuration;

        public BreakStartTime(BlockStartTime blockStartTime, TimeSpan blockDuration)
        {
            _blockStartTime = blockStartTime;
            _blockDuration = blockDuration;
        }

        public TimeSpan For(int index)
        {
            var block = _blockStartTime.For(index);
            return block.Add(_blockDuration);
        }
    }
}
