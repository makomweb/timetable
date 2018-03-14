using System.Net.Http;
using System.Text;
using Timetable;

namespace WebApp.Controllers
{
    public class CalendarContent : StringContent
    {
        public const string MediaType = "text/ical";

        public static CalendarContent From(Table table)
        {
            var ics = table.ToIcs();
            return new CalendarContent(ics);
        }

        protected CalendarContent(string raw) : base(raw, Encoding.UTF8, MediaType)
        {
        }
    }
}
