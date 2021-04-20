using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    static class Game
    {
        public static Entity[,] Map;
        public static Size mapSize;
        public static int MapHeight => Map.GetLength(0);
        public static int MapWidth => Map.GetLength(0);
    }
}
