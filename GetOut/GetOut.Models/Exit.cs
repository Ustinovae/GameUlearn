using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetOut.Models
{
    public class Exit: Entity
    {
        private readonly string password;

        public Exit(int posX, int posY, int width, int height, string name, string password): base(posX, posY, new Size(width, height), name)
        {
            this.password = password;
        }

        public bool CheckPassword(string password)
        {
            return this.password == password;
        }
    }
}
