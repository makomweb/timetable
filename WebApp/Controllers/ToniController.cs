﻿using System;
using System.Net.Http;
using System.Web.Http;
using Timetable;

namespace WebApp.Controllers
{
    public class ToniController : ApiController
    {
        private ToniTable _table = new ToniTable();

        public HttpResponseMessage Get()
        {
            try
            {
                return new CalendarResponseMessage("Europe/Berlin", _table);
            }
            catch (Exception ex)
            {
                return new JsonResponseMessage(ex);
            }
        }
    }
}
