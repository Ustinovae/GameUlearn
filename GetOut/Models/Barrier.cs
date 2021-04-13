using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace GetOut.Models
{
    class Barrier : IEntity
    {
        private Point location;

        public Barrier(int x, int y)
        {
            location.X = x;
            location.Y = y;
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
