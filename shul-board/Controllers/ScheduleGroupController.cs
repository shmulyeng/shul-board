using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shul_board.Data;
using shul_board.Models;
using shul_board.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleGroupController : BaseEntityController<ScheduleGroup>
    {
        private ScheduleGroupService scheduleGroupService;
        public ScheduleGroupController(ScheduleGroupService service): base(service)
        {
            this.scheduleGroupService = service;
        }

        [HttpGet("Active")]
        public async Task<ActionResult<List<ScheduleGroup>>> GetActiveScheduleGroupsAsync()
        {
            return await GetActiveScheduleGroupsAsync(DateTime.Now);
        }


        [HttpGet("Active/{dateTime}")]
        public async Task<ActionResult<List<ScheduleGroup>>> GetActiveScheduleGroupsAsync(DateTime dateTime)
        {
            return await scheduleGroupService.GetActiveScheduleGroupsWithItemsAsync(dateTime);
        }




      


    }
}
