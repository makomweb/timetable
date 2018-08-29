using Ical.Net.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class TimezoneTests
    {
        [TestMethod]
        public void Converting_local_time_object_to_utc_should_give_expected_result()
        {
            string timezoneId = "Europe/Berlin";
            var lt = DateTime.UtcNow;
            var dt = new CalDateTime(lt, timezoneId); // Note the UTC clock is carried into the local timezone!
            var utc = dt.AsUtc;

            Assert.IsTrue(utc.Hour < dt.Hour, "UTC is expected to be smaller then local time!");
        }
    }
}
