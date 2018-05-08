using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class JannisController : ApiController
    {
        private Table _table = CreateTable();

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

        private static Table CreateTable()
        {
            var startDate = new DateTime(2018, 4, 16, 0, 0, 1, DateTimeKind.Utc).Date;
            var startTable = new JannisTableA();
            var alternateTable = new JannisTableB();
            var table = new WeeklyAlternateTable(startDate, startTable, alternateTable);
            return table.Get(DateTime.UtcNow.Date);
        }
    }
}
