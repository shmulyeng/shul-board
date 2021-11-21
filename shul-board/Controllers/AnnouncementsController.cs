using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shul_board.Data;
using shul_board.Services;

namespace shul_board.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AnnouncementsController : BaseEntityController<Announcement>
    {
        private AnnouncementService announcementService;

        public AnnouncementsController(AnnouncementService announcementService) : base(announcementService)
        {
            this.announcementService = announcementService;
        }

        [HttpGet("Active")]
        public async Task<ActionResult<List<Announcement>>> GetActiveAnnouncementsAsync()
        {
            return await GetActiveAnnouncementsAsync(DateTime.Now);
        }

        [HttpGet("Active/{dateTime}")]
        public async Task<ActionResult<List<Announcement>>> GetActiveAnnouncementsAsync(DateTime dateTime)
        {
            return await announcementService.GetActiveAnnouncementsAsync(dateTime);
        }
    }
}
