using System;
using System.Drawing;

namespace GetOut.Models
{
    public class Enemy : Entity
    {
        private readonly int sizeStep;

        public Enemy(int x, int y, int sizeStep) : base(x, y)
        {
            this.sizeStep = sizeStep;
        }

        public void Move(Point direction)
        {
            if (!InBounds(direction))
                return;
            Location = new Point(direction.X * sizeStep, direction.Y * sizeStep);
        }

        private bool InBounds(Point direction)
        {
            return Location.X + direction.X * sizeStep >= 0 && Location.X + direction.X * sizeStep < Game.mapSize.Width &&
                Location.Y + direction.Y * sizeStep >= 0 && Location.Y + direction.Y * sizeStep < Game.mapSize.Height;
        }
    }
}
