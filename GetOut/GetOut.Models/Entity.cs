using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Entity
    {
        public int PosX;
        public int PosY;

        public Size Size;

        public Entity(int posX, int posY, Size size)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Size = size;
        }
    }
}
