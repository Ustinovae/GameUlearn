using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public class LevelsManager
    {
        public Level CurrentLevel { get; private set; }

        private readonly Dictionary<int, Level> levels = new()
        {
            {
                0,
                new Level(Properties.Resources.FirstLevel, 0,
                new List<string> { "Пароль на сегодня: алло",
                    "Чтобы выбратся тебе нужно узнать пароль и ввести его. Поищи еще записки",
                    "На карте есть ящики. Ты можешь их двигать. на F хватаешь, на R отпускать",
                    "За углом находятся враги. Если подойдешь близко они погонятся за тобой. Если догонят, то ты проиграл",
                    "Управление WSAD. Если отойдешь от подсказки, она исчезнет. На карте есть ещё записки. Почитай их" },
                "алло")
            },
            {
                1,
                new Level(Properties.Resources.SecondLevel, 0,
                new List<string>{
                    "               Книга Шифрования \n" +
                    "Шифр Цезаря — это вид шифра подстановки, в котором каждый \n" +
                    "символ в открытом тексте заменяется символом, находящимся \n" +
                    "на некотором постоянном числе позиций левее или правее него \n" +
                    " в алфавите. Например, в шифре со сдвигом вправо на 3, \n" +
                    "А была бы заменена на Г, Б станет Д, и так далее.",
                    "Алфавит:\nА Б В Г Д Е Ё Ж\nЗ И Й К Л М Н О\nП Р С Т У Ф Х Ц\nЧ Ш Щ Ъ Ы Ь Э Ю\nЯ",
                    "Дата: 10.06.2021 (чт)\nВремя 17:29\nИнформация для охранников: смена шифрования\nСпособ шифрования: Код Цезаря\nСлово для шифрования: щлнгжг",
                    "Новое расписание:\nПн Вт Ср Чт Пт Сб Вс\n+7 -9 +5 +1 -3 +4 -6",
                    "Поставка груза завтра, в субботу в 10:00"},
                "цикада")
            },
        };

        public GameMap GetNextLevel()
        {
            if (CurrentLevel == null)
                return ChangeLevel(0);
            var num = CurrentLevel.NumberLevel + 1;
            if (!levels.ContainsKey(num))
                Restart();
            return ChangeLevel(num);
        }

        public GameMap Restart()
        {
            return GameMap.ParseFromText(CurrentLevel.Map, CurrentLevel.HintsText, CurrentLevel.Password);
        }

        private GameMap ChangeLevel(int Level)
        {
            CurrentLevel = levels[Level];
            return GameMap.ParseFromText(CurrentLevel.Map, CurrentLevel.HintsText, CurrentLevel.Password);
        }
    }
}
