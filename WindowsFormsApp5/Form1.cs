using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        float animTime;
        int hardTime;
        bool right, left, up, gameOver;
        int score;
        int playerSpeed = 12;
        int enemySpeed = 4;
        int bulletSpeed = 17;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            reset();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                right = true;
            }


        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            animTime++;
            if (animTime >= 200)
            {
                animTime = 0;
                if(Player.Image == Properties.Resources.Fly)
                {
                    Player.Image = Properties.Resources.Player;
                }
                else if(Player.Image == Properties.Resources.Player)
                {
                    Player.Image = Properties.Resources.Fly;
                }
            }
            
            hardTime++;
            if(hardTime >= 500)
            {
                hardTime = 0;
                enemySpeed++;
                if(enemySpeed >= 13)
                {
                    enemySpeed = 8;
                }
            }
            if (left == true && Player.Left > 12)
            {
                Player.Left -= playerSpeed;
            }
            if (right == true && Player.Left < 500)
            {
                Player.Left += playerSpeed;
            }

            Enemy1.Top += enemySpeed;
            Enemy2.Top += enemySpeed;
            Enemy3.Top += enemySpeed;

            if(Enemy1.Top > 710 || Enemy2.Top > 710 || Enemy3.Top > 710)
            {
                GameOver();
            }

            if (up)
            {
                bulletSpeed = 34;
                Bullet.Top -= bulletSpeed;
            }
            else
            {
                Bullet.Left = -300;
            }

            if(Bullet.Top <= -30)
            {
                up = false;
            }
            if (Bullet.Bounds.IntersectsWith(Enemy1.Bounds))
            {
                Enemy1.Top = -450;
                score += 10;
                Enemy1.Left = rnd.Next(12, 500);
                up = false;
                label1.Text = "Score: " + score.ToString();
                SoundPlayer sp = new SoundPlayer(Properties.Resources.Hit);
                sp.Play();
            }
            else if (Bullet.Bounds.IntersectsWith(Enemy2.Bounds))
            {
                Enemy2.Top = -450;
                score += 10;
                Enemy2.Left = rnd.Next(12, 500);
                up = false;
                label1.Text = "Score: " + score.ToString();
                SoundPlayer sp = new SoundPlayer(Properties.Resources.Hit);
                sp.Play();
            }
            else if (Bullet.Bounds.IntersectsWith(Enemy3.Bounds))
            {
                Enemy3.Top = -450;
                score += 10;
                Enemy3.Left = rnd.Next(12, 500);
                up = false;
                label1.Text = "Score: " + score.ToString();
                SoundPlayer sp = new SoundPlayer(Properties.Resources.Hit);
                sp.Play();
            }
            
        }

        private void OnKeyUP(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                right = false;
            }
            if(e.KeyCode == Keys.Space && up == false)
            {
                up = true;
                Bullet.Top = Player.Top + 10;
                Bullet.Left = Player.Left + (Player.Width / 2);
                SoundPlayer sp = new SoundPlayer(Properties.Resources.Shoot);
                sp.Play();
            }

            if(e.KeyCode == Keys.Enter && gameOver)
            {
                reset();
            }
        }



        private void reset()
        {
            hardTime = 0;
            enemySpeed = 4;
            timer1.Start();
            Enemy1.Left = rnd.Next(12, 458);
            Enemy2.Left = rnd.Next(12, 458);
            Enemy3.Left = rnd.Next(12, 458);

            Enemy1.Top = rnd.Next(60, 200) * -1;
            Enemy2.Top = rnd.Next(60, 300) * -1;
            Enemy3.Top = rnd.Next(60, 500) * -1;
            score = 0;
            bulletSpeed = 0;
            Bullet.Top = -300;
            label1.Text = "Score: " + score.ToString();
            up = false;
        }

        private void GameOver()
        {
            SoundPlayer sp = new SoundPlayer(Properties.Resources.GameOver);
            sp.Play();
            gameOver = true;
            timer1.Stop();
            label1.Text += Environment.NewLine + "Game Over!" + Environment.NewLine + "Enter to restart";

        }

    }
}
