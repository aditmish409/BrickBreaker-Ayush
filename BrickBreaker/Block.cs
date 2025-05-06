using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickBreaker
{
    public class Block
    {
        public int width = 50;
        public int height = 25;

        public int x;
        public int y; 
        public int hp;
        public int maxHp;
        public Color color;
        public Image image;

        public static Random rand = new Random();

        public Block(int _x, int _y, int _hp, Color _color)
        {
            x = _x;
            y = _y;
            hp = _hp;
            color = _color;
        }
    }
}