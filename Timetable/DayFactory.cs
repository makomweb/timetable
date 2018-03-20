using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timetable
{
    public abstract class Weekday
    {
        public string Name => GetType().Name.ToString();

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
            return Name == dayOfWeek.ToString();
        }

        public DateTime GetDate(DateTime startOfWeek)
        {
            var date = startOfWeek;

            switch (Name)
            {
                case "Monday": break;
                case "Tuesday": date = startOfWeek.AddDays(1); break;
                case "Wednesday": date = startOfWeek.AddDays(2); break;
                case "Thursday": date = startOfWeek.AddDays(3); break;
                case "Friday": date = startOfWeek.AddDays(4); break;
                default:
                    throw new NotSupportedException($"Unsupported day name '{Name}'!");
            }

            return date;
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
