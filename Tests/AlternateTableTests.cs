using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Timetable;

namespace Tests
{
    [TestClass]
    public class AlternateTableTests
    {
        private WeeklyAlternateTable _table;

        public AlternateTableTests()
        {
            var startDate = new DateTime(2018, 8, 27, 0, 0, 1, DateTimeKind.Utc);
            var startTable = new JannisTableA();
            var alternateTable = new JannisTableB();
            _table = new WeeklyAlternateTable(startDate, startTable, alternateTable);
        }

        [TestMethod]
        public void Same_week_should_show_same_table()
        {
            var dateSameWeek = new DateTime(2018, 8, 29, 0, 0, 1, DateTimeKind.Utc);
            var table = _table.Get(dateSameWeek);

            Assert.IsInstanceOfType(table, typeof(JannisTableA), "Table is not of expected type JannisTableA!");
        }

        [TestMethod]
        public void Next_week_should_show_next_table()
        {
            var dateNextWeek = new DateTime(2018, 9, 5, 0, 0, 1, DateTimeKind.Utc);
            var table = _table.Get(dateNextWeek);

            Assert.IsInstanceOfType(table, typeof(JannisTableB), "Table is not of expected type JannisTableB!");
        }

        [TestMethod]
        public void Week_after_next_week_should_show_initial_table()
        {
            var dateWeekAfterNextWeek = new DateTime(2018, 9, 11, 0, 0, 1, DateTimeKind.Utc);
            var table = _table.Get(dateWeekAfterNextWeek);

            Assert.IsInstanceOfType(table, typeof(JannisTableA), "Table is not of expected type JannisTableA!");
        }
    }
}
