using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Player : Entity
    {
        private readonly int sizeStep;
        private bool holdFurniture;
        private Furniture takenFurniture;

        public Player(int x, int y, int sizeStep) : base(x, y)
        {
            this.sizeStep = sizeStep;
            holdFurniture = false;
        }

        public void TakeFurniture(Furniture furniture)
        {
            holdFurniture = true;
            takenFurniture = furniture;
        }

        public void LetGo()
        {
            holdFurniture = false;
            takenFurniture = null;
        }

        public void MoveTo(Point direction)
        {
            if (!InBounds(direction))
                return;
            if (holdFurniture)
                takenFurniture.MoveTo(direction);
            Location = new Point(direction.X * sizeStep, direction.Y * sizeStep);
        }

        private bool InBounds(Point direction)
        {
            return Location.X + direction.X * sizeStep >= 0 && Location.X + direction.X * sizeStep < Game.mapSize.Width &&
                Location.Y + direction.Y * sizeStep >= 0 && Location.Y + direction.Y * sizeStep < Game.mapSize.Height;
        }
    }
}