using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class JannisController : ApiController
    {
        private JannisTableA _table = new JannisTableA();

        public Table Get()
        {
            return _table;
        }
    }
}
