using System.Data;
using Timetable;

namespace Tests
{
    public class TestTable : Table
    {
        public TestTable(BlockStartTime blockStart) : this(DayFactory.Create(blockStart))
        {
        }

        protected TestTable(DayFactory dayFactory) : base(CreateTable(dayFactory))
        {
        }

        private static DataTable CreateTable(DayFactory dayFactory)
        {
            var table = new DataTable();

            table.Columns.Add("Day", typeof(string));

            const int maxSubjectsPerDay = 6;
            const int maxItemsPerDay = maxSubjectsPerDay * 2; // every subject is followed by a break

            for (var i = 1; i < maxItemsPerDay; i++)
            {
                table.Columns.Add(i + "", typeof(Item));
            }

#if fale
            // Toni
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
#else
            // Jannis
            // A-Woche: 5. März 2018
#if false
            var monday = _dayFactory.CreateDay("Montag", "Deutsch", "Biologie", "Englisch");
            table.Rows.Add(monday);

            var tuesday = _dayFactory.CreateDay("Dienstag", "Mathe", "Französisch", "Chemie/Physik", "Deutsch");
            table.Rows.Add(tuesday);

            var wednesday = _dayFactory.CreateDay("Mittwoch", "Kunst", "Sport", "Musik");
            table.Rows.Add(wednesday);

            var thursday = _dayFactory.CreateDay("Donnerstag", "Deutsch", "Informatik", "Ethik", "Klassenleiterstunde");
            table.Rows.Add(thursday);

            var friday = _dayFactory.CreateDay("Freitag", "Erdkunde", "Französisch", "Mathe");
            table.Rows.Add(friday);
#endif

            // B-Woche: 12. März 2018
            var monday = dayFactory.CreateDay("Montag", null, "Deutsch", "Englisch", "Kunst");
            table.Rows.Add(monday);

            var tuesday = dayFactory.CreateDay("Dienstag", "Französisch", "Informatik", "Mathe");
            table.Rows.Add(tuesday);

            var wednesday = dayFactory.CreateDay("Mittwoch", "Englisch", "Musik", "Sport", "Französisch");
            table.Rows.Add(wednesday);

            var thursday = dayFactory.CreateDay("Donnerstag", "Chemie/Physik", "Geschichte", "Ethik");
            table.Rows.Add(thursday);

            var friday = dayFactory.CreateDay("Freitag", "Sport", "Biologie", "Mathematik");
            table.Rows.Add(friday);
#endif
            return table;
        }
    }
}
