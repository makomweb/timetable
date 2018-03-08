using System.Collections.Generic;
using System.Data;
using Timetable;

namespace Tests
{
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
