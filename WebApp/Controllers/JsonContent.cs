using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace WebApp.Controllers
{
    public class JsonContent<T> : StringContent
    {
        public const string MediaType = "application/json";

        public JsonContent(T obj) : this(CreateObj(obj))
        {
        }

        public JsonContent(Exception exception) : this(CreateObj(exception))
        {
        }

        protected JsonContent(dynamic json) : base(json, Encoding.UTF8, MediaType)
        {
        }

        private static dynamic CreateObj(Exception exception)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static dynamic CreateObj(Exception exception)
        {
            return new
            {
                message = exception.Message,
                stack_trace = exception.StackTrace.ToString()
            };
        }
    }
}
