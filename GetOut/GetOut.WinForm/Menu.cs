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
    public partial class Menu : Form
    {
        private readonly Button startButton;
        private readonly Panel mainPanel;

        public Menu()
        {
            StartPosition = FormStartPosition.Manual;
            mainPanel = new Panel();
            mainPanel.ClientSize = new Size(30 * 20, 30 * 20);
            InitializeComponent();
            Width = 815;
            Height = 645;
            BackColor = Color.FromArgb(221, 183, 48);

            startButton = new Button
            {
                Text = "Let's Go",
                Size = new Size(300, 200),
                AutoSize = false,
                Location = new Point(250, 150)
            };
            startButton.Font = new Font(startButton.Name, 32f, startButton.Font.Style);
            startButton.Click += StartButton_Click;

            mainPanel.Controls.Add(startButton);

            Controls.Add(mainPanel);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var gameControl = new GetOutWinForm(this);
            Hide();
            gameControl.Show();
        }
    }
}
