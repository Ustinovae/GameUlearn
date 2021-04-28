using System;
using System.Drawing;

namespace GetOut.Models
{
    public class Enemy: Player
    {
        public Enemy(int posX, int posY, int idleFrames, int runFrames, int attackFrames, int deathFrames, Image spriteSheet, int width, int height)
            : base(posX, posY, idleFrames, runFrames, attackFrames, deathFrames, spriteSheet, width, height)
        {

        }
    }
}
