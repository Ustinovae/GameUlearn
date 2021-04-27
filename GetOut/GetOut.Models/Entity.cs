using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public class Entity
    {
        public int posX;
        public int posY;

        public int dirX;
        public int dirY;

        public int flip;

        public int idleFrames;
        public int runFrames;
        public int attackFrames;
        public int deathFrames;
        public bool isMoving;
        public Size size;

        public int currentAnimation;
        public int currentFrame;
        public int currentLimit;

        public Image spriteSheet;

        public Entity(int posX, int posY, int idleFrames, int runFrames, int attackFrames, int deathFrames, Image spriteSheet)
        {
            this.posX = posX;
            this.posY = posY;
            this.idleFrames = idleFrames;
            this.runFrames = runFrames;
            this.attackFrames = attackFrames;
            this.deathFrames = deathFrames;
            this.spriteSheet = spriteSheet;
            size = new Size(51, 100);
            currentAnimation = 0;
            currentFrame = 0;
            currentLimit = idleFrames;
            flip = 1;
        }

        public Entity(int posX, int posY, Image spriteSheet)
        {
            this.posX = posX;
            this.posY = posY;
            this.spriteSheet = spriteSheet;
        }
    }
}
