using Microsoft.EntityFrameworkCore;
using shul_board.Data;
using shul_board.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Services
{
    public class AnnouncementService: BaseService<Announcement>
    {
        public AnnouncementService(ShulBoardContext context) : base(context)
        {
        }

        public async Task<List<Announcement>> GetActiveAnnouncementsAsync(DateTime dateTime)
        {
            var results = await  GetAllQuery(false)
                .OrderBy(a => a.Sort)
                .ToListAsync<Announcement>();

            var filtered = results.Where(a => a.IsActive(dateTime)).ToList();

            return filtered;
        }
    }
}
