using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace GetOut.Models
{
    public class GameMap
    {
        public const int CellSize = 30;

        private GameMap(List<Entity> entitiesOnMap, List<Hint> hintOnLevels, List<Enemy> enemisOnMap, int mapHeight, int mapWidth, Exit exit, Player player)
        {
            MapHeight = mapHeight;
            MapWidth = mapWidth;
            Win = false;
            Lose = false;
            EntitiesOnMap = entitiesOnMap;
            HintOnLevels = hintOnLevels;
            Exit = exit;
            Player = player;
            EnemisOnMap = enemisOnMap;
        }

        public static int MapHeight { get; set; }
        public static int MapWidth { get; set; }
        public List<Entity> EntitiesOnMap { get; set; }
        public List<Hint> HintOnLevels { get; set; }
        public List<Enemy> EnemisOnMap { get; set; }

        public Player Player { get; set; }
        public Enemy Enemy { get; set; }
        public Exit Exit { get; }

        public bool Win { get; set; }
        public bool Lose { get; set; }

        public static GameMap ParseFromText(string text, List<string> hintsText, string password)
        {
            var lines = text.Split('\n');
            return FromLines(lines, hintsText, password);
        }

        public static GameMap FromLines(string[] lines, List<string> hintsText, string password)
        {
            Player player = null;
            Exit exit = null;
            var entitiesOnMap = new List<Entity>();
            var hintOnLevels = new List<Hint>();
            var enimisOnMap = new List<Enemy>();
            for (var y = 0; y< lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            entitiesOnMap.Add(new Barrier(x * CellSize, y * CellSize, new Size(CellSize, CellSize), "Barrier"));
                            break;
                        case 'f':
                            entitiesOnMap.Add(new Furniture(x * CellSize, y * CellSize, new Size(CellSize, CellSize), "Furniture"));
                            break;
                        case 'h':
                            hintOnLevels.Add(new Hint(x * CellSize, y * CellSize, new Size( CellSize, CellSize), "Hint", hintsText[hintOnLevels.Count]));
                            break;
                        case 'e':
                            enimisOnMap.Add(new Enemy(x * CellSize, y * CellSize, CellSize, 2 * CellSize, "Enemy"));
                            //entitiesOnMap.Add(new Enemy(x * CellSize, y * CellSize, CellSize, 2 * CellSize, "Enemy"));
                            break;
                        case 'p':
                            player = new Player(x * CellSize, y * CellSize, CellSize, 2 * CellSize, "Player");
                            break;
                        case 'w':
                            exit = new Exit(x * CellSize, y * CellSize, CellSize, CellSize, "Exit", password);
                            break;
                    }
                }
            }
            return new GameMap(entitiesOnMap, hintOnLevels, enimisOnMap, lines.Length, lines[1].Length, exit, player);
        }

        public bool CheckWin()
        {
            Win = Player.PosX + Player.Size.Width > Exit.PosX &&
                    Player.PosX < Exit.PosX + CellSize &&
                    Player.PosY + Player.Size.Height > Exit.PosY &&
                    Player.PosY < Exit.PosY + 2 * CellSize;
            return Win;

        }

        public bool IsCollide(Entity entity, Point nextPoint)
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

        public bool CheckBoundaries(Entity entity, Point nextPoiny)
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
