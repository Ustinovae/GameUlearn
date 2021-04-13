using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    class Player
    {
        private Point location;
        private readonly int sizeStep;
        private bool holdFurniture;
        private Furniture takenFurniture;

        public Player(int x, int y, int sizeStep)
        {
            location.X = x;
            location.Y = y;
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
            location = new Point(direction.X * sizeStep, direction.Y * sizeStep);
        }

        public Point Location
        {
            get
            {
                return location;
            }
        }

        private bool InBounds(Point direction)
        {
            return Location.X + direction.X * sizeStep >= 0 && Location.X + direction.X * sizeStep < Game.mapSize.Width &&
                Location.Y + direction.Y * sizeStep >= 0 && Location.Y + direction.Y * sizeStep < Game.mapSize.Height;
        }
    }
}
