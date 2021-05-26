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
        public Panel panel;
        public Label CheckTake;
        public Label hint;
        public Label desOfman;
        public TextBox input;


        public GetOutWinForm()
        {
            panel = new Panel();
            panel.Dock = DockStyle.Right;
            Controls.Add(panel);

            input = new TextBox();


            hint = new Label();
            hint.Width = panel.Width;
            
            CheckTake = new Label();
            CheckTake.Text = "Holds object: False";
            CheckTake.Width = panel.Width;
            CheckTake.Location = new Point(0, 25);

            desOfman = new Label();
            desOfman.Location = new Point(0, 50);
            desOfman.Size = new Size(panel.Width, 100);
            desOfman.Text = "T - взять \nR - отпустить \nG - показать подсказку \nB - вернуться в игру \nWSAD - обычное управление"; 

            panel.Controls.Add(hint);
            panel.Controls.Add(CheckTake);
            panel.Controls.Add(desOfman);
             
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
            this.Width = Game.cellSize * Game.mapWidth + 215;
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
                    if (player.capturedFurniture != null)
                        CheckTake.Text = "Holds object: True";
                    break;
                case Keys.R:
                    player.ReleaseObject();
                    CheckTake.Text = "Holds object: False";
                    break;
                case Keys.G:
                    var cur = Physics.HintsTrigger(player);
                    if (cur != null)
                    {
                        player.Block();
                        cur.Activate();
                    }
                    break;
                case Keys.B:
                    player.Unblock();
                    foreach (var hint in Game.hintOnLevels)
                        hint.Block();
                    break;
                case Keys.D1:
                    player.ReleaseObject();
                    levelsManager.ChangeLevel(1);
                    CheckTake.Text = "Holds object: False";
                    break;
                case Keys.D2:
                    player.ReleaseObject();
                    levelsManager.ChangeLevel(2);
                    CheckTake.Text = "Holds object: False";
                    break;
                case Keys.D3:
                    player.ReleaseObject();
                    levelsManager.ChangeLevel(3);
                    CheckTake.Text = "Holds object: False";
                    break;
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            player.PlayAnimation(g);
            Game.DrawMap(g);
        }

        private void Update(object sender, EventArgs e)
        {
            if (!Physics.IsCollide(player, player.DirX, player.DirY))
                player.Act();
            if (Physics.HintsTrigger(player) != null)
                hint.Text = "Hint near you";
            else
                hint.Text = "There's no hint near you";
            Invalidate();
        }
    }
}
