using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Furniture : Entity
    {
        private bool collided = false;

        public Furniture(int posX, int posY, Size size, string name) :base(posX, posY, size, name) 
        {
        }

        public bool CheckCollide() =>
            collided;

        public void Move(int dirX, int dirY, Map map)
        {
            collided = Map.IsCollide(this, new Point(PosX + dirX, PosY + dirY));
            if (!collided)
            {
                PosY += dirY;
                PosX += dirX;
            }
        }
    }
}
