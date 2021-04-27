using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Furniture : Entity
    {
        public Furniture(int posX, int posY, Image sprite):base(posX, posY, sprite) 
        {

        }

        public void Move()
        {
            posX += dirX;
            posY += dirY;
        }
    }
}
