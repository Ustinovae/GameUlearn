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
    public partial class LoseControl : UserControl
    {
        private readonly GetOutWinForm getOutWinForm;

        public LoseControl(GetOutWinForm getOutWinForm)
        {
            this.getOutWinForm = getOutWinForm;
        }

        public Button RestartButton { get; private set; }

        public void Init()
        {
            Width = getOutWinForm.Width / 2;
            Height = getOutWinForm.Height / 2;
            Location = new Point(getOutWinForm.Width / 2 - Width / 2, getOutWinForm.Height / 2 - Height / 2);
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.FromArgb(238, 90, 96);

            var text = new Label
            {
                Text = "К сожалению ты проиграл. Может попробуешь еще раз?",
                Size = new Size(Width, Height / 3),
                Location = new Point(Width / 4, Height / 3),
                AutoSize = false
            };

            RestartButton = new Button
            {
                Text = "Restart",
                Location = new Point(Width / 2 - 30, Height * 2 / 3)
            };

            Controls.Add(RestartButton);
            Controls.Add(text);
        }
    }
}
