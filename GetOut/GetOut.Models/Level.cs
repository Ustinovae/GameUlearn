using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public class Level
    {
        public Level(string map, int numberLevel, List<string> hintsText, string password)
        {
            Map = map;
            NumberLevel = numberLevel;
            HintsText = hintsText;
            Password = password;

        }

        public int NumberLevel { get; }
        public string Map { get; }
        public List<string> HintsText { get; }
        public string Password { get; }
    }
}
