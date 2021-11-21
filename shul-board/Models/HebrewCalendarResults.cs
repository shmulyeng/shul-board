using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Models
{
    public class HebrewCalendarResults
    {
        public DateTime EnglishDate { get; set; }
        public string HebrewDate { get; set; }
        public string Daf { get; set; }
        public string Parsha { get; set; }
        public string YomTov { get; internal set; }
    }

}
