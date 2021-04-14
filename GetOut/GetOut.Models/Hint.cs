using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Hint
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