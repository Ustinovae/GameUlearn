using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Furniture : Entity
    {
        private bool collided = false;
        public Furniture(int posX, int posY, Size size, Image sprite, string name) :base(posX, posY, size, sprite, name) 
        {
        }

        public bool CheckCollide() =>
            collided;

        public void Move(int dirX, int dirY)
        {
            collided = Physics.IsCollide(this, dirX, dirY);
            if (!collided)
            {
                PosY += dirY;
                PosX += dirX;
            }
        }
    }
}
