using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        public TestTable() : base(CreateTable())
        {
        }

        private static DataTable CreateTable()
        {
            var table = new DataTable();

            table.Columns.Add("Tag", typeof(string));

            for (var i = 1; i < 20; i++)
            {
                table.Columns.Add(i + "", typeof(Item));
            }

            table.Rows.Add("Montag",
                Block(0, "Deutsch"),
                Break(0),
                Block(1, "Musik"),
                Break(1),
                Block(2, "Mathe"),
                Break(2),
                Block(3, "Kunst"),
                Break(3),
                Block(4, "Sachkunde"),
                Break(4),
                Block(5, "Lebenskunde"));

            table.Rows.Add("Dienstag",
                Block(0, "Sport"),
                Break(0),
                Block(1, "Sport"),
                Break(1),
                Block(2, "Englisch"),
                Break(2),
                Block(3, "Sachkunde"),
                Break(3),
                Block(4, "Mathe"),
                Break(4),
                Block(5, "Deutsch"));

            table.Rows.Add("Mittwoch",
                Block(0, "Mathe"),
                Break(0),
                Block(1, "Sachkunde"),
                Break(1),
                Block(2, "Deutsch"),
                Break(2),
                Block(3, "Deutsch"),
                Break(3),
                Block(4, "Englisch"));

            table.Rows.Add("Donnerstag",
                Block(0, "Deutsch"),
                Break(0),
                Block(1, "Mathe"),
                Break(1),
                Block(2, "Kunst"),
                Break(2),
                Block(3, "Lebenskunde"),
                Break(3),
                Block(4, "Sachkunde"),
                Break(4),
                Block(5, "Musik"));

            table.Rows.Add("Freitag",
                Block(0, "Sachkunde"),
                Break(0),
                Block(1, "Englisch"),
                Break(1),
                Block(2, "Sport"),
                Break(2),
                Block(3, "Mathe"),
                Break(3),
                Block(4, "Deutsch"),
                Break(4),
                Block(5, "Deutsch"));

            return table;
        }

        private static Break Break(int index)
        {
            return _breakFactory.BreakAfter(index);
        }

        private static RegularBlock Block(int index, string subject)
        {
            return _blockFactory.CreateRegularBlock(index, subject);
        }
    }
}
