using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            var json = table.ToJson();
            Assert.IsFalse(string.IsNullOrEmpty(json), "JSON string should not be null or empty!");
        }

        [TestMethod]
        public void When_getting_second_block_start_time_it_should_succeed()
        {
            var times = new BlockStartTimes(new[] { "07:55:00", "08:40:00", "09:45:00", "10:45:00", "11:50:00", "12:45:00" });
            var t = times.Begins(1);
            Assert.AreEqual("08:40:00", t.ToString());
        }
    }
}
