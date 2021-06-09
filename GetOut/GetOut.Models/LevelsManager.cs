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

        public Dictionary<int, Level> Levels = new()
        {
            {
                0,
                new Level(Properties.Resources.FirstLevel, 0,
                new List<string> { "Чтобы выбратся тебе нужно узнать пароль и ввести его. Поищи еще записки",
                    "На карте есть ящики. Ты можешь их двигать на T хватаешь, на R отпускать",
                    "За углом находятся враги. Если подойдешь близко они погонятся за тобой. Если догонят, то ты проиграл",
                    "Управление WSAD. Если отойдешь от подсказкиона исчезнет. На карте есть ещё записки. Почитай их" },
                "Конь")},
            { 1, new Level(Properties.Resources.SecondLevel, 0, 
                new List<string>{ "Враг", "Пока", "Привет", "че по чем", "хз"},
                "Конь")
            },
            { 2, new Level(Properties.Resources.Level2, 1,
                new List<string>{ "EntitySprites\\exit.png","EntitySprites\\Hint21.png"},
                "ПАРИ") }
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
            return GameMap.ParseFromText(currentLevel.Map, currentLevel.HintsText, currentLevel.Password);
        }

        public GameMap Restart()
        {
            return GameMap.ParseFromText(currentLevel.Map, currentLevel.HintsText, currentLevel.Password);
        }
    }
}
//{
    //0, new Level(Properties.Resources.Level1, 0,
                  //new List<string> { "EntitySprites\\Hint3.png", "EntitySprites\\Hint5.png", "EntitySprites\\Hint.png", "EntitySprites\\exit.png", "EntitySprites\\Hint4.png" })
