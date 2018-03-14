using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class JannisController : ApiController
    {
        private JannisTableA _table = new JannisTableA();

        public HttpResponseMessage Get()
        {
            return CreateMessage(_table);
        }

        private static HttpResponseMessage CreateMessage(JannisTableA table)
        {
            try
            {
                var json = table.ToJson();
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(ex.Message) };
            }
        }
    }
}
