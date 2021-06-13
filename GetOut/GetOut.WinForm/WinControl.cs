using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetOut.WinForm
{
    public partial class WinControl : UserControl
    {
        private readonly GetOutWinForm getOutWinForm;

        public WinControl(GetOutWinForm getOutWinForm)
        {
            this.getOutWinForm = getOutWinForm;
        }

        public Button NextLevelButton { get; private set; }

        public void Init()
        {
            Width = getOutWinForm.Width / 2;
            Height = getOutWinForm.Height / 2;
            Location = new Point(getOutWinForm.Width / 2 - Width / 2, getOutWinForm.Height / 2 - Height/2);
            BackColor = Color.FromArgb(203, 227, 168);
            BorderStyle = BorderStyle.FixedSingle;

            var text = new Label
            {
                Text = "Вау, ты выйграл! Сыграем дальше?",
                Size = new Size(Width, Height / 3),
                Location = new Point(Width/3, Height / 3),
                AutoSize = false
            };

            NextLevelButton = new Button
            {
                Text = "Next Level",
                Location = new Point(Width/2 - 30, Height * 2 / 3)
            };

            Controls.Add(NextLevelButton);
            Controls.Add(text);
        }
    }
}
