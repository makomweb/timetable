using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class KidsController : ApiController
    {
#if true
        private Table _jannis = new JannisTableA();
#else
        private Table _jannis = new JannisTableB();
#endif
        private Table _toni = new ToniTable();
        private Table _moritz = new MoritzTable();

        public HttpResponseMessage Get()
        {
            try
            {
                return CalendarResponseMessage.From(_jannis, _toni, _moritz);
            }
            catch (Exception ex)
            {
                return JsonResponseMessage.From(ex);
            }
        }
    }
}
