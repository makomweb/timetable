using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace Timetable.Models
{
    public class Subject
    {
        public string Name { get; }

        public Subject(string name)
        {
            Name = name;
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

        public abstract string Type { get; }
    }
    
    public class Break : Item
    {
        public Break(DateTime begin, TimeSpan duration) : base(begin, duration)
        {
        }

        public override string Type => nameof(Break);
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

        public RegularBlock(DateTime begin, Subject subject) : base(subject, begin, DefaultDuration)
        {
        }

        public static TimeSpan DefaultDuration = TimeSpan.FromMinutes(45);

        public override string Type => nameof(RegularBlock);
    }
    
    public class DoubleBlock : Block
    {
        public DoubleBlock(DateTime begin, Subject subject) : base(subject, begin, DefaultDuration)
        {
        }

        public static TimeSpan DefaultDuration = TimeSpan.FromMinutes(90);

        public override string Type => nameof(DoubleBlock);
    }

    public class Table
    {
        private DataTable _table = new DataTable();
        
        public Table()
        {
            _table.Columns.Add("Tag", typeof(string));
            _table.Columns.Add("1", typeof(Item));
            _table.Columns.Add("2", typeof(Item));
            _table.Columns.Add("3", typeof(Item));
            _table.Columns.Add("4", typeof(Item));
            _table.Columns.Add("5", typeof(Item));
            _table.Columns.Add("6", typeof(Item));
            _table.Columns.Add("7", typeof(Item));
            _table.Columns.Add("8", typeof(Item));
            //_table.Rows.Add(1, "Deutsch", "Sport", "Mathe", "Deutsch", "Sachkunde");
            //_table.Rows.Add(2, "Musik", "Sport", "Sachkunde", "Mathe", "Englisch");

            _table.Rows.Add("Montag", new RegularBlock(DateTime.Now, "Deutsch"), new Break(DateTime.Now.Add(RegularBlock.DefaultDuration), TimeSpan.FromMinutes(15)), new RegularBlock(DateTime.Now, "Sport"));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(_table);
        }
    }
}
