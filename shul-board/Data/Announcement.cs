﻿using shul_board.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Data
{
    public class Announcement : BaseActiveEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }

    }
}
