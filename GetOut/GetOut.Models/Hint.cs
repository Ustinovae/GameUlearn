﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Hint: Entity
    {
       // private readonly string text;
        private bool IsACtive = false;

        public Hint(int posX, int posY, Size size, Image sprite, string name) : base(posX, posY, size, sprite, name)
        {
            
        }

        //public string Text
        //{
        //    get
        //    {
        //        return text;
        //    }
        //}

        public bool GetStatus() =>
            IsACtive;

        public void Activate()
        {
            IsACtive = true;
        }

        public void Block()
        {
            IsACtive = false;
        }
    }
}