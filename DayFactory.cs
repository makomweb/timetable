using Ninject;
using System.Collections.Generic;

namespace Timetable
{
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

        public object[] CreateDay(string name, params string[] subjects)
        {
            var items = new List<object> { name };

            for (var i = 0; i < subjects.Length; i++)
            {
                var subject = subjects[i];
                if (subject == null)
                {
                    continue;
                }

                items.Add(_blockFactory.CreateDoubleBlock(i, subject));

                // Check if a break has to be appended (there is no break after the last block)
                if (i < (subjects.Length - 1))
                {
                    items.Add(_breakFactory.BreakAfter(i));
                }
            }

            return items.ToArray();
        }
    }
}
