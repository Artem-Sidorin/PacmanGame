using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pc_Man_Game_MOO_ICT
{
    public class Object
    {
        public Object()
        {

        }

        public PictureBox CreateCoin(int rnd, int rnd2)
        {
            PictureBox picture = new PictureBox()
            {
                Image = Properties.Resources.coin,
                Size = new Size(30, 30),
                Location = new Point(rnd, rnd2),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "coin",
            };
            return picture;
        }


        public PictureBox CreateWall(int rnd, int rnd2)
        {
            PictureBox picture = new PictureBox()
            {
                BackColor = Color.Blue,
                Size = new Size(70, 70),
                Location = new Point(rnd, rnd2),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = "wall",
            };
            return picture;
        }
    }
}
