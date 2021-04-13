using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    class Hint : IEntity
    {
        private Point location;
        private readonly string text;

        public Hint(int x, int y, string text)
        {
            location.X = x;
            location.Y = y;
            this.text = text;
        }


        public Point Location
        {
            get
            {
                return location;
            }
        }

        public string Text
        {
            get 
            { 
                return text;
            }
        }
    }
}
