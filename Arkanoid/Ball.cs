using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace Arkanoid
{
    internal class Ball : GameObject
    {
        private int vX;
        private int vY;
        private int panelWidth;
        private int panelHeight;

        public Ball(int posX, int posY, int width, int height, Color color, int vX, int vY, int panelWidth, int panelHeight) : base(posX, posY, width, height, color) 
        { 
            this.vX = vX;
            this.vY = vY;
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
        }

        public override void Draw(PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(color);
            e.Graphics.FillEllipse(brush, new RectangleF(posX, posY, width, height));
        }

        public void Move()
        {
            if (posX + vX < 0)
            {
                posX = 0;
                vX = -vX;
            }
            else if (posX + width + vX > panelWidth)
            {
                posX = panelWidth - width;
                vX = -vX;
            }
            else
            {
                posX += vX;
            }

            if (posY + vY < 0)
            {
                posY = 0;
                vY = -vY;
            }
            else
            {
                posY += vY;
            }
        }

        public bool CheckNextMove()
        {
            if (posY + vY + height > panelHeight)
            {
                return false;
            }
            return true;
        }
    }
}