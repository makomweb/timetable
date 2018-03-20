using Ical.Net;
using Ical.Net.Serialization;

namespace Timetable
{
    public static class Ics
    {
        public static string From(params Table[] tables)
        {
            var calendar = new Calendar();
            foreach (var t in tables)
            {
                t.Externalize(calendar);
            }
            var serializer = new CalendarSerializer();
            return serializer.SerializeToString(calendar);
        }
    }
}
