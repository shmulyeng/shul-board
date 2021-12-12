using Microsoft.AspNetCore.Mvc;
using shul_board.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Zmanim;
using Zmanim.TimeZone;
using Zmanim.Utilities;
using Zmanim.TzDatebase;
using shul_board.Models;
using shul_board.Data;
using shul_board.Services;

namespace shul_board.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : ControllerBase
    {
        private ZmanimService zmanimService;

        public CalendarController(ZmanimService zmanimService)
        {
            this.zmanimService = zmanimService;
        }

        [HttpGet]
        public HebrewCalendarResponse GetCalendarInfo()
        {
            return GetCalendarInfo(DateTime.Now);
        }

        [HttpGet("{dateTime}")]
        public HebrewCalendarResponse GetCalendarInfo(DateTime dateTime)
        {

            var results = zmanimService.GetCalendar(dateTime);

            HebrewCalendarResponse response = new HebrewCalendarResponse()
            {
                Calendar = results
            };
            return response;
        }

    }
}