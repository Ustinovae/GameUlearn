using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Furniture : Entity
    {
        private readonly int sizeStep; // надо будет сделать глобальную перемненну. чтобы можно было сылться из сущности на неё

        public Furniture(int x, int y, int sizeStep) : base(x, y)
        {
            this.sizeStep = sizeStep;
        }

        public void MoveTo(Point direction)
        {
            Location = new Point(direction.X * sizeStep, direction.Y * sizeStep);
        }
    }
}
