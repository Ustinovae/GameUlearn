using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Player : Entity
    {
        private readonly int idleFrames;
        private readonly int runFrames;
        private readonly int attackFrames;
        private readonly int deathFrames;

        private bool block = false;
        private int currentAnimation;
        private int currentFrame;
        private int currentLimit;
        public Furniture capturedFurniture;

        public Player(int posX, int posY, int idleFrames, int runFrames, int attackFrames, int deathFrames, Image spriteSheet, int width, int height, string name) 
            : base(posX, posY, new Size(width, height), spriteSheet, name)
        {
            this.idleFrames = idleFrames;
            this.runFrames = runFrames;
            this.attackFrames = attackFrames;
            this.deathFrames = deathFrames;
            SpeedValue = 5;
            currentLimit = idleFrames;
        }

        public bool Flip;
        public int SpeedValue { get; }

        public int DirX { get; private set; }
        public int DirY { get; private set; }

        public void StartMove(int dirX, int dirY)
        {
            DirX = dirX;
            DirY = dirY;
        }

        public void StopMove()
        {
            DirX = 0;
            DirY = 0;
        }

        public void TakeAnFurniture()
        {

            capturedFurniture = (Furniture)Physics.CheckContactWithObject(this, typeof(Furniture));
        }

        public void ReleaseObject()
        {
            capturedFurniture = null;
        }

        public void Block()
        {
            block = true;
        }

        public void Unblock()
        {
            block = false;
        }

        public void Act()
        {
            if (!block)
            {
                if (capturedFurniture != null)
                    capturedFurniture.Move(DirX * SpeedValue, DirY * SpeedValue);
                if ((capturedFurniture != null && !capturedFurniture.CheckCollide()) || capturedFurniture == null)
                {
                    PosX += DirX * SpeedValue;
                    PosY += DirY * SpeedValue;
                }
            }
        }

        public void PlayAnimation(Graphics g)
        {
            if (block)
                if (Flip)
                    SetAnimationConfiguration(3);
                else
                    SetAnimationConfiguration(0);
            g.DrawImage(Sprite,
                new Rectangle(new Point(base.PosX, PosY),
                new Size(Size.Width, Size.Height)),
                Size.Width * currentFrame,
                Size.Height * currentAnimation,
                Size.Width,
                Size.Height,
                GraphicsUnit.Pixel);

            if (currentFrame < currentLimit - 1) currentFrame += 1;
            else currentFrame = 0;
        }

        public void SetAnimationConfiguration(int currentAnimation)
        {
            this.currentAnimation = currentAnimation;

            switch (currentAnimation)
            {
                case 0:
                    currentLimit = idleFrames;
                    break;
                case 1:
                    currentLimit = runFrames;
                    break;
                case 2:
                    currentLimit = runFrames;
                    break;
                case 3:
                    currentLimit = idleFrames;
                    break;
            }
        }
    }
}