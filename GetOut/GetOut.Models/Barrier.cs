﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Barrier : Entity
    {
        public Barrier(int posX, int posY, Size size, string name) : base(posX, posY, size, name)
        {
        }
    }
}