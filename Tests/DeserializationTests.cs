using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using PersistenceTable = Timetable.Persistence.Table;
using Timetable;

namespace Tests
{
    [TestClass]
    public class DeserializationTests
    {
        [TestMethod]
        public void Deserialized_table_should_not_be_null()
        {
            var table = Load("TimeTable.json");
            Assert.IsNotNull(table, "table should not be null!");
        }

        [TestMethod]
        public void Deserialized_block_start_time_should_be_available()
        {
            var table = Load("TimeTable.json");
            Assert.IsNotNull(table.StartTimes, "Block start times should not be null!");
        }

        [TestMethod]
        public void Deserialized_block_should_contain_subject_name()
        {
            var table = Load("TimeTable.json");
            var block = table.Weekdays.First().Blocks.First();
            Assert.IsFalse(string.IsNullOrEmpty(block.Name), "Block should contain subject name!");
        }

        [TestMethod]
        public void Convert_persistence_table_to_regular_table_should_succeed()
        {
            var table = Load("TimeTable.json");
            var regular = TableConvert.FromPersistence(table);
            Assert.IsNotNull(regular, "Regular table should not be null!");
        }

        private PersistenceTable Load(string fileName)
        {
            var path = Path.Combine("Data", fileName);
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<PersistenceTable>(json);
        }
    }
}
