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

namespace GetOut.WinForm
{
    public partial class GetOutWinForm : Form
    {
        private readonly int idleFrames = 2;
        private readonly int runFrames = 6;
        //private readonly int deathFrames = 3;
        private int currentAnimation;
        private int currentFrame;
        private int currentLimit = 2;

        private Enemy enemy;

        private Image Sprite = Properties.Resources.Player;
        public bool Flip;

        public Image playerSheet;
        public Player player;
        public Timer updateTimer = new();
        public LevelsManager levelsManager;
        public Map map;

        private readonly Menu MainForm;

        public GetOutWinForm(Menu mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
            StartPosition = FormStartPosition.CenterScreen;

            DoubleBuffered = true;
            updateTimer.Interval = 80;
            updateTimer.Tick += new EventHandler(Update);

            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);
            FormClosed += CloseButton_Click;
            Init();
        }

        private void Init()
        {
            levelsManager = new LevelsManager();
            map = levelsManager.ChangeLevel(1);
            this.Width = Map.CellSize * Map.MapWidth;
            this.Height = Map.CellSize * Map.MapHeight + 40;
            player = Map.Player;
            this.BackgroundImage = Properties.Resources.BackGround;
            enemy = map.enemy;

            updateTimer.Start();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (Flip)
                SetAnimationConfiguration(3);
            else
                SetAnimationConfiguration(0);
            player.StopMove();
        }

        private void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.StartMove(0, -1);
                    if (Flip)
                        SetAnimationConfiguration(2);
                    else
                        SetAnimationConfiguration(1);
                    break;
                case Keys.S:
                    player.StartMove(0, 1);
                    if (Flip)
                        SetAnimationConfiguration(2);
                    else
                        SetAnimationConfiguration(1);
                    break;
                case Keys.A:
                    player.StartMove(-1, 0);
                    Flip = true;
                    SetAnimationConfiguration(2);
                    break;
                case Keys.D:
                    player.StartMove(1, 0);
                    Flip = false;
                    SetAnimationConfiguration(1);
                    break;
                case Keys.T:
                    player.TakeAnFurniture(map);
                    break;
                case Keys.R:
                    player.ReleaseObject();
                    break;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            MainForm.Show();
        }

        private void Win()
        {
            var panel = new Panel();
            // panel.Dock = DockStyle.Fill;

            var text = new Label();
            text.Text = "К сожалению ты проиграл. Может попробуешь еще раз?";
            text.Size = new Size(panel.Width, panel.Height / 3);
            text.Location = new Point(0, panel.Height / 3);
            text.AutoSize = false;

            var button = new Button();
            button.Text = "Restart";
            button.Location = new Point(0, panel.Height * 2 / 3);
            button.Click += (s, e) =>
            {
                levelsManager.GetNextLevel();
                Controls.Clear();
                Init();
            };

            panel.Controls.Add(button);
            panel.Controls.Add(text);
            Controls.Add(panel);
        }

        private void Losing()
        {
            var panel = new Panel();
            var text = new Label();
            text.Text = "К сожалению ты проиграл. Может попробуешь еще раз?";
            text.Size = new Size(panel.Width, panel.Height / 3);
            text.Location = new Point(0, panel.Height / 3);
            text.AutoSize = false;

            var button = new Button();
            button.Text = "Restart";
            button.Location = new Point(0, panel.Height * 2 / 3);
            button.Click += (s, e) =>
            {
                levelsManager.Restart();
                Controls.Clear();
                Init();
            };

            panel.Controls.Add(button);
            panel.Controls.Add(text);
            Controls.Add(panel);
        }

        private void Update(object sender, EventArgs e)
        {
            if (map.Lose)
                Losing();
            enemy.MoveTo(new Point(player.PosX, player.PosY), map);
            if (!Map.IsCollide(player, new Point(player.PosX + player.DirX, player.DirY + player.PosY)))
                player.Act(map);
            var cur = map.HintTrigger(player);
            if (cur != null)
            {
                cur.SetActive(true);
            }
            else
            {
                foreach (var hint in Map.HintOnLevels)
                    hint.SetActive(false);
            }
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawMap(g);
        }

        private void DrawMap(Graphics g)
        {
            foreach (var entity in Map.EntitiesOnMap)
            {
                switch (entity.Name)
                {
                    case "Barrier":
                        g.DrawImage(Properties.Resources.barier, new Point(entity.PosX, entity.PosY));
                        break;
                    case "Furniture":
                        g.DrawImage(Properties.Resources.chest, new Point(entity.PosX, entity.PosY));
                        break;
                    case "Enemy":
                        g.DrawImage(Properties.Resources.Enemy, new Point(entity.PosX, entity.PosY));
                        break;
                }
            }
            g.DrawImage(Sprite,
                            new Rectangle(new Point(player.PosX, player.PosY),
                            new Size(player.Size.Width, player.Size.Height)),
                            player.Size.Width * currentFrame,
                            player.Size.Height * currentAnimation,
                            player.Size.Width,
                            player.Size.Height,
                            GraphicsUnit.Pixel);
            if (currentFrame < currentLimit - 1) currentFrame += 1;
            else currentFrame = 0;

            foreach (var hint in Map.HintOnLevels)
            {
                if (hint.GetStatus())
                    g.DrawImage(hint.Sprite, new Point(hint.PosX, hint.PosY));
            }
        }

        private void SetAnimationConfiguration(int currentAnimation)
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
