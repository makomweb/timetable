using System;

namespace Timetable
{
    public class TableSelector
    {
        private Table _jannis = CreateJannisTable();
        private Table _toni = new ToniTable();
        private Table _moritz = new MoritzTable();

        public Table Get(string id)
        {
            switch (id)
            {
                case "jannis": return _jannis;
                case "moritz": return _moritz;
                case "toni": return _toni;
                default: throw new NotImplementedException($"Case for ID '{id}' is not yet implemented!");
            }
        }

        private static Table CreateJannisTable()
        {
            var startDate = new DateTime(2018, 4, 16, 0, 0, 1, DateTimeKind.Utc).Date;
            var startTable = new JannisTableA();
            var alternateTable = new JannisTableB();
            var table = new AlternateTable(startDate, startTable, alternateTable);
            return table.Get(DateTime.UtcNow.Date);
        }
    }
}
