using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Player : Entity
    {
        private Furniture capturedFurniture;
        public int CurrentAnimation { get; set; }
        public int CurrentLimit { get; set; }
        public int CurrentFrame { get; set; }
        public bool Flip { get; set; }


        public Player(int posX, int posY, int width, int height, string name)
            : base(posX, posY, new Size(width, height), name)
        {
            SpeedValue = 10;
            CurrentLimit = 4;
            Flip = false;
        }

        public int SpeedValue { get; }

        public int DirX { get; private set; }
        public int DirY { get; private set; }

        public void StartMove(int dirX, int dirY)
        {
            DirX = dirX;
            DirY = dirY;
            if (dirX > 0)
                Flip = false;
            else if (dirX < 0)
                Flip = true;
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

        public void ReleaseObject()=>    
            capturedFurniture = null;
        
        public void Act(GameMap map)
        {
            if(!map.IsCollide(this, new Point(PosX+DirX*SpeedValue, PosY + DirY * SpeedValue)))
            {
                if (capturedFurniture != null)
                    capturedFurniture.Move(DirX * SpeedValue, DirY * SpeedValue, map);
                if (capturedFurniture != null && !capturedFurniture.CheckCollide())
                {
                    PosX += DirX * SpeedValue;// / 2;
                    PosY += DirY * SpeedValue;// / 2;
                }
                else if (capturedFurniture == null)
                {
                    PosX += DirX * SpeedValue;
                    PosY += DirY * SpeedValue;
                }
            }
        }

        public void SetAnimationConfiguration(int currentAnimation)
        {
            if (currentAnimation == 0 && TakeStatus())
                CurrentAnimation = 6;
            else if (currentAnimation == 1 && TakeStatus())
                CurrentAnimation = 7;
            else if (currentAnimation == 2 && TakeStatus())
                CurrentAnimation = 4;
            else if (currentAnimation == 3 && TakeStatus())
                CurrentAnimation = 5;
            else
                CurrentAnimation = currentAnimation;

            switch (currentAnimation)
            {
                case 0:
                    CurrentLimit = 4;
                    break;
                case 1:
                    CurrentLimit = 4;
                    break;
                case 2:
                    CurrentLimit = 6;
                    break;
                case 3:
                    CurrentLimit = 6;
                    break;
                case 8:
                    CurrentLimit = 1;
                    break;
            }
        }

        public void SetAnimationConfiguration(string animation)
        {
            if (animation == "State" && TakeStatus() && !Flip)
                CurrentAnimation = 6;
            else if (animation == "State" && TakeStatus() && Flip)
                CurrentAnimation = 7;
            else if (animation == "Run" && TakeStatus() && !Flip)
                CurrentAnimation = 4;
            else if (animation == "Run" && TakeStatus() && Flip)
                CurrentAnimation = 5;
            else if (animation == "State" && !Flip)
                CurrentAnimation = 0;
            else if (animation == "State" && Flip)
                CurrentAnimation = 1;
            else if (animation == "Run" && !Flip)
                CurrentAnimation = 2;
            else if (animation == "Run" && Flip)
                CurrentAnimation = 3;

            switch (animation)
            {
                case "State":
                    CurrentLimit = 4;
                    break;
                case "Run":
                    CurrentLimit = 6;
                    break;
                case "Deth":
                    CurrentLimit = 1;
                    break;
            }
        }
    }
}