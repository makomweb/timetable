using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Timetable.Models
{
    public class Subject
    {
        public string Name { get; }

        public Subject(string name)
        {
            Name = Name;
        }
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
        public RegularBlock(DateTime begin, string name) : this(begin, new Subject(name))
        {
        }

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
        public string Day => Caption;

        public IEnumerable<Item> Items { get; } = Enumerable.Empty<Item>();

        public DayColumn(string day, IEnumerable<Item> items = null)
        {
            Caption = day;
            Items = items;
        }
    }

    public class Table
    {
        private DataTable _table = new DataTable();
        
        public Table()
        {
            _table.Columns.Add("Stunde", typeof(int));
            _table.Columns.Add("Montag", typeof(string));
            _table.Columns.Add("Dienstag", typeof(string));
            _table.Columns.Add("Mittwoch", typeof(string));
            _table.Columns.Add("Donnerstag", typeof(string));
            _table.Columns.Add("Freitag", typeof(string));
            _table.Rows.Add(1, "Deutsch", "Sport", "Mathe", "Deutsch", "Sachkunde");
            _table.Rows.Add(2, "Musik", "Sport", "Sachkunde", "Mathe", "Englisch");
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(_table);
        }
    }
}
