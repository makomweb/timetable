using System.Net;
using System.Net.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class CalendarResponseMessage : HttpResponseMessage
    {
        public CalendarResponseMessage(string timezoneId, params Table[] tables) : this(HttpStatusCode.OK)
        {
            Content = new CalendarContent(timezoneId, tables);
        }
    }
}
