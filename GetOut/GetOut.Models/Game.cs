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
        public static List<Barrier> entitiesOnMap;

        public static void Intit()
        {
            map = GetMap();
            spriteSheet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), "EntitySprites\\barier.png"));
            entitiesOnMap = new List<Barrier>();
        }

        private static int[,] GetMap()
        {
            return new int[,]{
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, 1 },
                { 1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 1 },
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1, 1 }
            };
        }

        public static void DrawMap(Graphics g)
        {
            for (var i = 0; i < mapHeight; i++)
            {
                for (var j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 1)
                    {
                        g.DrawImage(spriteSheet, new Point(j*cellSize, i*cellSize));
                        entitiesOnMap.Add(new Barrier(j * cellSize, i * cellSize, null, new Size(cellSize, cellSize)));
                    }
                }
            }
        }
    }
}
