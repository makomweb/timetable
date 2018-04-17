using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class JannisController : ApiController
    {
        private static DateTime _startDate = new DateTime(2018, 4, 16, 0, 0, 1, DateTimeKind.Utc).Date;
        private static JannisTableA _startTable = new JannisTableA();
        private static JannisTableB _alternateTable = new JannisTableB();
        private AlternateTable _table = new AlternateTable(_startDate, _startTable, _alternateTable);
        
        public HttpResponseMessage Get()
        {
            var table = _table.Get(DateTime.UtcNow.Date);

            try
            {
                return new CalendarResponseMessage("Europe/Berlin").From(table);
            }
            catch (Exception ex)
            {
                return JsonResponseMessage.From(ex);
            }
        }
    }
}
