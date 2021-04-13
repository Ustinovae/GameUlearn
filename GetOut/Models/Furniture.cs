using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    class Furniture
    {
        private Point location;
        private readonly int sizeStep; // надо будет сделать глобальную перемненну. чтобы можно было сылться из сущности на неё

        public Furniture(int x, int y, int sizeStep)
        {
            location.X = x;
            location.Y = y;
            this.sizeStep = sizeStep;
        }


        public void MoveTo(Point direction)
        {
            location = new Point(direction.X * sizeStep, direction.Y * sizeStep);
        }

        public Point Location
        {
            get
            {
                return location;
            }
        }
    }
}
