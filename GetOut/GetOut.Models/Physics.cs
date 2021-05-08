﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public static class Physics
    {
        public static bool IsCollide(Entity entity, int dirX, int dirY)
        {
            if (CheckBoundaries(entity, dirX, dirY))
                return true;

            for (var i = 0; i < Game.entitiesOnMap.Count; i++)
            {
                var currentEntity = Game.entitiesOnMap[i];
                if (entity.PosX + entity.Size.Width + dirX > currentEntity.PosX &&
                    entity.PosX + dirX < currentEntity.PosX + currentEntity.Size.Width &&
                    entity.PosY + entity.Size.Height + dirY > currentEntity.PosY &&
                    entity.PosY + dirY < currentEntity.PosY + currentEntity.Size.Height)
                    return true;
            }
            return false;
        }

        public static bool CheckBoundaries(Entity entity, int dirX, int dirY)
        {
            return entity.PosX + dirX * 5<= 0 || entity.PosX + dirX*5 >= Game.cellSize * (Game.mapWidth - 1) ||
                entity.PosY + dirY*5 <= 0 || entity.PosY + dirY*5 + entity.Size.Height >= Game.cellSize *Game.mapHeight ;
        }
    }
}
