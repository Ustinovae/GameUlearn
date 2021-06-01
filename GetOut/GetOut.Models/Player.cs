using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GetOut.Models
{
    public class Player : Entity
    {
        private Furniture capturedFurniture;

        public Player(int posX, int posY, int width, int height, string name)
            : base(posX, posY, new Size(width, height), name)
        {
            SpeedValue = 10;
        }

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

        public void TakeAnFurniture(Map map) =>
            capturedFurniture = (Furniture)map.CheckContactWithObject(this, "Furniture");

        public void ReleaseObject()=>    
            capturedFurniture = null;
        
        public void Act(Map map)
        {
            if (capturedFurniture != null)
                capturedFurniture.Move(DirX * SpeedValue, DirY * SpeedValue, map);
            if ((capturedFurniture != null && !capturedFurniture.CheckCollide()) || capturedFurniture == null)
            {
                PosX += DirX * SpeedValue;
                PosY += DirY * SpeedValue;
            }
        }
    }
}