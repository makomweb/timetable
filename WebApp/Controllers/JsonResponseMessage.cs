using System;
using System.Net;
using System.Net.Http;

namespace WebApp.Controllers
{
    public class JsonResponseMessage<T> : HttpResponseMessage
    {
        public JsonResponseMessage(T obj) : base(HttpStatusCode.OK)
        {
            Content = new JsonContent(obj);
        }

        public JsonResponseMessage(Exception exception) : base(HttpStatusCode.InternalServerError)
        {
            Content = new JsonContent(exception);
        }
    }
}
