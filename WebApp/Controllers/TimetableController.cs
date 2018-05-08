using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class TimetableController : ApiController
    {
        private TableSelector _selector = new TableSelector();

        public HttpResponseMessage Get(string id)
        {
            try
            {
                var table = _selector.Get(id);
                return new CalendarResponseMessage("Europe/Berlin").From(table);
            }
            catch (Exception ex)
            {
                return JsonResponseMessage.From(ex);
            }
        }
    }
}
