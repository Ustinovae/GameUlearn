using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    class Enemy : IEntity
    {
        private Point location;
        private readonly int sizeStep;

        public Enemy(int x, int y, int sizeStep)
        {
            location.X = x;
            location.Y = y;
            this.sizeStep = sizeStep;
        }

        public Point Location
        {
            get
            {
                return location;
            }
        }

        public void Move(Point direction)
        {
            if (!InBounds(direction))
                return;
            location = new Point(direction.X * sizeStep, direction.Y * sizeStep);
        }

        private bool InBounds(Point direction)
        {
            return location.X + direction.X * sizeStep >= 0 && location.X + direction.X * sizeStep < Game.mapSize.Width &&
                location.Y + direction.Y * sizeStep >= 0 && location.Y + direction.Y * sizeStep < Game.mapSize.Height;
        }
    }
}
