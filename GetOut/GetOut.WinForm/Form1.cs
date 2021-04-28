using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GetOut.Models;

namespace GetOutWinForm
{
    public partial class GetOutWinForm : Form
    {
        public Image playerSheet;
        public Player player;
        public Timer updateTimer = new();
        

        public GetOutWinForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            updateTimer.Interval = 50;
            updateTimer.Tick += new EventHandler(Update);

            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);

            Init();
        }

        private void Init()
        {
            Game.Intit();
            this.Width = Game.cellSize * Game.mapWidth;
            this.Height = Game.cellSize * Game.mapHeight;
            var directorySprites = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent;
            var pathToPlayer = new Bitmap(Path.Combine(directorySprites.ToString(), "EntitySprites\\Player.png"));
            playerSheet = pathToPlayer;
            player = new Player(100, 100, 2, 6, 3, 3, playerSheet, 25, 55);
            updateTimer.Start();
            this.BackgroundImage = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), "EntitySprites\\WoodenFloor.png"));

        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            player.dirX = 0;
            player.dirY = 0;
            player.isMoving = false;
            player.SetAnimationConfiguration(0);
        }

        private void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.dirY = -5;

                    player.SetAnimationConfiguration(1);
                    player.isMoving = true;
                    break;
                case Keys.S:
                    player.dirY = 5;
                    player.SetAnimationConfiguration(1);
                    player.isMoving = true;
                    break;
                case Keys.A:
                    player.dirX = -5;
                    player.flip = -1;
                    player.SetAnimationConfiguration(1);
                    player.isMoving = true;
                    break;
                case Keys.D:
                    player.dirX = 5;
                    player.flip = 1;
                    player.SetAnimationConfiguration(1);
                    player.isMoving = true;
                    break;
                case Keys.Space:
                    player.dirX = 0;
                    player.dirY = 0;
                    player.isMoving = false;
                    player.SetAnimationConfiguration(2);
                    break;
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            Game.DrawMap(g);
            player.PlayAnimation(g);
        }

        private void Update(object sender, EventArgs e)
        {
            if (player.isMoving)
                player.Move();
            Invalidate();
        }
    }
}
