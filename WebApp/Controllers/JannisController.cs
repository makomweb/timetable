using System.Net.Http;

namespace WebApp.Controllers
{
    public class JannisController : TimetableController
    {
        public HttpResponseMessage Get()
        {
            return Get("jannis");
        }
    }
}
