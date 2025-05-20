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
    public class Ball
    {
        public float x, y, xSpeed = 6, ySpeed = 6, size;

        public Color colour;

        public static Random rand = new Random();

        public bool IsPaused = true;

        Paddle pad;

        public bool hasPowerUp; // Flag to indicate if the ball has the power-up

        List<Block> blocks = new List<Block>();

        public Ball(float _x, float _y, float _xSpeed, float _ySpeed, float _ballSize)
        {
            x = _x;
            y = _y;

            xSpeed = _xSpeed;
            ySpeed = _ySpeed;

            size = _ballSize;

            hasPowerUp = false;
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
            if (b.isDestroyed) return false; //if block is already destroyed, no collision

            RectangleF blockRec = new RectangleF(b.x, b.y, b.width, b.height);
            RectangleF ballRec = new RectangleF(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {

                if (x <= b.x + b.width && x >= b.x - size)
                {

                    ySpeed = -ySpeed;

                }

                if (y <= b.y + b.height && y >= b.y - size)
                {

                    xSpeed = -xSpeed;

                }
                return true;

            }
            return false;
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

        //Method to handle power-up effect
        public void ActivatePowerUp(List<Block> blocks, List<Block> destroyedBlocks) //added destroyedBlocks
        {
            if (hasPowerUp)
            {
                // Find the closest blocks to the current ball position.
                List<Block> surroundingBlocks = FindSurroundingBlocks(blocks);

                // Destroy the surrounding blocks.
                foreach (Block block in surroundingBlocks)
                {
                    if (!block.isDestroyed) //destroy only if not already destroyed
                    {
                        block.isDestroyed = true;
                        destroyedBlocks.Add(block); // Add to the destroyed list
                    }
                }
                hasPowerUp = false; // Reset power-up status after activation.
            }
        }

        private List<Block> FindSurroundingBlocks(List<Block> blocks)
        {
            List<Block> surrounding = new List<Block>();
            // Define a small area around the ball.
            float checkRadius = size * 3; // Check within a radius of ball size.

            foreach (Block block in blocks)
            {
                if (!block.isDestroyed) // Only check blocks that are not destroyed
                {
                    float distance = (float)Math.Sqrt(Math.Pow(block.x - x, 2) + Math.Pow(block.y - y, 2));
                    if (distance <= checkRadius)
                    {
                        surrounding.Add(block);
                    }
                }
            }
            return surrounding;
        }
    }

}

