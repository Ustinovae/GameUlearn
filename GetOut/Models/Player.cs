using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GetOut.Models
{
    class Player
    {
        private Point location;
        private readonly int sizeStep;
        private bool holdFurniture;

        public Player(int x, int y, int sizeStep)
        {
            location.X = x;
            location.Y = y;
            this.sizeStep = sizeStep;
            holdFurniture = false;
        }

        public void TakeFurniture()
        {
            holdFurniture = true;
        }

        public void MoveTo(Point direction)
        {
            if (holdFurniture)

            location = new Point(direction.X * sizeStep, direction.Y * sizeStep);
        }

        public Point Location
        {
            get
            {
                return location;
            }
        }

        private bool 
    }
}
