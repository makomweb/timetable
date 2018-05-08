using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class KidsController : ApiController
    {
        private Table _jannis = CreateJannisTable();
        private Table _toni = new ToniTable();
        private Table _moritz = new MoritzTable();

        public HttpResponseMessage Get()
        {
            try
            {
                return new CalendarResponseMessage("Europe/Berlin").From(_jannis, _toni, _moritz);
            }
            catch (Exception ex)
            {
                return JsonResponseMessage.From(ex);
            }
        }
        private static Table CreateJannisTable()
        {
            var startDate = new DateTime(2018, 4, 16, 0, 0, 1, DateTimeKind.Utc).Date;
            var startTable = new JannisTableA();
            var alternateTable = new JannisTableB();
            var table = new WeeklyAlternateTable(startDate, startTable, alternateTable);
            return table.Get(DateTime.UtcNow.Date);
        }
    }
}
