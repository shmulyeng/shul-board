using shul_board.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Data
{
    public class ScheduleGroup: BaseActiveEntity
    {
        public string Name { get; set; }
        public int Sort { get; set; }

        [InverseProperty("Group")]
        public List<ScheduleItem> Items { get; set; }
    }
}
