using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartaDungeon.Models;

namespace SpartaDungeon
{
    public class JsonFormat
    {
        public Player Player { get; set; }
        public List<Gear> Gears { get; set; }
        public List<Goods> Goods { get; set; }

        public int Flag { get; set; }
    }
}
