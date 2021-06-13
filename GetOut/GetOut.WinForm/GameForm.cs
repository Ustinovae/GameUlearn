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
        private readonly WinControl winControl;
        private readonly LoseControl loseControl;
        private readonly EnterPasswordControl enterPasswordControl;
        private readonly HintControl hintControl;

        private readonly Timer updateTimer;

        private Player player;
        private readonly LevelsManager levelsManager;
        private GameMap map;

        private bool lockKeybord;
        private bool pressE;

        private readonly Menu MainForm;

        public GetOutWinForm(Menu mainForm)
        {
            winControl = new WinControl(this);
            loseControl = new LoseControl(this);
            enterPasswordControl = new EnterPasswordControl(this);
            hintControl = new HintControl(this);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(228, 220, 168);
            InitializeComponent();
            MainForm = mainForm;
            AutoSize = true;

            DoubleBuffered = true;

            updateTimer = new Timer();
            updateTimer.Interval = 100;
            updateTimer.Tick += new EventHandler(Update);

            if (!lockKeybord)
            {
                KeyDown += new KeyEventHandler(OnPress);
                KeyUp += new KeyEventHandler(OnKeyUp);
            }
            FormClosed += CloseButton_Click;
            levelsManager = new LevelsManager();

            map = levelsManager.GetNextLevel();
            Init();
        }

        private void Init()
        {
            lockKeybord = false;
            pressE = false;
            Width = GameMap.CellSize * map.MapWidth;
            Height = GameMap.CellSize * map.MapHeight + 40;
            player = map.Player;
            player.SetAnimationConfiguration("State");
            updateTimer.Start();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (lockKeybord) return;
            player.SetAnimationConfiguration("State");
            player.StopMove();
        }

        private void OnPress(object sender, KeyEventArgs e)
        {
            if (lockKeybord) return;

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
                case Keys.F:
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
                        ShowEnterPasword();
                    }
                    else
                    {
                        var cur = map.HintTrigger(player);
                        if (cur != null)
                        {
                            ShowHint(cur.Text);
                        }
                    }
                    break;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e) =>
            MainForm.Show();

        private void Update(object sender, EventArgs e)
        {
            if (Controls.Count == 0) lockKeybord = false;
            if (map.Lose) ShowLoseControl();

            foreach (var enemy in map.EnemisOnMap)
                enemy.MoveTo(new Point(player.PosX, player.PosY), map);
            player.Act(map);

            var cur = map.HintTrigger(player);
            if (cur != null || map.CheckWin())
                pressE = true;
            else
            {
                foreach (var hint in map.HintOnLevels)
                    hint.SetActive(false);
                pressE = false;
            }
            Invalidate();
        }

        private void ShowHint(string text)
        {
            hintControl.Init();
            hintControl.ChangeHintText(text);
            Controls.Add(hintControl);
            lockKeybord = true;
        }

        private void ShowEnterPasword()
        {
            lockKeybord = true;
            enterPasswordControl.Init();

            enterPasswordControl.EnterButton.Click += (s, e) =>
            {
                if (map.Exit.CheckPassword(enterPasswordControl.InputText.Text))
                {
                    enterPasswordControl.Controls.Clear();
                    Controls.Clear();
                    ShowWinControl();
                }
            };

            Controls.Add(enterPasswordControl);
        }

        private void ShowLoseControl()
        {
            lockKeybord = true;
            pressE = false;
            Controls.Clear();

            player.SetAnimationConfiguration("Deth");

            loseControl.Init();
            loseControl.RestartButton.Click += (s, e) =>
            {
                loseControl.Controls.Clear();
                Controls.Clear();
                map = levelsManager.Restart();
                Init();
            };

            Controls.Add(loseControl);
        }

        private void ShowWinControl()
        {
            map.Win = true;
            pressE = false;
            Controls.Clear();

            winControl.Init();
            winControl.NextLevelButton.Click += (s, e) =>
            {
                lockKeybord = false;
                winControl.Controls.Clear();
                Controls.Clear();
                map = levelsManager.GetNextLevel();
                Init();
            };

            Controls.Add(winControl);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawMap(g);
        }

        private void DrawMap(Graphics g)
        {
            foreach (var entity in map.EntitiesOnMap)
                switch (entity.Name)
                {
                    case "BarrierHorizontal":
                        g.DrawImage(Properties.Resources.wallH, new Rectangle(new Point(entity.PosX, entity.PosY), entity.Size));
                        break;
                    case "BarrierVertical":
                        g.DrawImage(Properties.Resources.wallV, new Rectangle(new Point(entity.PosX, entity.PosY), entity.Size));
                        break;
                    case "Furniture":
                        g.DrawImage(Properties.Resources.box, new Point(entity.PosX, entity.PosY));
                        break;
                }

            g.DrawImage(Properties.Resources.exit, new Point(map.Exit.PosX, map.Exit.PosY));
            DrawHint(g);
            DrawPlayer(g);
            DrawEnemies(g);
            if (pressE) DrawOperation(g);
        }

        private void DrawOperation(Graphics g)
        {
            if (map.CheckWin()) g.DrawImage(Properties.Resources.passwordact, new Point(player.PosX, player.PosY - 20));
            else g.DrawImage(Properties.Resources.read, new Point(player.PosX, player.PosY - 30));
        }

        private void DrawHint(Graphics g)
        {
            for (var i = 0; i < map.HintOnLevels.Count; i++)
                g.DrawImage(Properties.Resources.note, map.HintOnLevels[i].PosX, map.HintOnLevels[i].PosY);
            
        }

        private void DrawPlayer(Graphics g)
        {
            g.DrawImage(Properties.Resources.player3_1,
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
