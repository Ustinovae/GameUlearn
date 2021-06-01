using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public class LevelsManager
    {
        public Level currentLevel;

        public Dictionary<int, Level> Levels = new Dictionary<int, Level>
        {
            { 1, new Level(Properties.Resources.TextFile1, 1, 
                new List<string>{ "EntitySprites\\Himt2.png", "EntitySprites\\Hint3.png","EntitySprites\\Hint.png"}) }
        };

        public Map GetNextLevel()
        {
            var num = currentLevel.NumberLevel + 1;
            if (!Levels.ContainsKey(num))
                Restart();
            return ChangeLevel(num);
        }

        public Map ChangeLevel(int Level)
        {
            currentLevel = Levels[Level];
            return Map.ParseFromText(currentLevel.Map, currentLevel.PathsToHints);
        }

        public Map Restart()
        {
            return Map.ParseFromText(currentLevel.Map, currentLevel.PathsToHints);
        }
    }
}
