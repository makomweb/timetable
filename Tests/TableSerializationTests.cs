﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Timetable;

namespace Tests
{
    [TestClass]
    public class TableSerializationTests
    {
        [TestMethod]
        public void When_serializing_to_json_it_should_succeed()
        {
            var table = new TestTable();
            var json = table.ToJson();
            Assert.IsFalse(string.IsNullOrEmpty(json), "JSON string should not be null or empty!");
        }

        [TestMethod]
        public void When_getting_second_block_start_time_it_should_succeed()
        {
            var time = new BlockStartTime(new[] { "07:55", "08:40", "09:45", "10:45", "11:50", "12:45" });
            var t = time.For(1);
            Assert.AreEqual("08:40:00", t.ToString());
        }

        [TestMethod]
        public void When_getting_last_block_start_time_it_should_succeed()
        {
            var time = new BlockStartTime(new[] { "07:45", "08:40", "09:45", "10:45", "11:50", "12:45" });
            var t = time.For(5);
            Assert.AreEqual("12:45:00", t.ToString());
        }

        [TestMethod]
        public void When_serializing_to_ics_it_should_succeed()
        {
            var table = new TestTable();
            var ics = table.ToIcs();
            Assert.IsFalse(string.IsNullOrEmpty(ics), "ICS string should not be null or empty!");
        }

        [TestMethod]
        public void Determining_block_duration_from_regular_block_should_succeed()
        {
            var time = new BlockStartTime(new[] { "07:45", "08:40", "09:45", "10:45", "11:50", "12:45" });
            Assert.AreEqual("RegularBlock", time.BlockType);
        }

        [TestMethod]
        public void Determining_block_duration_from_double_block_should_succeed()
        {
            var time = new BlockStartTime(new[] { "08:00:00", "10:00:00", "12:00:00", "13:45:00" });
            Assert.AreEqual("DoubleBlock", time.BlockType);
        }
    }
}
