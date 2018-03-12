using Ninject;
using System;
using System.Collections.Generic;

namespace Timetable
{
    public abstract class Weekday
    {
        public IEnumerable<Item> Items { get; private set; }

        public static Weekday Create(DayOfWeek dayOfWeek, IEnumerable<Item> items)
        {
            return Create(dayOfWeek.ToString(), items);
        }

        public static Weekday Create(string name, IEnumerable<Item> items)
        {
            Weekday day = null;
            switch (name)
            {
                case "Monday": day = new Monday(); break;
                case "Tuesday": day = new Tuesday(); break;
                case "Wednesday": day = new Wednesday(); break;
                case "Thursday": day = new Thursday(); break;
                case "Friday": day = new Friday(); break;
                default:
                    throw new NotSupportedException($"Unsupported weekday '{name}'!");
            }

            day.Items = items;
            return day;
        }

        public bool IsSame(DayOfWeek dayOfWeek)
        {
            return nameof(GetType) == dayOfWeek.ToString();
        }
    }

    public class Monday : Weekday { }

    public class Tuesday : Weekday { }

    public class Wednesday : Weekday { }

    public class Thursday : Weekday { }

    public class Friday : Weekday { }

    public class DayFactory
    {
        private readonly BlockFactory _blockFactory;
        private readonly BreakFactory _breakFactory;

        public static DayFactory Create(BlockStartTime blockStart)
        {
            var kernel = new StandardKernel();
            kernel.Bind<BlockStartTime>().ToConstant(blockStart);
            return kernel.Get<DayFactory>();
        }

        public DayFactory(BlockFactory blockFactory, BreakFactory breakFactory)
        {
            _blockFactory = blockFactory;
            _breakFactory = breakFactory;
        }

        public Weekday CreateWeekday(string name, params string[] subjects)
        {
            var items = new List<Item>();

            for (var i = 0; i < subjects.Length; i++)
            {
                var subject = subjects[i];
                if (subject == null)
                {
                    continue;
                }

                items.Add(_blockFactory.CreateBlock(i, subject));

                // Check if a break has to be appended (there is no break after the last block)
                if (i < (subjects.Length - 1))
                {
                    items.Add(_breakFactory.BreakAfter(i));
                }
            }

            return Weekday.Create(name, items);
        }
    }
}
