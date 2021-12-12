using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shul_board.Data;
using shul_board.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleItemController : BaseEntityController<ScheduleItem>
    {
        private ScheduleItemService scheduleItemService;
        public ScheduleItemController(ScheduleItemService service) : base(service)
        {
            this.scheduleItemService = service;
        }
    }
}
