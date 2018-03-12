using System;

namespace Timetable
{
    public class BreakStartTime
    {
        private readonly BlockStartTime _blockStartTime;
        private readonly TimeSpan _blockDuration;

        public BreakStartTime(BlockStartTime blockStartTime)
        {
            _blockStartTime = blockStartTime;
            _blockDuration = blockStartTime.BlockDuration;
        }

        public TimeSpan For(int index)
        {
            var block = _blockStartTime.For(index);
            return block.Add(_blockDuration);
        }
    }
}
