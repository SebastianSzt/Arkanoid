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
        private int vX;
        private int panelWidth;

        public int PaddlePosX { get { return posX; } }
        public int PaddlePosY { get { return posY; } }
        public int PaddleWidth { get { return width; } }
        public int PaddleHeight { get { return height; } }
        public int PaddleVX { get { return vX; } set { vX = value; } }

        public Paddle(int posX, int posY, int width, int height, Color color, int panelWidth) : base(posX, posY, width, height, color)
        {
            vX = 0;
            this.panelWidth = panelWidth;
        }

        public override void Draw(PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(color);
            e.Graphics.FillRectangle(brush, new Rectangle(posX, posY, width, height));
            brush.Dispose();
        }

        
        public void SetDirection(Keys e, int velocity)
        {
            if (e == Keys.A)
            {
                if (posX - velocity >= 0)
                    vX = -velocity;
                else
                {
                    posX = 0;
                    vX = 0;
                } 
            }
            else if (e == Keys.D)
            {
                if (posX + width + velocity <= panelWidth)
                    vX = velocity;
                else
                {
                    posX = panelWidth - width;
                    vX = 0;
                }
            }
        }

        public override void Move()
        {
            posX += vX;
        }

        public override void ChangeSize(float xRatio, float yRatio)
        {
            panelWidth = (int)Math.Round(Math.Round(panelWidth / this.xRatio) * xRatio);
            posX = (int)Math.Round(Math.Round(posX / this.xRatio) * xRatio);
            posY = (int)Math.Round(Math.Round(posY / this.yRatio) * yRatio);
            width = (int)Math.Round(Math.Round(width / this.xRatio) * xRatio);
            height = (int)Math.Round(Math.Round(height / this.yRatio) * yRatio);
            vX = (int)Math.Round(Math.Round(vX / this.xRatio) * xRatio);

            this.xRatio = xRatio;
            this.yRatio = yRatio;
        }
    }
}