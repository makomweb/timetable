using System;

namespace Timetable
{
    public class BlockFactory
    {
        private readonly BlockStartTime _startTime;

        public BlockFactory(BlockStartTime startTime)
        {
            _startTime = startTime;
        }

        public Block CreateBlock(int index, string subject)
        {
            switch (_startTime.BlockType)
            {
                case "RegularBlock": return new RegularBlock(_startTime.For(index), subject);
                case "DoubleBlock": return new DoubleBlock(_startTime.For(index), subject);
                default: throw new NotSupportedException($"Unsupported block type '{_startTime.BlockType}'!");
            }
        }
    }
}
