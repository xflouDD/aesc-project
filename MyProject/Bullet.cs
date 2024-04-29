using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using System.Windows.Forms;

namespace MyProject
{
    internal class Bullet
    {
        public int bullety = 0;
        public int bulletx = 0;
        public string direction;

        private int speed = 20;
        private PictureBox bullet = new PictureBox();
        private Timer bullettimer = new Timer();

        public void makebullet(Form form)
        {
            bullet.BackColor = Color.White;
            bullet.Size = new Size(5, 5);
            bullet.Tag = "bullet";
            bullet.Left = bulletx;
            bullet.Top = bullety;
            bullet.BringToFront();
            form.Controls.Add(bullet);

            bullettimer.Interval = speed;
            bullettimer.Tick += new EventHandler(bullettimerEvent);
            bullettimer.Start();
        }

        private void bullettimerEvent(object sender, EventArgs e)
        {
            if (direction == "left")
            {
                bullet.Left -= speed;
            }

            if (direction == "right")
            {
                bullet.Left += speed;
            }

            if (direction == "up")
            {
                bullet.Top -= speed;
            }

            if (direction == "down")
            {
                bullet.Top += speed;
            }

            if (bullet.Left < 10 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 600)
            {
                bullettimer.Stop();
                bullettimer.Dispose();
                bullet.Dispose();
                bullettimer = null;
                bullet = null;

            }

        }
    }
}
