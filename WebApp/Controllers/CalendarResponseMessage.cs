using System.Net;
using System.Net.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class CalendarResponseMessage
    {
        private readonly string _timezoneId;

        public CalendarResponseMessage(string timezoneId)
        {
            _timezoneId = timezoneId;
        }

        public HttpResponseMessage From(params Table[] tables)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = CalendarContent.From(_timezoneId, tables)
            };
        }
    }
}
