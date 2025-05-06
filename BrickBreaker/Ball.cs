using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace BrickBreaker
{
    public class Ball
    {

        public float x, y, xSpeed = 6, ySpeed = 6, size;

        public Color colour;

        public static Random rand = new Random();

        public bool IsPaused = true;

        Paddle pad;

        public Ball(float _x, float _y, float _xSpeed, float _ySpeed, float _ballSize)
        {
            x = _x;
            y = _y;

            xSpeed = _xSpeed;
            ySpeed = _ySpeed;

            size = _ballSize;

        }

        public void Move()
        {

            if (IsPaused == false)
            {

                x = x + xSpeed;

                y = y - ySpeed;

                if (y < 0)
                {
                    ySpeed = -ySpeed;

                    y = 5;
                }

            }
            else
            {

                if (pad != null)
                {

                    xSpeed = 0;

                    ySpeed = 0;

                    x = (pad.x + (pad.width / 2) - (size / 2));

                    y = (pad.y - pad.height);

                }

            }

        }

        public bool BlockCollision(Block b)
        {

            RectangleF blockRec = new RectangleF(b.x, b.y, b.width, b.height);
            RectangleF ballRec = new RectangleF(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {
                float ySSwitch = ySpeed;

                if (x <= b.x + b.width && x >= b.x - size)
                {

                    ySpeed = -ySpeed;

                }

                if (y <= b.y + b.height && y >= b.y - size)
                {

                    xSpeed = -xSpeed;

                }

                y -= 10;

            }

            return blockRec.IntersectsWith(ballRec);
        }

        public void PaddleCollision(Paddle p)
        {
            RectangleF ballRec = new RectangleF(x, y, size, size);
            RectangleF paddleRec = new RectangleF(p.x, p.y, p.width, p.height);

            pad = p;

            if (ballRec.IntersectsWith(paddleRec) && y >= ballRec.Y)
            {

                xSpeed += p.move / 3;

                ySpeed = -ySpeed;

                y = p.y - ballRec.Height;
            }
        }

        public void WallCollision(UserControl UC)
        {
            // Collision with left or right wall
            if (x <= 0 || x >= (UC.Width - size))
            {
                xSpeed = -xSpeed;
            }

            // Collision with top wall
            if (y <= 0)
            {
                ySpeed = -ySpeed;

            }
        }

        public bool BottomCollision(UserControl UC)
        {
            Boolean didCollide = false;

            if (y >= UC.Height)
            {

                if (pad != null)
                {

                    x = (pad.x + (pad.width / 2) - (size / 2));

                    y = (pad.y - pad.height);

                    ySpeed = -6;
                    xSpeed = 0;

                }

                didCollide = true;
            }

            return didCollide;
        }

    }
}
