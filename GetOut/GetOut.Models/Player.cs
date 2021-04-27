using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Player : Entity
    {
        public Player(int posX, int posY, int idleFrames, int runFrames, int attackFrames, int deathFrames, Image spriteSheet) 
            : base(posX, posY, idleFrames, runFrames, attackFrames, deathFrames, spriteSheet)
        {

        }

        public void Move()
        {
            posX += dirX;
            posY += dirY;
        }

        public void PlayAnimation(Graphics g)
        {
            g.DrawImage(spriteSheet,
                new Rectangle(new Point(posX - flip * size.Width / 2, posY),
                new Size(flip * size.Width, size.Height)),
                size.Width * currentFrame + 26 * (currentFrame + 1),
                size.Height * currentAnimation + 26 * (currentAnimation + 1),
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
                    currentAnimation = attackFrames;
                    break;
                case 3:
                    currentAnimation = deathFrames;
                    break;
            }
        }
    }
}