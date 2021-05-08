using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Player : Entity
    {
        private readonly int speedValue = 5;

        public Player(int posX, int posY, int idleFrames, int runFrames, int attackFrames, int deathFrames, Image spriteSheet, int width, int height) 
            : base(posX, posY, idleFrames, runFrames, attackFrames, deathFrames, spriteSheet,  width, height)
        {
        }

        public void StartMove(int dirX, int dirY)
        {
            this.dirX = dirX;
            this.dirY = dirY;
        }

        public void StopMove()
        {
            dirX = 0;
            dirY = 0;
        }

        public void Act()
        {
            posX += dirX * speedValue;
            posY += dirY * speedValue;
        }

        public void PlayAnimation(Graphics g)
        {
            g.DrawImage(spriteSheet,
                new Rectangle(new Point(posX, posY),
                new Size(size.Width, size.Height)),
                size.Width * currentFrame,
                size.Height * currentAnimation,
                size.Width,
                size.Height,
                GraphicsUnit.Pixel);

            if (currentFrame < currentLimit - 1)
                currentFrame += 1;
            else currentFrame = 0;
        }

        public void SetAnimationConfiguration(int currentAnimation)
        {
            this.currentAnimation = currentAnimation;

            switch (currentAnimation)
            {
                case 0:
                    currentAnimation = idleFrames;
                    break;
                case 1:
                    currentAnimation = runFrames;
                    break;
                case 2:
                    currentAnimation = runFrames;
                    break;
                case 3:
                    currentAnimation = idleFrames;
                    break;
            }
        }
    }
}