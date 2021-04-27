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
        public Timer timer1 = new();

        public GetOutWinForm()
        {

            InitializeComponent();
            DoubleBuffered = true;
            timer1.Interval = 40;
            timer1.Tick += new EventHandler(Update);

            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);

            Init();

        }

        private void Init()
        {
            playerSheet = new Bitmap("D:\\C#\\GetOut\\GetOut\\GetOut.WinForm\\EntitySprites\\Player.png");

            player = new Player(100, 100, 2, 6, 3, 3, playerSheet);
            timer1.Start();
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
