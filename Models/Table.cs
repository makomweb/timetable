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
        public TimeSpan Begin { get; }

        public TimeSpan Duration { get; }

        protected Item(TimeSpan begin, TimeSpan duration)
        {
            Begin = begin;
            Duration = duration;
        }

        public abstract string Type { get; }
    }
    
    public class Break : Item
    {
        public Break(TimeSpan begin, TimeSpan duration) : base(begin, duration)
        {
        }

        public override string Type => nameof(Break);
    }

    public class BlockStartTime
    {
        private List<TimeSpan> _startTimes = new List<TimeSpan>();

        public BlockStartTime(IEnumerable<string> startTimes)
        {
            foreach (var t in startTimes)
            {
                var ts = new TimeSpan(int.Parse(t.Split(':')[0]), int.Parse(t.Split(':')[1]), 0);

                _startTimes.Add(ts);
            }
        }

        public TimeSpan For(int index)
        {
            return _startTimes[index];
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

        public TimeSpan For(int index)
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

        protected Block(Subject subject, TimeSpan begin, TimeSpan duration) : base(begin, duration)
        {
            Subject = subject;
        }
    }
    
    public class RegularBlock : Block
    {
        public RegularBlock(TimeSpan begin, string name) : this(begin, new Subject(name))
        {
        }

        public RegularBlock(TimeSpan begin, Subject subject) : base(subject, begin, DefaultDuration)
        {
        }

        public static TimeSpan DefaultDuration = TimeSpan.FromMinutes(45);

        public override string Type => nameof(RegularBlock);
    }
    
    public class DoubleBlock : Block
    {
        public DoubleBlock(TimeSpan begin, Subject subject) : base(subject, begin, DefaultDuration)
        {
        }

        public static TimeSpan DefaultDuration = TimeSpan.FromMinutes(90);

        public override string Type => nameof(DoubleBlock);
    }

    public class BlockFactory
    {
        private readonly BlockStartTime _startTime;

        public BlockFactory(BlockStartTime startTime)
        {
            _startTime = startTime;
        }

        public RegularBlock CreateRegularBlock(int index, string subject)
        {
            return new RegularBlock(_startTime.For(index), subject);
        }

        public DoubleBlock CreateDoubleBlock(int index, string subject)
        {
            throw new NotImplementedException();
        }
    }

    public class BreakFactory
    {
        private readonly BreakStartTime _startTime;
        private readonly BreakDuration _duration;

        public BreakFactory(BreakStartTime startTime, BreakDuration duration)
        {
            _startTime = startTime;
            _duration = duration;
        }

        public Break BreakAfter(int index)
        {
            return new Break(_startTime.For(index), _duration.For(index));
        }
    }

    public class Table
    {
        private DataTable _table;
        
        public Table(DataTable table)
        {
            _table = table;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(_table);
        }
    }
}
