using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Barrier : Entity
    {
        public Size Size;
        public Barrier(int posX, int posY, Image sprite, Size size) : base(posX, posY, sprite)
        {
            this.Size = size;
        }
    }
}