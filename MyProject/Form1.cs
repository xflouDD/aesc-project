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
        const int minDistX = 150, minDistY = 150;
        const int minPlayerSpeed = 5, maxPlayerSpeed = 10;
        const int maxHp = 100;

        const int startZombies = 3;
        const int killsPerZombie = 15;

        int bullets = 10;
        int speed = 10;
        int zmspeed = 3;

        int score = 0;

        bool isKitDropped = false;

        List<PictureBox> zombies = new List<PictureBox>();
        public Form1()
        {
            
            InitializeComponent();
            Restart();
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

            if(PlayerHealth < 50 && gameover == false && isKitDropped == false)
            {
                dropNewKit();
                isKitDropped = true;
            }
            speed = getPlayerSpeed();
            txtBullet.Text = "Bullets: " + bullets;
            txtKill.Text = "Kills: " + score;
            Keys adown = Keys.Down;
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

            List<Control> toRemove = new List<Control>();
            
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "newbull")
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        toRemove.Add(x);
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        bullets += random.Next(3, 8);
                    }
                }

                if (x is PictureBox && (string)x.Tag == "kit")
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds))
                    {
                        toRemove.Add(x);
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        PlayerHealth += random.Next(20, maxHp - PlayerHealth);
                        isKitDropped = false;
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
                            makeZombies();
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
                Player.BackColor = Color.Transparent;

            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                direct = "right";
                Player.Image = Properties.Resources.right;
                Player.BackColor = Color.Transparent;
            }

            if (e.KeyCode == Keys.Up)
            {
                goup = true;
                direct = "up";
                Player.Image = Properties.Resources.up;
                Player.BackColor = Color.Transparent;
            }

            if (e.KeyCode == Keys.Down)
            {
                godown = true;
                direct = "down";
                Player.Image = Properties.Resources.down;
                Player.BackColor = Color.Transparent;
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

            if(e.KeyCode == Keys.Escape && gameover == true)
            {
                this.Close();
            }

        }

        private int getPlayerSpeed()
        {
            return minPlayerSpeed + PlayerHealth * (maxPlayerSpeed - minPlayerSpeed) / maxHp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private int getCountZombies()
        {
            return startZombies + (score / killsPerZombie);
        }

        private void shoot(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletx = Player.Left + (Player.Width / 2);
            shootBullet.bullety = Player.Top + (Player.Height / 2);
            shootBullet.makebullet(this);
        }

        private void makeZombies() {
            while ((int)zombies.Count < getCountZombies()) {
                makezombie();
            }
        }

        private void makezombie()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
          

            while (true) {
                bool good = true;
                
                int tempX = random.Next(0, 900);
                int tempY = random.Next(0, 800);

                
                if (Math.Abs(tempX - Player.Left) < minDistX) {
                    good = false;
                }

                if (Math.Abs(tempY - Player.Top) < minDistY) {
                    good = false;
                }

                if (good) {
                    zombie.Left = tempX;
                    zombie.Top = tempY;
                    break;
                }
            }

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

            while (true) {
                bool good = true;

                int tempX = random.Next(10, this.ClientSize.Width - newbull.Width);
                int tempY = random.Next(50, this.ClientSize.Height - newbull.Height);


                if (Math.Abs(tempX - Player.Left) < minDistX) {
                    good = false;
                }

                if (Math.Abs(tempY - Player.Top) < minDistY) {
                    good = false;
                }

                if (good) {
                    newbull.Left = tempX;
                    newbull.Top = tempY;
                    break;
                }
            }

            newbull.Tag = "newbull";
            this.Controls.Add(newbull);
            newbull.BringToFront();
            Player.BringToFront();
        }

        private void dropNewKit()
        {
            PictureBox kit = new PictureBox();
            kit.Image = Properties.Resources.kitmn1;
            while (true)
            {
                bool good = true;

                int tempX = random.Next(10, this.ClientSize.Width - kit.Width);
                int tempY = random.Next(50, this.ClientSize.Height - kit.Height);


                if (Math.Abs(tempX - Player.Left) < minDistX)
                {
                    good = false;
                }

                if (Math.Abs(tempY - Player.Top) < minDistY)
                {
                    good = false;
                }

                if (good)
                {
                    kit.Left = tempX;
                    kit.Top = tempY;
                    break;
                }
            }

            kit.Tag = "kit";
            this.Controls.Add(kit);
            kit.BringToFront();
            Player.BringToFront();
        }

        private void Restart()
        {
            Player.BackgroundImage = null;
            Player.Image = Properties.Resources.up;
            direct = "up";
            Player.BackColor = Color.Transparent;

            zombies.Clear();

            isKitDropped = false;

            List<Control> toRemove = new List<Control>();
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "kit")
                {
                    toRemove.Add(x);
                }
                if (x is PictureBox && (string)x.Tag == "newbull")
                {
                    toRemove.Add(x);
                }
                if(x is PictureBox && (string) x.Tag == "zombie")
                {
                    toRemove.Add(x);
                }
            }

            foreach (Control x in toRemove)
            {
                this.Controls.Remove(x);
                ((PictureBox)x).Dispose();
            }

            zombies.Clear();
            goup = false;
            goleft = false;
            godown = false;
            goright = false;
            gameover = false;
            PlayerHealth = 100;
            bullets = 10;
            score = 0;
            makeZombies();
            gameTimer.Start();
        }
    }
}