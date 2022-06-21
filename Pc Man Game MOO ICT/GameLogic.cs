using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pc_Man_Game_MOO_ICT
{
    public partial class GameLogic : Form
    {
        bool goup, godown, goleft, goright, isGameOver;

        int score, playerSpeed, pinkGhostX, pinkGhostY, yellowGhostX, yellowGhostY, redGhostSpeed, yellowGhostSpeed, pinkGhostSpeed;

        Random random = new Random(Guid.NewGuid().GetHashCode());
        int rnd = 3;

        Image pacmanStop = Properties.Resources.pacman0;
        Image pacmanLeft = Properties.Resources.pacmanLeft;
        Image pacmanRight = Properties.Resources.pacman1;
        Image pacmanUp = Properties.Resources.pacmanUp;
        Image pacmanDown = Properties.Resources.pacmanDown;

        public GameLogic()
        {
            this.WindowState = FormWindowState.Maximized;

            InitializeComponent();
            Closing += ExecuteOnClosing;
            resetGame();
            this.Name = "Pacman";
        }

        private void ExecuteOnClosing(object sender, CancelEventArgs e)
        {
            Application.Exit();
        }

        private void menu()
        {
            Menu menu = new Menu();
            menu.Show();
            Hide();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: goup = true; break;
                case Keys.Down: godown = true; break;
                case Keys.Left: goleft = true; break;
                case Keys.Right: goright = true; break;
                case Keys.Escape: menu(); break;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: goup = false; break;
                case Keys.Down: godown = false; break;
                case Keys.Left: goleft = false; break;
                case Keys.Right: goright = false; break;
            }
            if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                resetGame();
            }
        }

        // Выход за пределы экрана
        public void OutOfBounds(PictureBox picture)
        {
            if (picture.Left < -10)
            {
                picture.Left = this.Width;
            }
            if (picture.Left > this.Width)
            {
                picture.Left = -10;
            }

            if (picture.Top < -10)
            {
                picture.Top = 750;
            }
            if (picture.Top > 750)
            {
                picture.Top = -10;
            }
        }

        // Анимация
        async private void animation(Image image, Image image2)
        {
            if (godown && goup || godown && goright || godown && goleft || goup && goright || goup && goleft)
                pacman.Image = image;
            else
            {
                pacman.Image = image;
                await Task.Delay(26);
                pacman.Image = image2;
                await Task.Delay(3);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Width = this.Width;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.AutoSize = false;

        }

        private Boolean mooveRedDop()
        {
            if (redGhost.Top < -10 || redGhost.Top > 740)
            {
                return true;
            }

            if (redGhost.Left < -10 || redGhost.Left > this.Width - 50)
            {
                return true;
            }
            return false;
        } 

        // Движение красного призрака
        private void mooveRed()
        {
            if (!isGameOver)
            {
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox)
                    {
                        if ((string)x.Tag == "wall" || (string)x.Tag == "ghost" || (string)x.Tag == "ghostSpecial")
                        {
                            if (redGhost.Bounds.IntersectsWith(x.Bounds) || mooveRedDop())
                            {
                                rnd = random.Next(1, 4);
                                switch (rnd)
                                {
                                    case 1: redGhost.Top = redGhost.Top + 50; break;
                                    case 2: redGhost.Top = redGhost.Top - 50; break;
                                    case 3: redGhost.Left = redGhost.Left + 50; break;
                                    case 4: redGhost.Left = redGhost.Left - 50; break;
                                }
                            }
                        }
                    }
                }
                if (rnd == 4)
                {
                    redGhost.Top += redGhostSpeed;
                }
                if (rnd == 3)
                {
                    redGhost.Top -= redGhostSpeed;
                }
                if (rnd == 2)
                {
                    redGhost.Left += redGhostSpeed;
                }
                if (rnd == 1)
                {
                    redGhost.Left -= redGhostSpeed;
                }
            }
            OutOfBounds(redGhost);
        }

        // Движение желтого призрака
        private void mooveYellow()
        {
            if (!isGameOver)
            {
                if (pacman.Top > yellowGhost.Top)
                {
                    yellowGhost.Top += yellowGhostY;
                }
                if (pacman.Top < yellowGhost.Top)
                {
                    yellowGhost.Top -= yellowGhostY;
                }
                if (pacman.Left > yellowGhost.Left)
                {
                    yellowGhost.Left += yellowGhostX;
                }
                if (pacman.Left < yellowGhost.Left)
                {
                    yellowGhost.Left -= yellowGhostX;
                }
            }
            OutOfBounds(yellowGhost);
        }

        // Движение розового призрака
        private void moovePink()
        {
            pinkGhost.Left -= pinkGhostX;
            pinkGhost.Top -= pinkGhostY;

            if (pinkGhost.Top < 0 || pinkGhost.Top > 740)
            {
                pinkGhostY = -pinkGhostY;
            }

            if (pinkGhost.Left < 0 || pinkGhost.Left > this.Width-50)
            {
                pinkGhostX = -pinkGhostX;
            }
        }

        // Движение пакмана
        private void moovePacman()
        {
            if (goleft)
            {
                pacman.Left -= playerSpeed;
                animation(pacmanStop, pacmanLeft);
            }
            if (goright)
            {
                pacman.Left += playerSpeed;
                animation(pacmanStop, pacmanRight);
            }
            if (godown)
            {
                pacman.Top += playerSpeed;
                animation(pacmanStop, pacmanDown);
            }
            if (goup)
            {
                pacman.Top -= playerSpeed;
                animation(pacmanStop, pacmanUp);
            }

            OutOfBounds(pacman);
        }

        private void mainGameTimer(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            moovePacman();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            if (goleft)
                            {
                                pacman.Image = Properties.Resources.pacmanEatLeft;
                            }
                            if (goright)
                            {
                                pacman.Image = Properties.Resources.pacman2;
                            }
                            if (godown)
                            {
                                pacman.Image = Properties.Resources.pacmanEatDown;
                            }
                            if (goup)
                            {
                                pacman.Image = Properties.Resources.pacmanEatUP;
                            }
                            score += 1;
                            Controls.Remove(x as Control);
                            spawnCoin(1);
                        }
                    }

                    if ((string)x.Tag == "wall" || (string)x.Tag == "ghost" || (string)x.Tag == "ghostSpecial2")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("You Lose!", score);
                        }


                        if (pinkGhost.Bounds.IntersectsWith(x.Bounds))
                        {
                            pinkGhostY = -pinkGhostY;
                        }
                    }
                    if ((string)x.Tag == "ghostSpecial" || (string)x.Tag == "ghostSpecial2")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameOver("You Lose!", score);
                        }
                    }
                }
            }

            switch (score)
            {
                case 20:
                    playerSpeed = 8;
                    yellowGhostSpeed = 5;
                    pinkGhostSpeed = 8;
                    redGhostSpeed = 6;
                    break;
                case 50:
                    playerSpeed = 9;
                    yellowGhostSpeed = 6;
                    pinkGhostSpeed = 9;
                    redGhostSpeed = 8;
                    break;

            }

            // движение красного
            mooveRed();

            //движение желтого
            mooveYellow();

            // движение розового
            moovePink();
        }


        // Спавн монет
        private void spawnCoin(int num)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < num; i++)
            {
                int rnd = random.Next(100, this.Width -100);
                int rnd2 = random.Next(20, 700);
                Object coin = new Object();
                Controls.Add(coin.CreateCoin(rnd, rnd2));

                foreach (Control x in this.Controls)
                {
                    foreach (Control y in this.Controls)
                    {
                        if (x is PictureBox || y is PictureBox)
                        {
                            if ((string)x.Tag == "wall" && (string)y.Tag == "coin")
                            {
                                if (y.Bounds.IntersectsWith(x.Bounds))
                                {
                                    Controls.Remove(y as Control);
                                    spawnCoin(1);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void spawnWall(int num)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < num; i++)
            {
                int rnd = random.Next(100, this.Width - 100);
                int rnd2 = random.Next(20, 700);
                Object wall = new Object();
                Controls.Add(wall.CreateWall(rnd, rnd2));

                foreach (Control x in this.Controls)
                {
                    foreach (Control y in this.Controls)
                    {
                        if (x is PictureBox || y is PictureBox)
                        {
                            if ((string)x.Tag == "ghost" && (string)y.Tag == "wall")
                            {
                                if (y.Bounds.IntersectsWith(x.Bounds))
                                {
                                    Controls.Remove(y as Control);
                                }
                            }
                        }
                        if((string)y.Tag == "wall")
                        {
                            if (y.Bounds.IntersectsWith(pacman.Bounds))
                            {
                                Controls.Remove(y as Control);
                            }
                        }
                    }
                }
            }
        }

        // Уничтожение монет
        private void deliteCoin()
        {
            foreach (Control x in this.Controls)
            {
                if ((string)x.Tag == "coin")
                {
                    Controls.Remove(x as Control);
                }
            }
        }

        // Уничтожение стен
        private void deliteWall()
        {
            foreach (Control x in this.Controls)
            {
                if ((string)x.Tag == "wall")
                {
                    Controls.Remove(x as Control);
                }
            }
        }

        private void resetGame()
        {
            label1.Visible = false;
            deliteCoin();
            deliteWall();
            spawnWall(10);
            spawnCoin(10);
            txtScore.Text = "Score: 0";
            score = 0;
            rnd = 3;

            pinkGhostSpeed = 7;
            yellowGhostSpeed = 4;
            pinkGhostX = pinkGhostSpeed;
            pinkGhostY = pinkGhostSpeed;
            yellowGhostX = yellowGhostSpeed;
            yellowGhostY = yellowGhostSpeed;
            playerSpeed = 7;
            redGhostSpeed = 5;

            isGameOver = false;

            pacman.Left = 10;
            pacman.Top = 80;

            redGhost.Left = 480;
            redGhost.Top = 140;

            yellowGhost.Left = 640;
            yellowGhost.Top = 470;

            pinkGhost.Left = 930;
            pinkGhost.Top = 250;

            gameTimer.Start();
        }

        private void gameOver(string message, int score)
        {
            //deliteWall();
            //deliteCoin();

            label1.Visible = true;

            Save save = new Save();

            save.WriteScore(score);

            isGameOver = true;

            gameTimer.Stop();

            txtScore.Text = "Score: " + score + Environment.NewLine + message;
        }
    }

}
