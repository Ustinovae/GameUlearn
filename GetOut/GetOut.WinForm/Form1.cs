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
        public LevelsManager levelsManager;


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
            levelsManager = new LevelsManager();
            levelsManager.ChangeLevel(1);
            Game.Intit();
            this.Width = Game.cellSize * Game.mapWidth + 15;
            this.Height = Game.cellSize * Game.mapHeight + 40;
            var directorySprites = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent;
            var pathToPlayer = new Bitmap(Path.Combine(directorySprites.ToString(), "EntitySprites\\Player.png"));
            playerSheet = pathToPlayer;
            player = new Player(480, 480, 2, 6, 3, 3, playerSheet, 30, 60, "Player");
            updateTimer.Start();
            this.BackgroundImage = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.ToString(), "EntitySprites\\BackGround.png"));
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (player.Flip)
                player.SetAnimationConfiguration(3);
            else
                player.SetAnimationConfiguration(0);
            player.StopMove();
        }

        private void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.StartMove(0, -1);
                    if (player.Flip)
                        player.SetAnimationConfiguration(2);
                    else
                        player.SetAnimationConfiguration(1);
                    break;
                case Keys.S:
                    player.StartMove(0, 1);
                    if (player.Flip)
                        player.SetAnimationConfiguration(2);
                    else
                        player.SetAnimationConfiguration(1);
                    break;
                case Keys.A:
                    player.StartMove(-1, 0);
                    player.Flip = true;
                    player.SetAnimationConfiguration(2);
                    break;
                case Keys.D:
                    player.StartMove(1, 0);
                    player.Flip = false;
                    player.SetAnimationConfiguration(1);
                    break;
                case Keys.T:
                    player.TakeAnFurniture();
                    break;
                case Keys.R:
                    player.ReleaseObject();
                    break;
                case Keys.G:
                    foreach (var hint in Game.hintOnLevels)
                        hint.Activate();
                    break;
                case Keys.B:
                    foreach (var hint in Game.hintOnLevels)
                        hint.Block();
                    break;
                case Keys.D1:
                    player.ReleaseObject();
                    levelsManager.ChangeLevel(1);
                    break;
                case Keys.D2:
                    player.ReleaseObject();
                    levelsManager.ChangeLevel(2);
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
            if (!Physics.IsCollide(player, player.DirX, player.DirY))
                player.Act();
            Invalidate();
        }
    }
}
