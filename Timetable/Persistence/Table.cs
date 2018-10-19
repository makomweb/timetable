namespace Timetable.Persistence
{
    public class Table
    {
        public string Id { get; set; }

        public string[] BlockStartTimes { get; set; }

        public Weekday[] Weekdays { get; set; }
    }
}
