using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GetOut.Models
{
    public class Map
    {
        public const int CellSize = 30;

        private Map(List<Entity> entitiesOnMap, List<Hint> hintOnLevels, int indexEnemy, int mapHeight, int mapWidth)
        {
            MapHeight = mapHeight;
            MapWidth = mapWidth;
            Win = false;
            Lose = false;
            EntitiesOnMap = entitiesOnMap;
            HintOnLevels = hintOnLevels;
            if(indexEnemy != -1)
                enemy = (Enemy)entitiesOnMap[indexEnemy];
        }

        public static int MapHeight { get; set; }
        public static int MapWidth { get; set; }
        public static List<Entity> EntitiesOnMap { get; set; }
        public static List<Hint> HintOnLevels { get; set; }
        public static Player Player { get; set; }
        public Enemy enemy { get; set; }

        public bool Win { get; set; }
        public bool Lose { get; set; }

        public static Map ParseFromText(string text, List<string> pathsToHints)
        {
            var lines = text.Split('\n');
            return FromLines(lines, pathsToHints);
        }

        public static Map FromLines(string[] lines, List<string> pathsToHints)
        {
            var indexEnemy = -1;
            var entitiesOnMap = new List<Entity>();
            var hintOnLevels = new List<Hint>();
            for (var y = 0; y< lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    switch(lines[y][x])
                    {
                        case '#':
                            entitiesOnMap.Add(new Barrier(x * CellSize, y * CellSize, new Size(CellSize, CellSize), "Barrier"));
                            break;
                        case 'f':
                            entitiesOnMap.Add(new Furniture(x * CellSize, y * CellSize, new Size(CellSize, CellSize), "Furniture"));
                            break;
                        case 'h':
                            var image = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), pathsToHints[hintOnLevels.Count]));
                            hintOnLevels.Add(new Hint(x * CellSize, y * CellSize, new Size(image.Width, image.Height), image, "Hint"));
                            break;
                        case 'e':
                            indexEnemy = entitiesOnMap.Count();
                            entitiesOnMap.Add(new Enemy(x * CellSize, y * CellSize, CellSize, 2*CellSize, "Enemy"));
                            break;
                        case 'P':
                            Player = new Player(x * CellSize, y * CellSize, CellSize, 2*CellSize, "Player");
                            break;
                    }
                }
            }
            return new Map(entitiesOnMap, hintOnLevels, indexEnemy, lines.Length, lines[1].Length);
        }

        public static bool IsCollide(Entity entity, Point nextPoint)
        {
            if (CheckBoundaries(entity, nextPoint))
                return true;

            for (var i = 0; i < EntitiesOnMap.Count; i++)
            {
                var currentEntity = EntitiesOnMap[i];
                if (nextPoint.X + entity.Size.Width > currentEntity.PosX &&
                    nextPoint.X < currentEntity.PosX + currentEntity.Size.Width &&
                    nextPoint.Y + entity.Size.Height > currentEntity.PosY &&
                    nextPoint.Y < currentEntity.PosY + currentEntity.Size.Height
                    && entity != currentEntity)
                    return true;
            }
            return false;
        }

        public static bool CheckBoundaries(Entity entity, Point nextPoiny)
        {
            return nextPoiny.X < 0 || nextPoiny.X + entity.Size.Width > CellSize * MapWidth ||
                nextPoiny.Y < 0 || nextPoiny.Y + entity.Size.Height > CellSize * MapHeight;
        }

        public Entity CheckContactWithObject(Entity entity, string name)
        {
            for (var i = 0; i < EntitiesOnMap.Count; i++)
            {
                var currentEntity = EntitiesOnMap[i];
                if (currentEntity.Name == name)
                    if (((entity.PosX + entity.Size.Width == currentEntity.PosX || entity.PosX == currentEntity.PosX + currentEntity.Size.Width) &&
                        entity.PosY + entity.Size.Height <= currentEntity.PosY + currentEntity.Size.Height * 1.3 &&
                        entity.PosY >= currentEntity.PosY - currentEntity.Size.Height)
                        ||
                        (entity.PosY + entity.Size.Height == currentEntity.PosY || entity.PosY == currentEntity.PosY + currentEntity.Size.Height) &&
                        entity.PosX + entity.Size.Width <= currentEntity.PosX + currentEntity.Size.Width * 1.3 &&
                        entity.PosX >= currentEntity.PosX - currentEntity.Size.Width * 0.3)

                        return currentEntity;
            }
            return null;
        }

        public Hint HintTrigger(Entity entity)
        {
            for (var i = 0; i < HintOnLevels.Count; i++)
            {
                var currentEntity = HintOnLevels[i];
                if (entity.PosX + entity.Size.Width > currentEntity.PosX &&
                    entity.PosX < currentEntity.PosX + currentEntity.Size.Width &&
                    entity.PosY + entity.Size.Height > currentEntity.PosY &&
                    entity.PosY < currentEntity.PosY + currentEntity.Size.Height
                    && entity != currentEntity)
                    return currentEntity;
            }
            return null;
        }
    }
}
