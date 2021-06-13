using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Player : Entity
    {
        private Furniture capturedFurniture;
        private string dirForAnimation;

        public Player(int posX, int posY, int width, int height, string name)
            : base(posX, posY, new Size(width, height), name)
        {
            SpeedValue = 10;
            CurrentLimit = 4;
            dirForAnimation = "down";
        }

        public int CurrentAnimation { get; private set; }
        public int CurrentLimit { get; private set; }
        public int CurrentFrame { get; set; }

        public int SpeedValue { get; }

        public int DirX { get; private set; }
        public int DirY { get; private set; }

        public void StartMove(int dirX, int dirY)
        {
            DirX = dirX;
            DirY = dirY;
            if (capturedFurniture == null)
            {
                if (dirX > 0)
                    dirForAnimation = "right";
                else if (dirX < 0)
                    dirForAnimation = "left";
                else if (dirY > 0)
                    dirForAnimation = "down";
                else if (dirY < 0)
                    dirForAnimation = "up";
            }
        }

        public void StopMove()
        {
            DirX = 0;
            DirY = 0;
        }

        public bool TakeStatus() =>
            capturedFurniture != null;

        public void TakeAnFurniture(GameMap map) =>
            capturedFurniture = (Furniture)map.CheckContactWithObject(this, "Furniture");

        public void ReleaseObject() =>
            capturedFurniture = null;

        public void Act(GameMap map)
        {
            if (!map.IsCollide(this, new Point(PosX + DirX * SpeedValue, PosY + DirY * SpeedValue)))
            {
                if (capturedFurniture != null)
                    capturedFurniture.Move(DirX * SpeedValue, DirY * SpeedValue, map);
                if (capturedFurniture != null && !capturedFurniture.CheckCollide() || capturedFurniture == null)
                {
                    PosX += DirX * SpeedValue;
                    PosY += DirY * SpeedValue;
                }
            }
        }

        public void SetAnimationConfiguration(string animation)
        {

            if (capturedFurniture != null)
            {
                if (PosX + Size.Width == capturedFurniture.PosX)
                    dirForAnimation = "right";
                else if (PosX == capturedFurniture.PosX + capturedFurniture.Size.Width)
                    dirForAnimation = "left";
                else if (PosY == capturedFurniture.PosY + capturedFurniture.Size.Height)
                    dirForAnimation = "up";
                else if (PosY + Size.Height == capturedFurniture.PosY)
                    dirForAnimation = "down";
            }
            switch (dirForAnimation)
            {
                case "up":
                    CurrentAnimation = 0;
                    break;
                case "left":
                    CurrentAnimation = 1;
                    break;
                case "down":
                    CurrentAnimation = 2;
                    break;
                case "right":
                    CurrentAnimation = 3;
                    break;
            }
            if (TakeStatus())
                CurrentAnimation += 8;
            else if (animation == "Run")
                CurrentAnimation += 4;
            else if (animation == "State")
                CurrentAnimation += 0;
            CurrentLimit = 4;
        }
    }
}