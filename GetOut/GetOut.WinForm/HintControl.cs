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
    public partial class HintControl : UserControl
    {
        private readonly GetOutWinForm getOutWinForm;
        private  Label hintText;

        public HintControl(GetOutWinForm getOutWinForm)
        {
            InitializeComponent();
            this.getOutWinForm = getOutWinForm;
            DoubleBuffered = true;
        }

        public void Init()
        {
            Width = getOutWinForm.Width / 2  +30;
            Height = getOutWinForm.Height / 2 - 60;
            Location = new Point(getOutWinForm.Width / 2 - Width / 2, getOutWinForm.Height / 2 - Height / 2);
            BackgroundImage = Properties.Resources.Cartel_Nota;
            BorderStyle = BorderStyle.FixedSingle;

            hintText = new Label()
            {
                Font = new Font(Font.FontFamily, 14f),
                Location = new Point(Width / 2 - Width / 2, Height / 2 - Height / 4),
                Width = Width ,
                Height = Height / 2,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(226, 226, 226)
            };

            var closeButton = new Button()
            {
                Width = 100,
                Height = 30,
                Text = "Убрать листок",
                Location = new Point(Width - 100, 0),
                BackColor = Color.FromArgb(190, 190, 190)
            };

            closeButton.Click += (o, e) =>
            {
                Controls.Clear();
                getOutWinForm.Controls.Clear();
            };

            Controls.Add(hintText);
            Controls.Add(closeButton);
        }

        public void ChangeHintText(string text)
        {
            hintText.Text = text;
        }

        
    }
}
