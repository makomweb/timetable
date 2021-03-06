﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Timetable;

namespace Tests
{
    [TestClass]
    public class FileTests
    {
        private Ics _ics = new Ics("Europe/Berlin");

        [TestMethod]
        public void Serialize_Tonis_table()
        {
            var table = new ToniTable();
            var ics = _ics.From(table);
            Assert.IsFalse(string.IsNullOrEmpty(ics), "ICS string should not be null or empty!");
        }

        [TestMethod]
        public void Serialize_Jannis_table()
        {
            var table = new JannisTableB();
            var ics = _ics.From(table);
            Assert.IsFalse(string.IsNullOrEmpty(ics), "ICS string should not be null or empty!");
        }

        [TestMethod]
        public void Serialize_Moritz_table()
        {
            var table = new MoritzTable();
            var ics = _ics.From(table);
            Assert.IsFalse(string.IsNullOrEmpty(ics), "ICS string should not be null or empty!");
        }
    }
}
