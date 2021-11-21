using Microsoft.EntityFrameworkCore;
using shul_board.Data;
using shul_board.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Services
{
    public class ScheduleGroupService : BaseService<ScheduleGroup>
    {
        public ScheduleGroupService(ShulBoardContext context) : base(context)
        {
        }

        public async Task<List<ScheduleGroup>> GetActiveScheduleGroupsWithItemsAsync(DateTime dateTime)
        {
            var results = await GetAllQuery(false)
                .OrderBy(g => g.Sort)
                .Include(g => g.Items
                    .OrderBy(i => i.Sort)
                ).ToListAsync<ScheduleGroup>();

            var filtered = results.Where(g => g.IsActive(dateTime)).ToList();

            foreach (var group in filtered)
            {
                group.Items = group.Items.Where(i => i.IsActive(dateTime)).ToList();
            }

            return filtered;
        }
    }
}
