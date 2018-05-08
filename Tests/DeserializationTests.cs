using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using DeserializedTable = Timetable.Deserialized.Table;
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

        private DeserializedTable Deserialize(string fileName)
        {
            var path = Path.Combine("Data", fileName);
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<DeserializedTable>(json);
        }
    }
}
