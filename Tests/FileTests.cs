using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class FileTests
    {
        [TestMethod]
        public void Serialize_Tonis_table()
        {
            var table = new ToniTable();
            var ics = table.ToIcs();
            Assert.IsFalse(string.IsNullOrEmpty(ics), "ICS string should not be null or empty!");
        }

        [TestMethod]
        public void Serialize_Jannis_table()
        {
            var table = new JannisTableB();
            var ics = table.ToIcs();
            Assert.IsFalse(string.IsNullOrEmpty(ics), "ICS string should not be null or empty!");
        }

        [TestMethod]
        public void Serialize_Moritz_table()
        {
            var table = new MoritzTable();
            var ics = table.ToIcs();
            Assert.IsFalse(string.IsNullOrEmpty(ics), "ICS string should not be null or empty!");
        }
    }
}
