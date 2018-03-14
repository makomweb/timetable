using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace WebApp.Controllers
{
    public class JsonContent : StringContent
    {
        public const string MediaType = "application/json";

        public static JsonContent From<T>(T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new JsonContent(json);
        }

        public static JsonContent From(Exception exception)
        {
            var obj = new
            {
                message = exception.Message,
                stack_trace = exception.StackTrace.ToString()
            };

            return From<dynamic>(obj);
        }

        protected JsonContent(string json) : base(json, Encoding.UTF8, MediaType)
        {
        }
    }
}
