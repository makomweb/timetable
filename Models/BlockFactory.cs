using System;

namespace Timetable.Models
{
    public class BlockFactory
    {
        private readonly BlockStartTime _startTime;

        public BlockFactory(BlockStartTime startTime)
        {
            _startTime = startTime;
        }

        public RegularBlock CreateRegularBlock(int index, string subject)
        {
            return new RegularBlock(_startTime.For(index), subject);
        }

        public DoubleBlock CreateDoubleBlock(int index, string subject)
        {
            throw new NotImplementedException();
        }
    }
}
