using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Timetable;

namespace Tests
{
    [TestClass]
    public class DeserializationTests
    {
        [TestMethod]
        public void Deserialized_table_should_not_be_null()
        {
            var table = Deserialize("TimeTable.json");
            Assert.IsNotNull(table, "table should not be null!");
        }

        [TestMethod]
        public void Deserialized_block_start_time_should_be_available()
        {
            var table = Deserialize("TimeTable.json");
            Assert.IsNotNull(table.StartTimes, "Block start times should not be null!");
        }

        [TestMethod]
        public void Deserialized_block_should_contain_subject_name()
        {
            var table = Deserialize("TimeTable.json");
            var block = table.Weekdays.First().Blocks.First();
            Assert.IsFalse(string.IsNullOrEmpty(block.Name), "Block should contain subject name!");
        }

        [TestMethod]
        public void Converting_deserialized_table_to_regular_table_should_succeed()
        {
            var deserialized = Deserialize("TimeTable.json");
            var table = TableConvert.FromDeserialized(deserialized);
            Assert.IsNotNull(table, "Table should not be null!");
        }

        private Deserialized.Table Deserialize(string fileName)
        {
            var path = Path.Combine("Data", fileName);
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Deserialized.Table>(json);
        }
    }

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

    namespace Deserialized
    {
        public class Block
        {
            public string Name { get; set; }

            public string Begin { get; set; }
        }

        public class Weekday
        {
            public string Name { get; set; }

            public Block[] Blocks { get; set; }
        }

        public class Table
        {
            public string[] BlockStartTimes { get; set; }

            public BlockStartTime StartTimes => new BlockStartTime(BlockStartTimes);

            public Weekday[] Weekdays { get; set; }
        }
    }
}
