using Ical.Net;
using Ical.Net.Serialization;

namespace Timetable
{
    public class Ics
    {
        private string _timezoneId;

        public Ics(string timezoneId)
        {
            _timezoneId = timezoneId;
        }

        public string From(params Table[] tables)
        {
            var calendar = new Calendar();

            foreach (var t in tables)
            {
                t.Externalize(calendar, _timezoneId);
            }

            var serializer = new CalendarSerializer();
            return serializer.SerializeToString(calendar);
        }
    }
}
