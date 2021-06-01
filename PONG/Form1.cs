using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PONG
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(200, 170, 10, 60);
        Rectangle player2 = new Rectangle(250, 170, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 4;
        int ballXSpeed = 6;
        int ballYSpeed = 6;

        bool wDown = false;
        bool sDown = false;
        bool aLeft = false;
        bool dRight = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool LeftArrowLeft = false;
        bool RightArrowRight = false;

        string activePlayer = "player1";

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush greenBrush = new SolidBrush(Color.LawnGreen);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen whitePen = new Pen(Color.White);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aLeft = true;
                    break;
                case Keys.D:
                    dRight = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    RightArrowRight = true;
                    break;
                case Keys.Left:
                    LeftArrowLeft = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aLeft = false;
                    break;
                case Keys.D:
                    dRight = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    RightArrowRight = false;
                    break;
                case Keys.Left:
                    LeftArrowLeft = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            //move players
            if (wDown == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (aLeft == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }

            if (dRight == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            if (RightArrowRight == true && player2.Y < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
            }

            if (LeftArrowLeft == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }
            //ball collision with top and bottom walls
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }

            //ball collision with right wall
            if(ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;
            }

            //ball collision with player1 if its their turn and the ball is moving left
            if (player1.IntersectsWith(ball) && activePlayer == "player1" && ballXSpeed < 0)
            {
                ballXSpeed--; //personally I prefer it with just the X getting faster
                ballXSpeed *= -1;
                ball.X = player1.X + ball.Width;
                activePlayer = "player2"; //when the ball comes in contact, active player is switched
            }
            else if (player2.IntersectsWith(ball) && activePlayer == "player2" && ballXSpeed < 0) 
            {
                ballXSpeed--;
                ballXSpeed *= -1;
                ball.X = player2.X + ball.Width;
                activePlayer = "player1"; //when the ball come in contact, active player is switched
            }

            //check for point scored
            if (ball.X < 0)
            {
                if (activePlayer == "player1")
                {
                    player2Score++;
                }
                else if (activePlayer == "player2")
                {
                    player1Score++;
                }

                p1ScoreLabel.Text = $"{player1Score}";
                p2ScoreLabel.Text = $"{player2Score}";

                ball.X = 295;
                ball.Y = 195;
                ballXSpeed = 6;

                player1.Y = 170;
                player2.Y = 170;
            }
           

            //check for game over
            if (player1Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Blue Wins!!";
            }
            else if (player2Score == 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Green Wins!!";
            }

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(whiteBrush, ball);
            if (activePlayer == "player1")
            {
                e.Graphics.DrawRectangle(whitePen, player1);
            }
            else if (activePlayer == "player2")
            {
                e.Graphics.DrawRectangle(whitePen, player2);
            }
        }
    }
}
