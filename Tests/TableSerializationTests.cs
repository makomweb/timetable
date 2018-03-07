using Microsoft.VisualStudio.TestTools.UnitTesting;
using Timetable.Models;

namespace Tests
{
    [TestClass]
    public class TableSerializationTests
    {
        [TestMethod]
        public void When_serializing_table_it_should_return_json()
        {
            var table = new Table();
            var json = Serialization.ToJson(table);
            Assert.IsFalse(string.IsNullOrEmpty(json), "JSON string should not be null or empty!");
        }
    }
}
