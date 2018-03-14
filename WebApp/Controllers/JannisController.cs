﻿using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class JannisController : ApiController
    {
        private JannisTableA _table = new JannisTableA();

        public HttpResponseMessage Get()
        {
            try
            {
                return CalendarResponseMessage.From(_table);
            }
            catch (Exception ex)
            {
                return JsonResponseMessage.From(ex);
            }
        }
    }
}
