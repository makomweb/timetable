using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class JannisController : ApiController
    {
#if false
        private Table _table = new JannisTableA();
#else
        private Table _table = new JannisTableB();
#endif

        public HttpResponseMessage Get()
        {
            try
            {
                return CalendarResponseMessage.From(_table);
            }
            catch (Exception ex)
            {
                return JsonResponseMessage.From(ex);
            }
        }
    }

    public class JannisJsonController : ApiController
    {
#if false
        private Table _table = new JannisTableA();
#else
        private Table _table = new JannisTableB();
#endif

        public HttpResponseMessage Get()
        {
            try
            {
                return JsonResponseMessage.From(_table);
            }
            catch (Exception ex)
            {
                return JsonResponseMessage.From(ex);
            }
        }
    }
}
