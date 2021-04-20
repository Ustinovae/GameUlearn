using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetOut
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Added();
        }



        public void Added()
        {
            var t = new PictureBox();
            t.Image = Resource1.Безымянный;
            t.Location = new Point(0, 0);
            Controls.Add(t);
        }
    }
}
