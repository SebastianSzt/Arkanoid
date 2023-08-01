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

        private int originalPosX;
        private int originalPosY;
        private int originalWidth;
        private int originalHeight;
        public int originalVX;
        private int originalPanelWidth;

        public int posXValue { get { return posX; } }
        public int widthValue { get { return width; } }

        public Paddle(int posX, int posY, int width, int height, Color color, int vX, int panelWidth) : base(posX, posY, width, height, color)
        {
            this.vX = vX;
            this.panelWidth = panelWidth;

            originalPosX = posX;
            originalPosY = posY;
            originalWidth = width;
            originalHeight = height;
            originalVX = vX;
            originalPanelWidth = panelWidth;
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
                originalPosX = 0;
            }
            else if (posX + width + vX > panelWidth)
            {
                posX = panelWidth - width;
                originalPosX = originalPanelWidth - originalWidth;
            }
            else
            { 
                posX += vX;
                originalPosX += originalVX;
            }
        }

        public void ChangeSize(float xRatio, float yRatio)
        {
            panelWidth = (int)(originalPanelWidth * xRatio);
            posX = (int)(originalPosX * xRatio);
            posY = (int)(originalPosY * yRatio);
            width = (int)(originalWidth * xRatio);
            height = (int)(originalHeight * yRatio);
            vX = (int)(originalVX * xRatio);
        }
    }
}
