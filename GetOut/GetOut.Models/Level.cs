using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public class Level
    {
        public int NumberLevel;
        public int[,] Map;

        public Level(int [,] map, int numberLevel)
        {
            Map = map;
            NumberLevel = numberLevel;
        }
    }
}
