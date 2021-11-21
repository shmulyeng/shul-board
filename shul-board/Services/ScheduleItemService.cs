using Microsoft.EntityFrameworkCore;
using shul_board.Data;
using shul_board.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Services
{
    public class ScheduleItemService : BaseService<ScheduleItem>
    {
        public ScheduleItemService(ShulBoardContext context) : base(context)
        {
        }





    }
}
