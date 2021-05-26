using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public class Level
    {
        private bool win;

        public int NumberLevel;
        public int[,] Map;
        public List<string> PathsToHints;
        

        public Level(int [,] map, int numberLevel, List<string> pathsToHints)
        {
            win = false;
            Map = map;
            NumberLevel = numberLevel;
            PathsToHints = pathsToHints;
        }

        public bool GetStatus() =>
            win;

        public void PlayerWin() =>
            win = true;
    }
}
