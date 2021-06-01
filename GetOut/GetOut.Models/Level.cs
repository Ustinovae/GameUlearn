using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public class Level
    {
        public Level(string map, int numberLevel, List<string> pathsToHints)
        {
            Map = map;
            NumberLevel = numberLevel;
            PathsToHints = pathsToHints;
        }

        public int NumberLevel { get; }
        public string Map { get; }
        public List<string> PathsToHints { get; }
    }
}
