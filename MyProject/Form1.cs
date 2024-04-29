using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyProject
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        bool goleft, goright, godown, goup, gameover = false;

        string direct = "up";

        int PlayerHealth = 100;
        int bullets = 10;
        int speed = 10;
        int zmspeed = 3;
        int score = 0;

        List<PictureBox> zombies = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
            Restart();
            foreach (Control control in Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.BackColor = Color.Transparent;
                    pictureBox.BackgroundImage = null;
                }
            }
        }

        private void txtHealth_Click(object sender, EventArgs e)
        {

        }

        private void MainEvent(object sender, EventArgs e)
        {
            if (PlayerHealth > 1)
            {
                HealthBar.Value = PlayerHealth;
            }
            else
            {
                gameover = true;
                Player.Image = Properties.Resources.dead;
                gameTimer.Stop();

            }
            txtBullet.Text = "Bullets: " + bullets;
            txtKill.Text = "Kills: " + score;
            if (goleft == true && Player.Left > 0)
            {
                Player.Left -= speed;
            }
            if (goright == true && Player.Left + Player.Width < this.ClientSize.Width)
            {
                Player.Left += speed;
            }

            if (goup == true && Player.Top > 50)
            {
                Player.Top -= speed;
            }
            if (godown == true && Player.Top + Player.Height < this.ClientSize.Height)
            {
                Player.Top += speed;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "newbull")
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        bullets += 5;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "zombie")
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        PlayerHealth -= 1;
                    }
                    if (x.Left > Player.Left)
                    {
                        x.Left -= zmspeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }
                    if (x.Left < Player.Left)
                    {
                        x.Left += zmspeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }
                    if (x.Top > Player.Top)
                    {
                        x.Top -= zmspeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (x.Top < Player.Top)
                    {
                        x.Top += zmspeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                }
                foreach (Control i in this.Controls)
                {
                    if (i is PictureBox && (string)i.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(i.Bounds))
                        {
                            score++;
                            this.Controls.Remove(x);
                            this.Controls.Remove(i);
                            ((PictureBox)x).Dispose();
                            ((PictureBox)i).Dispose();
                            zombies.Remove((PictureBox)x);
                            makezombie();
                        }
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (gameover == true)
            {
                return;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                direct = "left";
                Player.Image = Properties.Resources.left;

            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                direct = "right";
                Player.Image = Properties.Resources.right;

            }

            if (e.KeyCode == Keys.Up)
            {
                goup = true;
                direct = "up";
                Player.Image = Properties.Resources.up;

            }

            if (e.KeyCode == Keys.Down)
            {
                godown = true;
                direct = "down";
                Player.Image = Properties.Resources.down;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }

            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }

            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }

            if (e.KeyCode == Keys.Space && bullets > 0 && gameover == false)
            {
                bullets--;
                shoot(direct);
                if (bullets < 1)
                {
                    dropnewbullets();
                }
            }

            if (e.KeyCode == Keys.Enter && gameover == true)
            {
                Restart();
            }

        }

        private void shoot(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletx = Player.Left + (Player.Width / 2);
            shootBullet.bullety = Player.Top + (Player.Height / 2);
            shootBullet.makebullet(this);
        }

        private void makezombie()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = random.Next(0, 900);
            zombie.Top = random.Next(0, 800);
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombie.BackColor = Color.Transparent;
            zombie.BackgroundImage = null;
            zombies.Add(zombie);
            this.Controls.Add(zombie);
            Player.BringToFront();

        }

        private void dropnewbullets()
        {
            PictureBox newbull = new PictureBox();
            newbull.Image = Properties.Resources.ammo_Image;
            newbull.Left = random.Next(10, this.ClientSize.Width - newbull.Width);
            newbull.Top = random.Next(50, this.ClientSize.Height - newbull.Height);
            newbull.Tag = "newbull";
            this.Controls.Add(newbull);
            newbull.BringToFront();
            Player.BringToFront();
        }

        private void Restart()
        {
            Player.BackgroundImage = null;
            Player.Image = Properties.Resources.up;

            foreach (PictureBox x in zombies)
            {
                this.Controls.Remove(x);
            }

            zombies.Clear();
            for (int i = 0; i < 3; i++)
            {
                makezombie();
            }
            goup = false;
            goleft = false;
            godown = false;
            goright = false;
            gameover = false;
            PlayerHealth = 100;
            bullets = 10;
            score = 0;
            gameTimer.Start();
        }
    }
}