using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Hint : Entity
    {
        private readonly string text;

        public Hint(int x, int y, string text) : base(x, y)
        {
            this.text = text;
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