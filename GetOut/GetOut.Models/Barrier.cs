using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Barrier
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