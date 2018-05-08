using System.Collections.Generic;
using System.Linq;

namespace Timetable
{
    public static class TableConvert
    {
        public static Table FromPersistence(Persistence.Table table)
        {
            var blockType = table.StartTimes.BlockType;
            var weekdays = table.Weekdays.Select(w => FromPersistence(w, blockType));
            return new Table(weekdays);
        }

        public static Persistence.Table ToPersistence(BlockStartTime startTimes, Table table)
        {
            return new Persistence.Table
            {
                StartTimes = startTimes,
                Weekdays = ToPersistence(table.Days).ToArray()
            };
        }

        private static Weekday FromPersistence(Persistence.Weekday day, string blockType)
        {
            return Weekday.Create(day.Name, day.Blocks.Select(b => FromPersistence(b, blockType)));
        }

        private static Block FromPersistence(Persistence.Block b, string blockType)
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
                Blocks = ToBlocks(day).ToArray()
            };
        }

        private static IEnumerable<Persistence.Block> ToBlocks(Weekday day)
        {
            return ToBlocks(day.Items.OfType<Block>());
        }

        private static IEnumerable<Persistence.Block> ToBlocks(IEnumerable<Block> blocks)
        {
            return blocks.Select(b => new Persistence.Block { Name = b.Subject.Name, Begin = b.Begin.ToString() });
        }
    }
}
