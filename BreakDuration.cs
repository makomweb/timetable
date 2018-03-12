using System;

namespace Timetable
{
    public class BreakDuration
    {
        private readonly BlockStartTime _blockStartTime;
        private readonly TimeSpan _blockDuration;
        private readonly BreakStartTime _breakStart;

        public BreakDuration(BlockStartTime blockStartTime, BreakStartTime breakStart)
        {
            _blockStartTime = blockStartTime;
            _blockDuration = blockStartTime.BlockDuration;
            _breakStart = breakStart;
        }

        public TimeSpan For(int index)
        {
            var breakStart = _breakStart.For(index);
            var nextBlock = _blockStartTime.For(index + 1);
            return nextBlock.Subtract(breakStart);
        }
    }
}
