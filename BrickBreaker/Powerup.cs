using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace BrickBreaker
{
    internal class Powerup
    {
        public int x, y, xSpeed, ySpeed;
        public int size = 5;

        List<Ball> pballs = new List<Ball>();
        List<Block> bricksList = new List<Block>();

        public Powerup(int _x, int _y, int _xSpeed, int _ySpeed)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        public void Move()
        {
            y += ySpeed;
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

        private void firePU()
        {
            
        }


    }
}
