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
            Assert.IsTrue(table.BlockStartTimes.Any(), "Block start times should not be null or empty!");
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
            var regular = TableConvert.FromPersistenceTable(table);
            Assert.IsNotNull(regular, "Regular table should not be null!");
        }

        [TestMethod]
        public void To_persistence_table_should_succeed()
        {
            {
                var table = new JannisTableA();
                var persistence = TableConvert.ToPersistenceTable(JannisTable.BlockStartTimes, table);
                var json = ToJson(persistence);
                Assert.IsFalse(string.IsNullOrEmpty(json), "JSON should not be null or empty!");
            }
            {
                var table = new JannisTableB();
                var persistence = TableConvert.ToPersistenceTable(JannisTable.BlockStartTimes, table);
                var json = ToJson(persistence);
                Assert.IsFalse(string.IsNullOrEmpty(json), "JSON should not be null or empty!");
            }
            {
                var table = new MoritzTable();
                var persistence = TableConvert.ToPersistenceTable(MoritzTable.BlockStartTimes, table);
                var json = ToJson(persistence);
                Assert.IsFalse(string.IsNullOrEmpty(json), "JSON should not be null or empty!");
            }
            {
                var table = new ToniTable();
                var persistence = TableConvert.ToPersistenceTable(ToniTable.BlockStartTimes, table);
                var json = ToJson(persistence);
                Assert.IsFalse(string.IsNullOrEmpty(json), "JSON should not be null or empty!");
            }
        }

        private PersistenceTable Load(string fileName)
        {
            var path = Path.Combine("Data", fileName);
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<PersistenceTable>(json);
        }

        private static string ToJson(PersistenceTable table)
        {
            return JsonConvert.SerializeObject(table);
        }
    }
}
