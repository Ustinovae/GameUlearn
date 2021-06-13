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
    public partial class EnterPasswordControl : UserControl
    {
        private readonly GetOutWinForm getOutWinForm;

        public EnterPasswordControl(GetOutWinForm getOutWinForm)
        {
            InitializeComponent();
            this.getOutWinForm = getOutWinForm;
            //KeyDown += OnPress;
        }

        //private void OnPress(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case(Keys.Escape)
        //            Controls.Clear();
        //            getOutWinForm.Controls.Clear();
        //            break;
        //    }
        //}

        public Button EnterButton { get; private set; }
        public Button CloseButton { get; private set; }
        public TextBox InputText { get; private set; }

        public void Init()
        {
            
            Width = getOutWinForm.Width / 2;
            Height = getOutWinForm.Height / 2;
            Location = new Point(getOutWinForm.Width / 2 - Width / 2, getOutWinForm.Height / 2 - Height / 2);
            BackColor = Color.FromArgb(182, 172, 193);
            BorderStyle = BorderStyle.FixedSingle;

            var p = new Panel()
            {
                Size = new Size(Width, Height / 3),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(168, 228, 172)
        };

            InputText = new TextBox
            {
                Text = "Введите пароль",
                ForeColor = Color.Gray,
                Size = new Size(Width/2, Height / 3),
                Location = new Point(Width/2 - Width/4, Height / 3)
            };
            InputText.Enter += (s, e) =>
            {
                if (InputText.Text == "Введите пароль")
                {
                    InputText.Text = "";
                    InputText.ForeColor = Color.Black;
                }
            };

            InputText.Leave += (s, e) =>
            {
                if (InputText.Text == "")
                {
                    InputText.Text = "Введите пароль";
                    InputText.ForeColor = Color.Gray;
                }
            };

            EnterButton = new Button
            {
                Text = "Ввести",
                Location = new Point( Width * 3 / 4, Height / 3)
            };

            CloseButton = new Button
            {
                Text = "Закрыть",
                Location = new Point(Width - 80, Height / 3)
                
            };

            CloseButton.Click += (o, e) =>
            {
                Controls.Clear();
                getOutWinForm.Controls.Clear();
            };
            Controls.Add(p);
            Controls.Add(CloseButton);
            Controls.Add(InputText);
            Controls.Add(EnterButton);
        }
    }
}
