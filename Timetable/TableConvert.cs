using System.Linq;

namespace Timetable
{
    public static class TableConvert
    {
        public static Table FromDeserialized(Deserialized.Table table)
        {
            var blockType = table.StartTimes.BlockType;
            var weekdays = table.Weekdays.Select(w => FromDeserialized(w, blockType));
            return new Table(weekdays);
        }

        private static Weekday FromDeserialized(Deserialized.Weekday day, string blockType)
        {
            return Weekday.Create(day.Name, day.Blocks.Select(b => FromDeserialized(b, blockType)));
        }

        private static Block FromDeserialized(Deserialized.Block b, string blockType)
        {
            var begin = BlockStartTime.FromString(b.Begin);
            return Block.Create(begin, b.Name, blockType);
        }
    }
}
