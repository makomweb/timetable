using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using Timetable.Models;

namespace Tests
{
    [TestClass]
    public class TableSerializationTests
    {
        [TestMethod]
        public void When_serializing_table_it_should_return_json()
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
    }

    public class TestTable : Table
    {
        private static readonly BlockStartTime _blockStart = new BlockStartTime(new[] { "07:45:00", "08:40:00", "09:45:00", "10:45:00", "11:50:00", "12:45:00" });
        private static readonly BreakStartTime _breakStart = new BreakStartTime(_blockStart, RegularBlock.DefaultDuration);
        private static readonly BreakDuration _breakDuration = new BreakDuration(_blockStart, RegularBlock.DefaultDuration, _breakStart);
        private static readonly BlockFactory _blockFactory = new BlockFactory(_blockStart);
        private static readonly BreakFactory _breakFactory = new BreakFactory(_breakStart, _breakDuration);
        private static readonly DayFactory _dayFactory = new DayFactory(_blockFactory, _breakFactory);

        public TestTable() : base(CreateTable())
        {
        }

        public class DayFactory
        {
            private readonly BlockFactory _blockFactory;
            private readonly BreakFactory _breakFactory;

            public DayFactory(BlockFactory blockFactory, BreakFactory breakFactory)
            {
                _blockFactory = blockFactory;
                _breakFactory = breakFactory;
            }

            public object[] CreateDay(string name, params string[] subjects)
            {
                var items = new List<object> { name };

                for (var i = 0; i < subjects.Length; i++)
                {
                    items.Add(_blockFactory.CreateRegularBlock(i, subjects[i]));

                    // Check if a break has to be appended (there is no break after the last block)
                    if (i < (subjects.Length - 1))
                    {
                        items.Add(_breakFactory.BreakAfter(i));
                    }
                }

                return items.ToArray();
            }
        }

        private static DataTable CreateTable()
        {
            var table = new DataTable();

            table.Columns.Add("Day", typeof(string));

            const int maxSubjectsPerDay = 6;
            const int maxItemsPerDay = maxSubjectsPerDay * 2; // subject is followed by a break

            for (var i = 1; i < maxItemsPerDay; i++)
            {
                table.Columns.Add(i + "", typeof(Item));
            }

            var monday = _dayFactory.CreateDay("Montag", "Deutsch", "Musik", "Mathe", "Kunst", "Sachkunde", "Lebenskunde");
            table.Rows.Add(monday);

            var tuesday = _dayFactory.CreateDay("Dienstag", "Sport", "Sport", "Englisch", "Sachkunde", "Mathe", "Deutsch");
            table.Rows.Add(tuesday);

            var wednesday = _dayFactory.CreateDay("Mittwoch", "Mathe", "Sachkunde", "Deutsch", "Deutsch", "Englisch");
            table.Rows.Add(wednesday);

            var thursday = _dayFactory.CreateDay("Donnerstag", "Deutsch", "Mathe", "Kunst", "Lebenskunde", "Sachkunde", "Musik");
            table.Rows.Add(thursday);

            var friday = _dayFactory.CreateDay("Freitag", "Sachkunde", "Englisch", "Sport","Mathe", "Deutsch", "Deutsch");
            table.Rows.Add(friday);

            return table;
        }
    }
}
