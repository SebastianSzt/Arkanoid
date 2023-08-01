using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Arkanoid
{
    internal class Ball : GameObject
    {
        private int vX;
        private int vY;
        private int panelWidth;
        private int panelHeight;

        public int AccelerateBall { get { return vY; } set { vY = value; } }

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
            brush.Dispose();
        }

        public override void Move()
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

        public void MoveX(int paddleVX)
        {
            posX += paddleVX;
        }

        public bool CheckNextMove()
        {
            if (posY + vY + height > panelHeight)
            {
                return false;
            }
            return true;
        }

        public override void ChangeSize(float xRatio, float yRatio)
        {
            panelWidth = (int)Math.Round(Math.Round(panelWidth / this.xRatio) * xRatio);
            panelHeight = (int)Math.Round(Math.Round(panelHeight / this.yRatio) * yRatio);
            posX = (int)Math.Round(Math.Round(posX / this.xRatio) * xRatio);
            posY = (int)Math.Round(Math.Round(posY / this.yRatio) * yRatio);
            width = (int)Math.Round(Math.Round(width / this.xRatio) * xRatio);
            height = (int)Math.Round(Math.Round(height / this.yRatio) * yRatio);
            vX = (int)Math.Round(Math.Round(vX / this.xRatio) * xRatio);
            vY = (int)Math.Round(Math.Round(vY / this.yRatio) * yRatio);

            this.xRatio = xRatio;
            this.yRatio = yRatio;
        }
    }
}