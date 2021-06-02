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
        private int currentAnimation;
        private int currentFrame;
        private int currentLimit = 4;

        private Enemy enemy;
        private bool Flip;

        private Player player;
        private readonly Timer updateTimer;
        private readonly LevelsManager levelsManager;
        private GameMap map;

        private readonly Menu MainForm;

        public GetOutWinForm(Menu mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
            AutoSize = true;

            DoubleBuffered = true;

            updateTimer = new Timer();
            updateTimer.Interval = 80;
            updateTimer.Tick += new EventHandler(Update);

            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);
            FormClosed += CloseButton_Click;
            levelsManager = new LevelsManager();

            map = levelsManager.GetNextLevel();
            Init();
        }

        private void Init()
        {
            Flip = false;
            StartPosition = FormStartPosition.Manual;
            this.Width = GameMap.CellSize * GameMap.MapWidth;
            this.Height = GameMap.CellSize * GameMap.MapHeight+40;
            player = map.Player;
            this.BackgroundImage = Properties.Resources.BackGround;
            SetAnimationConfiguration(0);
            currentLimit = 4;
            enemy = map.enemy;

            updateTimer.Start();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (Flip)
                SetAnimationConfiguration(1);
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
                        SetAnimationConfiguration(3);
                    else
                        SetAnimationConfiguration(2);
                    break;
                case Keys.S:
                    player.StartMove(0, 1);
                    if (Flip)
                        SetAnimationConfiguration(3);
                    else
                        SetAnimationConfiguration(2);
                    break;
                case Keys.A:
                    player.StartMove(-1, 0);
                    Flip = true;
                    SetAnimationConfiguration(3);
                    break;
                case Keys.D:
                    player.StartMove(1, 0);
                    Flip = false;
                    SetAnimationConfiguration(2);
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

            var text = new Label();
            text.Text = "Вау, ты выйграл! Сыграем дальше?";
            text.Size = new Size(panel.Width, panel.Height / 3);
            text.Location = new Point(0, panel.Height / 3);
            text.AutoSize = false;

            var button = new Button();
            button.Text = "Next Level";
            button.Location = new Point(0, panel.Height * 2 / 3);
            button.Click += (s, e) =>
            {
                map = levelsManager.GetNextLevel();
                Init();
                Controls.Clear();
            };

            panel.Controls.Add(button);
            panel.Controls.Add(text);
            Controls.Add(panel);
        }

        private void Lose()
        {
            SetAnimationConfiguration(8);
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
                map = levelsManager.ChangeLevel(levelsManager.currentLevel.NumberLevel);
                Init();
                Controls.Clear();
            };

            panel.Controls.Add(button);
            panel.Controls.Add(text);
            Controls.Add(panel);
        }

        private void Update(object sender, EventArgs e)
        {
            if (map.Lose)
                Lose();
            if (map.CheckWin())
                Win();
            else
                enemy.MoveTo(new Point(player.PosX, player.PosY), map);
            player.Act(map);
            var cur = map.HintTrigger(player);
            if (cur != null)
                cur.SetActive(true);
            else
                foreach (var hint in map.HintOnLevels)
                    hint.SetActive(false);
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawMap(g);
        }

        private void DrawMap(Graphics g)
        {
            
            foreach (var entity in map.EntitiesOnMap)
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

            foreach (var hint in map.HintOnLevels)
            {
                if (hint.GetStatus())
                    g.DrawImage(hint.Sprite, new Point(hint.PosX, hint.PosY));
            }
            DrawPlayer(g);
        }

        private void DrawPlayer(Graphics g)
        {
            if (currentAnimation == 8)
                g.DrawImage(Properties.Resources.Plyer3_0,
                           new Rectangle(new Point(player.PosX, player.PosY),
                           new Size(player.Size.Height, player.Size.Width)),
                           player.Size.Width * currentFrame,
                           player.Size.Height * currentAnimation,
                           player.Size.Height,
                           player.Size.Width,
                           GraphicsUnit.Pixel);
            else
                g.DrawImage(Properties.Resources.Plyer3_0,
                                new Rectangle(new Point(player.PosX, player.PosY),
                                new Size(player.Size.Width, player.Size.Height)),
                                player.Size.Width * currentFrame,
                                player.Size.Height * currentAnimation,
                                player.Size.Width,
                                player.Size.Height,
                                GraphicsUnit.Pixel);
            if (currentFrame < currentLimit - 1) currentFrame += 1;
            else currentFrame = 0;
        }

        private void SetAnimationConfiguration(int currentAnimation)
        {
            if (currentAnimation == 0 && player.TakeStatus())
                this.currentAnimation = 6;
            else if (currentAnimation == 1 && player.TakeStatus())
                this.currentAnimation = 7;
            else if (currentAnimation == 2 && player.TakeStatus())
                this.currentAnimation = 4;
            else if (currentAnimation == 3 && player.TakeStatus())
                this.currentAnimation = 5;
            else
                this.currentAnimation = currentAnimation;

            switch (currentAnimation)
            {
                case 0:
                    currentLimit = 4;
                    break;
                case 1:
                    currentLimit = 4;
                    break;
                case 2:
                    currentLimit = 6;
                    break;
                case 3:
                    currentLimit = 6;
                    break;
                case 8:
                    currentLimit = 1;
                    break;
            }
        }
    }
}
