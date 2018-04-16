using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class JannisController : ApiController
    {
#if true
        private Table _table = new JannisTableA();
#else
        private Table _table = new JannisTableB();
#endif

        public HttpResponseMessage Get()
        {
            try
            {
                return new CalendarResponseMessage("Europe/Berlin").From(_table);
            }
            catch (Exception ex)
            {
                return JsonResponseMessage.From(ex);
            }
        }
    }
}
