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
        public static int cellSize = 30;
        public static int[,] map = new int[mapHeight, mapWidth];
        public static Image spriteSheet;
        public static List<Entity> entitiesOnMap = new();

        public static void Intit()
        {
            map = GetMap();
            spriteSheet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), "EntitySprites\\WoodenFloor.png"));
        }

        private static int[,] GetMap()
        {
            return new int[,]{
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0 }
            };
        }

        public static void DrawMap(Graphics g)
        {
            for (var i = 0; i < mapWidth; i++)
            {
                for (var j = 0; j < mapHeight; j++)
                {
                    if (map[i, j] == 1)
                    {
                        //g.DrawImage(spriteSheet,
                        //    new Rectangle(new Point(i * cellSize, j * cellSize),
                        //    new Size(cellSize, cellSize)),
                        //    0, 0,
                        //    70, 70,
                        //    GraphicsUnit.Pixel);
                    }
                }
            }
        }
    }
}
