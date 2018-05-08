using System;
using System.Globalization;

namespace Timetable
{
    public class WeeklyAlternateTable
    {
        private readonly DateTime _startDate;
        private readonly Table _startTable;
        private readonly Table _alternateTable;

        public WeeklyAlternateTable(DateTime startDate, Table startTable, Table alternateTable)
        {
            _startDate = startDate;
            _startTable = startTable;
            _alternateTable = alternateTable;
        }

        public Table Get(DateTime date)
        {
            var week = new WeekOfYear();

            var diff = week.GetNumber(date) - week.GetNumber(_startDate);

            if (IsOdd(diff))
            {
                return _alternateTable;
            }
            else
            {
                return _startTable;
            }
        }

        private static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        private class WeekOfYear
        {
            private readonly DateTimeFormatInfo _info;

            public WeekOfYear() : this(DateTimeFormatInfo.CurrentInfo)
            {
            }

            public WeekOfYear(DateTimeFormatInfo info)
            {
                _info = info;
            }

            public int GetNumber(DateTime date)
            {
                var calendar = _info.Calendar;
                return calendar.GetWeekOfYear(date, _info.CalendarWeekRule, _info.FirstDayOfWeek);
            }
        }
    }
}
