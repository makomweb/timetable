using System.Net.Http;
using System.Text;
using Timetable;

namespace WebApp.Controllers
{
    public class CalendarContent : StringContent
    {
        public const string MediaType = "text/ical";

        public static CalendarContent From(string timezoneId, params Table[] tables)
        {
            var ics = new Ics(timezoneId);
            return new CalendarContent(ics.From(tables));
        }

        protected CalendarContent(string raw) : base(raw, Encoding.UTF8, MediaType)
        {
        }
    }
}
