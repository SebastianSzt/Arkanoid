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

        private int originalPosX;
        private int originalPosY;
        private int originalWidth;
        private int originalHeight;
        private int originalVX;
        private int originalVY;
        private int originalPanelWidth;

        public int AccelerateBall { get { return vY; } set { vY = value; } }

        public Ball(int posX, int posY, int width, int height, Color color, int vX, int vY, int panelWidth, int panelHeight) : base(posX, posY, width, height, color) 
        { 
            this.vX = vX;
            this.vY = vY;
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;

            originalPosX = posX;
            originalPosY = posY;
            originalWidth = width;
            originalHeight = height;
            originalVX = vX;
            originalVY = vY;
            originalPanelWidth = panelWidth;
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
                originalPosX = 0;
                originalVX = -originalVX;

                posX = 0;
                vX = -vX;
            }
            else if (posX + width + vX > panelWidth)
            {
                originalPosX = originalPanelWidth - originalWidth;
                originalVX = -originalVX;

                posX = panelWidth - width;
                vX = -vX;
            }
            else
            {
                originalPosX += originalVX;

                posX += vX;
            }

            if (posY + vY < 0)
            {
                originalPosY = 0;
                originalVY = -originalVY;

                posY = 0;
                vY = -vY;
            }
            else
            {
                originalPosY += originalVY;

                posY += vY;
            }
        }

        public void MoveX(int paddleVX)
        {
            posX += paddleVX;
            originalPosX += paddleVX;
        }

        public bool CheckNextMove()
        {
            if (posY + vY + height > panelHeight)
            {
                return false;
            }
            return true;
        }

        public void ChangeSize(float xRatio, float yRatio)
        {
            panelWidth = (int)(originalPanelWidth * xRatio);
            posX = (int)(originalPosX * xRatio);
            posY = (int)(originalPosY * yRatio);
            width = (int)(originalWidth * xRatio);
            height = (int)(originalHeight * yRatio);
            vX = (int)(originalVX * xRatio);
            vY = (int)(originalVX * yRatio);
        }
    }
}