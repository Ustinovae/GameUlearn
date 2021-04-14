using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Player
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

        public Point Location
        {
            get
            {
                return location;
            }
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

        private bool InBounds(Point direction)
        {
            return location.X + direction.X * sizeStep >= 0 && location.X + direction.X * sizeStep < Game.mapSize.Width &&
                location.Y + direction.Y * sizeStep >= 0 && location.Y + direction.Y * sizeStep < Game.mapSize.Height;
        }
    }
}