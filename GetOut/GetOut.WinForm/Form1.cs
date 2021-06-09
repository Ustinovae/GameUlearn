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
        private Player player;
        private readonly Timer updateTimer;
        private readonly LevelsManager levelsManager;
        private GameMap map;
        private List<Label> hintOnMaps;

        private bool pressE;

        private readonly Menu MainForm;

        public GetOutWinForm(Menu mainForm)
        {
            StartPosition = FormStartPosition.Manual;
            InitializeComponent();
            MainForm = mainForm;
            AutoSize = true;

            DoubleBuffered = true;

            updateTimer = new Timer();
            updateTimer.Interval = 100;
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
            hintOnMaps = new List<Label>();
            pressE = false;
            Width = GameMap.CellSize * GameMap.MapWidth ;
            Height = GameMap.CellSize * GameMap.MapHeight+40;
            player = map.Player;
            BackgroundImage = Properties.Resources.BackGround;
            player.SetAnimationConfiguration(0);
            foreach(var hint in map.HintOnLevels)
            {
                var l = new Label()
                {
                    Text = hint.Text,
                    Font = new Font(Font.FontFamily, 14f),
                    Location = new Point(Width / 2 - Width / 4, Height / 2 - Height / 6),
                    Width = Width / 2,
                    Height = Height / 3,
                    TextAlign = ContentAlignment.MiddleCenter 
                };
                Controls.Add(l);
                l.Hide();
                hintOnMaps.Add(l);
            }


            updateTimer.Start();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (player.Flip)
                player.SetAnimationConfiguration(1);
            else
                player.SetAnimationConfiguration(0);
            player.StopMove();
        }

        private void EnterPasword()
        {
            var panel = new Panel();
            panel.Location = new Point(Width / 2 - 5 * GameMap.CellSize / 2, Height / 2 - 3 * GameMap.CellSize / 2);
            panel.Width = 10 * GameMap.CellSize;
            panel.Height = 10 * GameMap.CellSize;
            var text = new TextBox
            {
                Text = "Введите пароль",
                ForeColor = Color.Gray,
                Size = new Size(panel.Width, panel.Height / 3),
                Location = new Point(0, panel.Height / 3),
                AutoSize = false
            };
            text.Enter += (s, e) =>
            {
                if (text.Text == "Введите пароль")
                {
                    text.Text = "";
                    text.ForeColor = Color.Black;
                }
            };

            text.Leave += (s, e) =>
            {
                if (text.Text == "")
                {
                    text.Text = "Введите пароль";
                    text.ForeColor = Color.Gray;
                }
            };

            var enterButton = new Button
            {
                Text = "Ввести",
                Location = new Point(panel.Width / 2, panel.Height / 4 * 3)
            };
            enterButton.Click += (s, e) =>
            {
                if (map.Exit.CheckPassword(text.Text))
                {
                    panel.Controls.Clear();
                    Controls.Clear();
                    Win();
                }
            };

            var closeButton = new Button
            {
                Text = "Закрыть",
                Location = new Point(0, panel.Height / 4 * 3)
            };
            closeButton.Click += (s, e) =>
            {
                panel.Controls.Clear();
                Controls.Clear();
            };

            panel.Controls.Add(closeButton);
            panel.Controls.Add(text);
            panel.Controls.Add(enterButton);
            Controls.Add(panel);
        }

        private void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.StartMove(0, -1);
                    player.SetAnimationConfiguration("Run");
                    break;
                case Keys.S:
                    player.StartMove(0, 1);
                    player.SetAnimationConfiguration("Run");
                    break;
                case Keys.A:
                    player.StartMove(-1, 0);
                    player.SetAnimationConfiguration("Run");
                    break;
                case Keys.D:
                    player.StartMove(1, 0);
                    player.SetAnimationConfiguration("Run");
                    break;
                case Keys.T:
                    player.TakeAnFurniture(map);
                    break;
                case Keys.R:
                    player.ReleaseObject();
                    break;
                case Keys.E:
                    if (map.CheckWin())
                    {
                        foreach (var hint in map.HintOnLevels)
                            hint.SetActive(false);
                        EnterPasword();
                    }
                    else
                    {
                        var cur = map.HintTrigger(player);
                        if (cur != null)
                            cur.SetActive(true);
                    }
                    break;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            MainForm.Show();
        }

        private void Win()
        {
            pressE = false;
            var panel = new Panel();

            var text = new Label
            {
                Text = "Вау, ты выйграл! Сыграем дальше?",
                Size = new Size(panel.Width, panel.Height / 3),
                Location = new Point(0, panel.Height / 3),
                AutoSize = false
            };

            var button = new Button
            {
                Text = "Next Level",
                Location = new Point(0, panel.Height * 2 / 3)
            };
            button.Click += (s, e) =>
            {
                panel.Controls.Clear();
                Controls.Clear();
                map = levelsManager.GetNextLevel();
                Init();
            };

            panel.Controls.Add(button);
            panel.Controls.Add(text);
            Controls.Add(panel);
        }

        private void Lose()
        {
            pressE = false;
            KeyPreview = true;
            player.SetAnimationConfiguration(8);
            var panel = new Panel();
            var text = new Label
            {
                Text = "К сожалению ты проиграл. Может попробуешь еще раз?",
                Size = new Size(panel.Width, panel.Height / 3),
                Location = new Point(0, panel.Height / 3),
                AutoSize = false
            };

            var button = new Button
            {
                Text = "Restart",
                Location = new Point(0, panel.Height * 2 / 3)
            };
            button.Click += (s, e) =>
            {
                map = levelsManager.ChangeLevel(levelsManager.currentLevel.NumberLevel);
                panel.Controls.Clear();
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
            {
                Lose();
            }               
            else
                foreach(var enemy in map.EnemisOnMap)
                    enemy.MoveTo(new Point(player.PosX, player.PosY), map);
            player.Act(map);
            var cur = map.HintTrigger(player);
            if (cur != null || map.CheckWin())
            {
                pressE = true;
            }
            else
            {
                foreach (var hint in map.HintOnLevels)
                    hint.SetActive(false);
                pressE = false;
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
                }
            }

            g.DrawImage(Properties.Resources.exit, new Point(map.Exit.PosX, map.Exit.PosY));
            DrawEnemies(g);
            DrawHint(g);
            DrawPlayer(g);
            if (pressE)
                DrawOperation(g);
        }

        private void DrawOperation(Graphics g)
        {
            g.DrawImage(Properties.Resources.read, new Point(player.PosX, player.PosY));
        }

        private void DrawHint(Graphics g)
        {
            for(var i = 0; i < map.HintOnLevels.Count; i++)
            {
                g.DrawImage(Properties.Resources.note, map.HintOnLevels[i].PosX, map.HintOnLevels[i].PosY);
                if (map.HintOnLevels[i].GetStatus())
                {
                    hintOnMaps[i].Show();
                }
                else
                {
                    hintOnMaps[i].Hide();
                }
            }
        }

        private void DrawPlayer(Graphics g)
        {
            if (player.CurrentAnimation == 8)
                g.DrawImage(Properties.Resources.Plyer3_0,
                           new Rectangle(new Point(player.PosX, player.PosY),
                           new Size(player.Size.Height, player.Size.Width)),
                           player.Size.Width * player.CurrentFrame,
                           player.Size.Height * player.CurrentAnimation,
                           player.Size.Height,
                           player.Size.Width,
                           GraphicsUnit.Pixel);
            else
                g.DrawImage(Properties.Resources.Plyer3_0,
                                new Rectangle(new Point(player.PosX, player.PosY),
                                new Size(player.Size.Width, player.Size.Height)),
                                player.Size.Width * player.CurrentFrame,
                                player.Size.Height * player.CurrentAnimation,
                                player.Size.Width,
                                player.Size.Height,
                                GraphicsUnit.Pixel);
            if (player.CurrentFrame < player.CurrentLimit - 1) player.CurrentFrame += 1;
            else player.CurrentFrame = 0;
        }

        private void DrawEnemies(Graphics g)
        {
            foreach (var enemy in map.EnemisOnMap)
            {
                g.DrawImage(Properties.Resources.EnemyAnimation,
                                    new Rectangle(new Point(enemy.PosX, enemy.PosY),
                                    new Size(enemy.Size.Width, enemy.Size.Height)),
                                    enemy.Size.Width * enemy.CurrentFrame,
                                    enemy.Size.Height * enemy.CurrentAnimation,
                                    enemy.Size.Width,
                                    enemy.Size.Height,
                                    GraphicsUnit.Pixel);
                if (enemy.CurrentFrame < enemy.CurrentLimit - 1) enemy.CurrentFrame += 1;
                else enemy.CurrentFrame = 0;
            }
        }
    }
}
