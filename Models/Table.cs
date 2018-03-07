using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

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

    public class BlockStartTime
    {
        private List<DateTime> _startTimes = new List<DateTime>();

        public BlockStartTime(IEnumerable<string> startTimes)
        {
            const string format = "hh:mm:ss";

            foreach (var t in startTimes)
            {
                _startTimes.Add(DateTime.ParseExact(t, format, CultureInfo.InvariantCulture));
            }
        }

        public TimeSpan TimeFor(int index)
        {
            return _startTimes[index].TimeOfDay;
        }

        public DateTime For(int index)
        {
            var b = TimeFor(index);
            return Convert.ToDateTime(b.ToString());
        }
    }

    public class BreakStartTime
    {
        private readonly BlockStartTime _blockStartTime;
        private readonly TimeSpan _blockDuration;

        public BreakStartTime(BlockStartTime blockStartTime, TimeSpan blockDuration)
        {
            _blockStartTime = blockStartTime;
            _blockDuration = blockDuration;
        }

        public DateTime For(int index)
        {
            var block = _blockStartTime.For(index);
            return block.Add(_blockDuration);
        }
    }

    public class BreakDuration
    {
        private readonly BlockStartTime _blockStartTime;
        private readonly TimeSpan _blockDuration;
        private readonly BreakStartTime _breakStart;

        public BreakDuration(BlockStartTime blockStartTime, TimeSpan blockDuration, BreakStartTime breakStart)
        {
            _blockStartTime = blockStartTime;
            _blockDuration = blockDuration;
            _breakStart = breakStart;
        }

        public TimeSpan For(int index)
        {
            var breakStart = _breakStart.For(index);
            var nextBlock = _blockStartTime.For(index + 1);
            return nextBlock.Subtract(breakStart);
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
            _table.Columns.Add("9", typeof(Item));
            _table.Columns.Add("10", typeof(Item));
            _table.Columns.Add("11", typeof(Item));
            
            var blockStart = new BlockStartTime(new[] { "07:55:00", "08:40:00", "09:45:00", "10:45:00", "11:50:00", "12:45:00" });
            var breakStart = new BreakStartTime(blockStart, RegularBlock.DefaultDuration);
            var breakDuration = new BreakDuration(blockStart, RegularBlock.DefaultDuration, breakStart);

            _table.Rows.Add("Montag", 
                new RegularBlock(blockStart.For(0), "Deutsch"),
                new Break(breakStart.For(0), breakDuration.For(0)),
                new RegularBlock(blockStart.For(1), "Musik"),
                new Break(breakStart.For(1), breakDuration.For(1)),
                new RegularBlock(blockStart.For(2), "Mathe"),
                new Break(breakStart.For(2), breakDuration.For(2)),
                new RegularBlock(blockStart.For(3), "Kunst"),
                new Break(breakStart.For(3), breakDuration.For(3)),
                new RegularBlock(blockStart.For(4), "Sachkunde"),
                new Break(breakStart.For(4), breakDuration.For(4)),
                new RegularBlock(blockStart.For(5), "Lebenskunde"));

            _table.Rows.Add("Dienstag",
                new RegularBlock(blockStart.For(0), "Sport"),
                new Break(breakStart.For(0), breakDuration.For(0)),
                new RegularBlock(blockStart.For(1), "Sport"),
                new Break(breakStart.For(1), breakDuration.For(1)),
                new RegularBlock(blockStart.For(2), "Englisch"),
                new Break(breakStart.For(2), breakDuration.For(2)),
                new RegularBlock(blockStart.For(3), "Sachkunde"),
                new Break(breakStart.For(3), breakDuration.For(3)),
                new RegularBlock(blockStart.For(4), "Mathe"),
                new Break(breakStart.For(4), breakDuration.For(4)),
                new RegularBlock(blockStart.For(5), "Deutsch"));

            _table.Rows.Add("Mittwoch",
                new RegularBlock(blockStart.For(0), "Mathe"),
                new Break(breakStart.For(0), breakDuration.For(0)),
                new RegularBlock(blockStart.For(1), "Sachkunde"),
                new Break(breakStart.For(1), breakDuration.For(1)),
                new RegularBlock(blockStart.For(2), "Deutsch"),
                new Break(breakStart.For(2), breakDuration.For(2)),
                new RegularBlock(blockStart.For(3), "Deutsch"),
                new Break(breakStart.For(3), breakDuration.For(3)),
                new RegularBlock(blockStart.For(4), "Englisch"));

            _table.Rows.Add("Donnerstag",
                new RegularBlock(blockStart.For(0), "Deutsch"),
                new Break(breakStart.For(0), breakDuration.For(0)),
                new RegularBlock(blockStart.For(1), "Mathe"),
                new Break(breakStart.For(1), breakDuration.For(1)),
                new RegularBlock(blockStart.For(2), "Kunst"),
                new Break(breakStart.For(2), breakDuration.For(2)),
                new RegularBlock(blockStart.For(3), "Lebenskunde"),
                new Break(breakStart.For(3), breakDuration.For(3)),
                new RegularBlock(blockStart.For(4), "Sachkunde"),
                new Break(breakStart.For(4), breakDuration.For(4)),
                new RegularBlock(blockStart.For(5), "Musik"));

            _table.Rows.Add("Freitag",
                new RegularBlock(blockStart.For(0), "Sachkunde"),
                new Break(breakStart.For(0), breakDuration.For(0)),
                new RegularBlock(blockStart.For(1), "Englisch"),
                new Break(breakStart.For(1), breakDuration.For(1)),
                new RegularBlock(blockStart.For(2), "Sport"),
                new Break(breakStart.For(2), breakDuration.For(2)),
                new RegularBlock(blockStart.For(3), "Mathe"),
                new Break(breakStart.For(3), breakDuration.For(3)),
                new RegularBlock(blockStart.For(4), "Deutsch"),
                new Break(breakStart.For(4), breakDuration.For(4)),
                new RegularBlock(blockStart.For(5), "Deutsch"));
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(_table);
        }
    }
}
