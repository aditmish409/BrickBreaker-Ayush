using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    internal class Powerup
    {
        public int x, y;
        public int size = 5;
        public int ySpeed;
        //gameBall = new Ball(GameScreen.gameScreenWidth / 2, GameScreen.Height - 40, 10, -8);

        List<Ball> balls = new List<Ball>();
        //List<Bricks> bricksList = new List<Bricks>();

        public Powerup(int _x, int _y, int _xSpeed, int _ySpeed)
        {
            x = _x;
            y = _y;
            ySpeed = _ySpeed;
        }

        public void Move()
        {
            y += ySpeed;
        }

        private void firePU()
        {
            //if (gameBall.IntersectsWit
        }
    }
}
