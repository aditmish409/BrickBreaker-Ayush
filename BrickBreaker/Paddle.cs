using System.Drawing;
using System.Windows.Forms;

namespace BrickBreaker
{
    public class Paddle
    {
        public float x, y, width, height, speed, move;
        public Color colour;

        public Paddle(float _x, float _y, float _width, float _height, float _speed, Color _colour)
        {
            x = _x;
            y = _y;
            width = _width;
            height = _height;
            speed = _speed;
            colour = _colour;
        }

        public void Move(string direction, UserControl UC)
        {
            if (x + width <= UC.Width && x >= 0)
            {

                switch (direction)
                {
                    case "left":

                        if (move > 0)
                        {
                            move += -10 * speed;
                        }
                        else
                        {
                            move += -speed;
                        }

                        break;

                    case "right":

                        if (move < 0)
                        {
                            move += 10 * speed;
                        }
                        else
                        {
                            move += speed;
                        }

                        break;

                    default:
                        move = (float)(move / 1.2);
                        break;
                }

            }
            else
            {

                if (x < 0)
                {

                    move = 0;

                    x = 0;

                }
                else if (x > UC.Width - width)
                {
                    move = 0;

                    x = UC.Width - width;
                }

            }

            x += move;

        }
    }
}
