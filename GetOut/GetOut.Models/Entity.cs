using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Entity
    {
        public int PosX;
        public int PosY;
        public Image Sprite { get; }
        private string Name;

        public Size Size;

        public Entity(int posX, int posY, Size size, Image sprite, string name)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Size = size;
            Sprite = sprite;
            Name = name;
        }

        public string GetName() =>
            Name;
    }
}
