﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Hint: Entity
    {
        private readonly string text;

        public Hint(int posX, int posY, Size size, string text) : base(posX, posY, size)
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