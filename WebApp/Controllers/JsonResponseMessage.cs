using System;
using System.Net;
using System.Net.Http;

namespace WebApp.Controllers
{
    public static class JsonResponseMessage
    {
        public static HttpResponseMessage From<T>(T obj)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.From(obj)
            };
        }

        public static HttpResponseMessage From(Exception exception)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = JsonContent.From(exception)
            };
        }
    }
}
