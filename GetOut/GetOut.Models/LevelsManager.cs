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
            { 0, new Level(Properties.Resources.Level1, 0, 
                new List<string>{ "EntitySprites\\Hint3.png", "EntitySprites\\Hint5.png", "EntitySprites\\Hint.png", "EntitySprites\\exit.png", "EntitySprites\\Hint4.png"}) },
            { 1, new Level(Properties.Resources.Level2, 1,
                new List<string>{ "EntitySprites\\exit.png","EntitySprites\\Hint21.png"}) }
        };

        public GameMap GetNextLevel()
        {
            if (currentLevel == null)
                return ChangeLevel(0);
            var num = currentLevel.NumberLevel + 1;
            if (!Levels.ContainsKey(num))
                Restart();
            return ChangeLevel(num);
        }

        public GameMap ChangeLevel(int Level)
        {
            currentLevel = Levels[Level];
            return GameMap.ParseFromText(currentLevel.Map, currentLevel.PathsToHints);
        }

        public GameMap Restart()
        {
            return GameMap.ParseFromText(currentLevel.Map, currentLevel.PathsToHints);
        }
    }
}
