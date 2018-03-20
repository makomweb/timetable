using System.Net;
using System.Net.Http;
using Timetable;

namespace WebApp.Controllers
{
    public static class CalendarResponseMessage
    {
        public static HttpResponseMessage From(params Table[] tables)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = CalendarContent.From(tables)
            };
        }
    }
}
