using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    class Hint
    {
        private Point location;

        public Hint(int x, int y)
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
