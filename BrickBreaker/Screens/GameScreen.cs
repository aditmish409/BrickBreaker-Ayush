/*  Created by: 
 *  Project: Brick Breaker
 *  Date: 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Drawing.Text;
using System.Xml;
using BrickBreaker.Screens;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, escDown;

        // Game values
        int lives;
        int level = 3;
        public static int score;

        // Paddle and Ball objects
        Paddle paddle;
        Ball ball;

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }


        public void OnStart()
        {
            //set life counter
            lives = 3;
            score = 0;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            float paddleSpeed = (float)0.5;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            float ballX = this.Width / 2 - 10;
            float ballY = this.Height - paddle.height - 80;

            // Creates a new ball
            int xSpeed = 6;
            int ySpeed = 6;
            int ballSize = 20;
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);

            #region Creates blocks for generic level. Need to replace with code that loads levels.
            ExtractLevel(level);
            #endregion
            

            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:

                    if (ball.IsPaused)
                    {
                        CheckIfBallPaused();
                    }
                    break;
                case Keys.Escape:
                    escDown = true;
                    break;

                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                default:
                    break;
            }
        }

        private bool CheckIfBallPaused()
        {
            // Noble Method

            if (ball.IsPaused == true)
            {

                ball.IsPaused = false;

                ball.xSpeed = paddle.move;

                ball.ySpeed = 6;

                return true;
            }
            else
            {
                return false;
            }
        }

        private void MovePaddleCheck()
        {
            //
            // NOBLE METHOD
            //

            if (leftArrowDown)
            {

                paddle.Move("left", this);

            }
            else if (rightArrowDown)
            {

                paddle.Move("right", this);

            }
            else
            {

                paddle.Move(String.Empty, this);

            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            // Move the paddle
            MovePaddleCheck();

            // Move ball
            ball.Move();

            // Check for collision with top and side walls
            ball.WallCollision(this);

            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this))
            {
                lives--;

                // Moves the ball back to origin
                ball.IsPaused = true;

                if (lives == 0)
                {
                    Form1.ChangeScreen(this, new GameOverScreen());
                    gameTimer.Enabled = false;
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle);

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    score += 10;
                    if (b.hp <= 0)
                    {
                        blocks.Remove(b); //Remove the brick 
                        break;
                    }
                    
                    

                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        level++;
                    }
                    break;
                }
            }

            //If the escape key is pressed then stop the game
            if (escDown == true)
            {
                gameTimer.Stop();
                Form1.ChangeScreen(this, new MenuScreen());
            }

            //redraw the screen
            Refresh();
        }

        private void nextLevelButton_Click(object sender, EventArgs e)
        {
            blocks.Clear();
            level++;
            ExtractLevel(level);
        }

        public void ExtractLevel(int level)
        {
            XmlReader reader = XmlReader.Create(Application.StartupPath + $"/Resources/level{level}.xml");
            while (reader.Read())
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        int x = Convert.ToInt16(reader.ReadString());

                        reader.ReadToNextSibling("y");
                        int y = Convert.ToInt16(reader.ReadString());

                        reader.ReadToNextSibling("hp");
                        int hp = Convert.ToInt16(reader.ReadString());

                        reader.ReadToNextSibling("colour");
                        string colorString = reader.ReadString();
                        Color color = Color.FromName(colorString);

                        Block b = new Block(x, y, hp, color);
                        blocks.Add(b);
                    }
                }
            }
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //Update the Score Label
            scoreLabel.Text = $"{score}";

            // Draws paddle
            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(paddleBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draws blocks
            foreach (Block b in blocks)
            {
                using (SolidBrush brush = new SolidBrush(b.color))
                {
                    e.Graphics.FillRectangle(brush, b.x, b.y, b.width, b.height);
                }
            }

            // Draws ball
            e.Graphics.FillEllipse(ballBrush, ball.x, ball.y, ball.size, ball.size);
        }
    }
}
