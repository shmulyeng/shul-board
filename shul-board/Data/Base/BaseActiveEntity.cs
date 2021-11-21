using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Data.Base
{
    public abstract class BaseActiveEntity: BaseEntity
    {
        public bool Active { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }

        public bool IsActive(DateTime compareTo)
        {
            return this.Active && (!this.ActiveFrom.HasValue || this.ActiveFrom.Value < compareTo) && (!this.ActiveTo.HasValue || this.ActiveTo.Value > compareTo);
        }
    }
}
