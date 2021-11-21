using shul_board.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Models
{
    public class GetScheduleResponse
    {
        public List<ScheduleGroup> Groups { get; set; }
    }
}
