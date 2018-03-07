using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Timetable.Models
{
    public class Subject
    {
        public string Name { get; }
    }

    public abstract class Item
    {
        public DateTime Begin { get; }

        public TimeSpan Duration { get; }

        protected Item(DateTime begin, TimeSpan duration)
        {
            Begin = begin;
            Duration = duration;
        }
    }

    public class Break : Item
    {
        public Break(DateTime begin, TimeSpan duration) : base(begin, duration)
        {
        }
    }

    public abstract class Block : Item
    {
        public Subject Subject { get; }

        protected Block(Subject subject, DateTime begin, TimeSpan duration) : base(begin, duration)
        {
            Subject = subject;
        }
    }

    public class RegularBlock : Block
    {
        public RegularBlock(DateTime begin, Subject subject) : base(subject, begin, TimeSpan.FromMinutes(45))
        {
        }
    }

    public class DoubleBlock : Block
    {
        public DoubleBlock(DateTime begin, Subject subject) : base(subject, begin, TimeSpan.FromMinutes(90))
        {
        }
    }

    public class DayColumn : DataColumn
    {
        public string Day { get; }

        public IEnumerable<Item> Items { get; } = Enumerable.Empty<Item>();

        public DayColumn(string day, IEnumerable<Item> items = null)
        {
            Day = day;
            Items = items;
        }
    }

    public class Table
    {
        private DataTable _table = new DataTable();

        public Table()
        {
            _table.Columns.Add(new DayColumn("Montag"));
            _table.Columns.Add(new DayColumn("Dienstag"));
            _table.Columns.Add(new DayColumn("Mittwoch"));
            _table.Columns.Add(new DayColumn("Donnerstag"));
            _table.Columns.Add(new DayColumn("Freitag"));
        }
    }
}
