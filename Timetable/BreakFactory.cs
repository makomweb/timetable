namespace Timetable
{
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
}
