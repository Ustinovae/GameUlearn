using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Entity
    {
        public Entity(int posX, int posY, Size size, string name)
        {
            PosX = posX;
            PosY = posY;
            Size = size;
            Name = name;
        }

        public Size Size { get; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string Name { get; }
    }
}
