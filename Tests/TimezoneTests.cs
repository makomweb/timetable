using Ical.Net.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class TimezoneTests
    {
        [TestMethod]
        public void Run()
        {
            string timezoneId = "Europe/Berlin";
            var lt = DateTime.UtcNow;
            var dt = new CalDateTime(lt, timezoneId); // Note the UTC clock is carried into the local timezone!
            var utc = dt.AsUtc;

            Assert.IsTrue(utc.Hour < dt.Hour, "UTC is expected to be smaller then local time!");
        }
    }
}
