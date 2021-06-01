using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Hint: Entity
    {
        private bool IsACtive = false;
        
        public Hint(int posX, int posY, Size size, Image sprite, string name) : base(posX, posY, size, name)
        {
            Sprite = sprite;
        }

        public Image Sprite { get; }

        public bool GetStatus() =>
            IsACtive;

        public void SetActive(bool s) =>
            IsACtive = s;
    }
}