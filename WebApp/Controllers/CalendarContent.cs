using System.Net.Http;
using System.Text;
using Timetable;

namespace WebApp.Controllers
{
    public class CalendarContent : StringContent
    {
        public const string MediaType = "text/ical";

        public CalendarContent(string timezoneId, params Table[] tables)
		: this(new Ics(timezoneId).From(tables))
        {
        }

        protected CalendarContent(string raw) : base(raw, Encoding.UTF8, MediaType)
        {
        }
    }
}
