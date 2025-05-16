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
        public float x, y, size;
        public Color color;
        public bool collected; // Flag to check if the power-up has been collected

        public Powerup(float _x, float _y, float _size, Color _color)
        {
            x = _x;
            y = _y;
            size = _size;
            color = _color;
            collected = false;
        }

        public void Draw(PaintEventArgs e)
        {
            if (!collected) // Only draw if not collected
            {
                SolidBrush powerUpBrush = new SolidBrush(color);
                e.Graphics.FillRectangle(powerUpBrush, x, y, size, size);
                powerUpBrush.Dispose();
            }
        }

        public void Move()
        {
            y += 2; // Make the power-up fall
        }
    }

}
