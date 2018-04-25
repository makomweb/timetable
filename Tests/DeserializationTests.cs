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
        public void Deserialize_table_from_file_should_succeed()
        {
            var table = Deserialize("TimeTable.json");
            Assert.IsNotNull(table, "table should not be null!");
        }

        private TestTable Deserialize(string fileName)
        {
            var path = Path.Combine("Data", fileName);
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<TestTable>(json);
        }

        private class TestWeekday : Weekday
        {
            [JsonProperty("Name")]
            public string DayName { get; set; }
        }

        private class TestTable : Table
        {
            public TestTable() : base(Enumerable.Empty<TestWeekday>())
            {
            }

            public TestWeekday[] Weekdays
            {
                get { return Days.Cast<TestWeekday>().ToArray(); }
                set { Days = value; }
            }
        }
    }
}
