using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Timetable;

namespace Tests
{
    [TestClass]
    public class AlternateTableTests
    {
        private static DateTime _startDate = new DateTime(2018, 8, 27, 0, 0, 1, DateTimeKind.Utc);
        private static JannisTableA _startTable = new JannisTableA();
        private static JannisTableB _alternateTable = new JannisTableB();
        private WeeklyAlternateTable _table = new WeeklyAlternateTable(_startDate, _startTable, _alternateTable);

        [TestMethod]
        public void Same_week_should_show_same_table()
        {
            var table = _table.Get(new DateTime(2018, 8, 29, 0, 0, 1, DateTimeKind.Utc));

            Assert.IsInstanceOfType(table, typeof(JannisTableA), "Table is not of expected type JannisTableA!");
        }

        [TestMethod]
        public void Next_week_should_show_next_table()
        {
            var table = _table.Get(new DateTime(2018, 9, 5, 0, 0, 1, DateTimeKind.Utc));

            Assert.IsInstanceOfType(table, typeof(JannisTableB), "Table is not of expected type JannisTableB!");
        }

        [TestMethod]
        public void Week_after_next_week_should_show_initial_table()
        {
            var table = _table.Get(new DateTime(2018, 9, 11, 0, 0, 1, DateTimeKind.Utc));

            Assert.IsInstanceOfType(table, typeof(JannisTableA), "Table is not of expected type JannisTableA!");
        }
    }
}
