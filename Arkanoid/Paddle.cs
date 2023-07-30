using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanoid
{
    internal class Paddle : GameObject
    {
        public int vX;
        private int panelWidth;

        public int posXValue { get { return posX; } }
        public int widthValue { get { return width; } }

        public Paddle(int posX, int posY, int width, int height, Color color, int vX, int panelWidth) : base(posX, posY, width, height, color)
        {
            this.vX = vX;
            this.panelWidth = panelWidth;
        }

        public override void Draw(PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(color);
            e.Graphics.FillRectangle(brush, new Rectangle(posX, posY, width, height));
            brush.Dispose();
        }

        public override void Move()
        {
            if (posX + vX < 0)
            {
                posX = 0;
            }
            else if (posX + width + vX > panelWidth)
            {
                posX = panelWidth - width;
            }
            else
            { 
                posX += vX;
            }
        }
    }
}
