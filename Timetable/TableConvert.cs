using System.Collections.Generic;
using System.Linq;

namespace Timetable
{
    public static class TableConvert
    {
        public static Table FromPersistenceTable(Persistence.Table table)
        {
            var blockType = new BlockStartTime(table.BlockStartTimes).BlockType;
            var weekdays = table.Weekdays.Select(w => FromPersistenceDay(w, blockType));
            return new Table(table.Id, weekdays);
        }

        public static Persistence.Table ToPersistenceTable(BlockStartTime startTimes, Table table)
        {
            return new Persistence.Table
            {
                BlockStartTimes = startTimes.ToArray(),
                Weekdays = ToPersistence(table.Days).ToArray()
            };
        }

        private static Weekday FromPersistenceDay(Persistence.Weekday day, string blockType)
        {
            return Weekday.Create(day.Name, day.Blocks.Select(b => FromPersistenceBlock(b, blockType)));
        }

        private static Block FromPersistenceBlock(Persistence.Block b, string blockType)
        {
            var begin = BlockStartTime.FromString(b.Begin);
            return Block.Create(begin, b.Name, blockType);
        }

        private static IEnumerable<Persistence.Weekday> ToPersistence(IEnumerable<Weekday> days)
        {
            return days.Select(d => ToPersistence(d));
        }

        private static Persistence.Weekday ToPersistence(Weekday day)
        {
            return new Persistence.Weekday()
            {
                Name = day.Name,
                Blocks = ToPersistenceBlocks(day).ToArray()
            };
        }

        private static IEnumerable<Persistence.Block> ToPersistenceBlocks(Weekday day)
        {
            return ToPersistenceBlocks(day.Items.OfType<Block>());
        }

        private static IEnumerable<Persistence.Block> ToPersistenceBlocks(IEnumerable<Block> blocks)
        {
            return blocks.Select(b => new Persistence.Block { Name = b.Subject.Name, Begin = b.Begin.ToString() });
        }
    }
}
