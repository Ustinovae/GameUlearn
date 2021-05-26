using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GetOut.Models
{
    public static class Game
    {
        public const int mapHeight = 20;
        public const int mapWidth = 20;
        public const int cellSize = 30;
        public static int[,] map;
        private static Image spriteSheet;
        private static Image spriteChest;
        public static List<Entity> entitiesOnMap;
        public static List<Hint> hintOnLevels;
        public static List<string> pathsToHints;

        public static void Intit()
        {
            spriteSheet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), "EntitySprites\\barier.png"));
            spriteChest = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), "EntitySprites\\chest.png"));

            InitMap();
        }

        public static void DrawMap(Graphics g)
        {
            foreach (var obj in entitiesOnMap)
            {
                    g.DrawImage(obj.Sprite, new Point(obj.PosX, obj.PosY));   
            }
            foreach(var hint in hintOnLevels)
            {
                if (hint.GetStatus())
                    g.DrawImage(hint.Sprite, new Point(hint.PosX, hint.PosY));
            }
        }

        public static void InitMap()
        {
            entitiesOnMap = new List<Entity>();
            hintOnLevels = new List<Hint>();
            for (var i = 0; i < mapHeight; i++)
            {
                for (var j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 1)
                    {
                        entitiesOnMap.Add(new Barrier(j * cellSize, i * cellSize, new Size(cellSize, cellSize), spriteSheet, "Barrier"));
                    }
                    if (map[i, j] == 2)
                    {
                        entitiesOnMap.Add(new Furniture(j * cellSize, i * cellSize, new Size(cellSize, cellSize), spriteChest, "Furniture"));
                    }
                    if (map[i, j] == 3)
                    {
                        var image = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), pathsToHints[hintOnLevels.Count]));
                        hintOnLevels.Add(new Hint(j * cellSize, i * cellSize, new Size(image.Width, image.Height), image, "Hint"));
                    }
                }
            }
        }
    }
}
