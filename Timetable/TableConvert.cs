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

        private static Weekday FromPersistence(Persistence.Weekday day, string blockType)
        {
            return Weekday.Create(day.Name, day.Blocks.Select(b => FromPersistence(b, blockType)));
        }

        private static Block FromPersistence(Persistence.Block b, string blockType)
        {
            var begin = BlockStartTime.FromString(b.Begin);
            return Block.Create(begin, b.Name, blockType);
        }
    }
}
