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
    public class ZmanimController : ControllerBase
    {
        private ZmanimService zmanimService;

        public ZmanimController(ZmanimService zmanimService)
        {
            this.zmanimService =zmanimService;
        }

        [HttpGet]
        public ZmanimResponse GetZmanim()
        {
            return GetZmanim(DateTime.Now);
        }

        [HttpGet("{dateTime}")]
        public ZmanimResponse GetZmanim(DateTime dateTime)
        {

            var results = zmanimService.GetZmanim(dateTime);

            ZmanimResponse response = new ZmanimResponse()
            {
                Zmanim = results
            };
            return response;
        }
    }
}