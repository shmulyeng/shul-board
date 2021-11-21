using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Data.Base
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset? LastUpdated { get; set; }
    }
}
