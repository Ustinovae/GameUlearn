using System;
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
            for (var i = 0; i < Game.entitiesOnMap.Count; i++)
            {
                var currentEntity = Game.entitiesOnMap[i];
                if (entity.posX + entity.size.Width + dirX > currentEntity.posX &&
                    entity.posX + dirX < currentEntity.posX + currentEntity.Size.Width &&
                    entity.posY + entity.size.Height + dirY > currentEntity.posY &&
                    entity.posY + dirY < currentEntity.posY + currentEntity.Size.Height)
                    return true;
            }
            return false;
        }
    }
}
