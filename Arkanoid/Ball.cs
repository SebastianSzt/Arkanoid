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

        private Paddle GamePaddle;

        Random random = new Random();

        public int AccelerateVY 
        { 
            get { return vY; } 
            set 
            {
                if (vY < 0)
                    vY -= value;
                else if (vY > 0)
                    vY += value;
            } 
        }

        public Ball(int posX, int posY, int width, int height, Color color, int vX, int vY, int panelWidth, int panelHeight, Paddle GamePaddle) : base(posX, posY, width, height, color) 
        { 
            this.vX = vX;
            this.vY = vY;
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            this.GamePaddle = GamePaddle;
        }

        public override void Draw(PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(color);
            e.Graphics.FillEllipse(brush, new RectangleF(posX, posY, width, height));
            brush.Dispose();
        }

        public void PositionWithPaddle()
        {
            posX = GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2) - (width / 2);
            posY = GamePaddle.PaddlePosY - height;
        }

        public override void Move()
        {
            posX += vX;
            posY += vY;
        }

        public void CheckColisionWithPaddle()
        {
            RectangleF ballRect = new RectangleF(posX - 1, posY - 1, width + 2, height + 2);
            Rectangle paddleRect = new Rectangle(GamePaddle.PaddlePosX - 1, GamePaddle.PaddlePosY - 1, GamePaddle.PaddleWidth + 2, GamePaddle.PaddleHeight + 2);

            double paddleFriction = 0.3;

            if (ballRect.IntersectsWith(paddleRect))
            {
                if (posX + width / 2 <= GamePaddle.PaddlePosX && posY + width / 2 >= GamePaddle.PaddlePosY)
                {
                    posX = GamePaddle.PaddlePosX - width;

                    if (vX < 0 && GamePaddle.PaddleVX < 0)
                        vX = vX + (int)Math.Round((1.0f - paddleFriction) * GamePaddle.PaddleVX);
                    else if (vX > 0 && GamePaddle.PaddleVX < 0)
                        vX = -vX + (int)Math.Round(paddleFriction * GamePaddle.PaddleVX);
                    else
                        vX = -vX;
                }
                else if (posX + width / 2 >= GamePaddle.PaddlePosX + GamePaddle.PaddleWidth && posY + width / 2 >= GamePaddle.PaddlePosY)
                {
                    posX = GamePaddle.PaddlePosX + GamePaddle.PaddleWidth;

                    if (vX < 0 && GamePaddle.PaddleVX > 0)
                        vX = -vX + (int)Math.Round(paddleFriction * GamePaddle.PaddleVX);
                    else if (vX > 0 && GamePaddle.PaddleVX > 0)
                        vX = vX + (int)Math.Round((1.0f - paddleFriction) * GamePaddle.PaddleVX);
                    else
                        vX = -vX;
                }
                else
                {
                    posY = GamePaddle.PaddlePosY - height;
                   
                    if ((vX < 0 && GamePaddle.PaddleVX < 0) || (vX > 0 && GamePaddle.PaddleVX > 0))
                        vX = vX + (int)Math.Round((random.NextDouble() * 0.3 + 0.2) * GamePaddle.PaddleVX);
                    else if ((vX < 0 && GamePaddle.PaddleVX > 0) || (vX > 0 && GamePaddle.PaddleVX < 0))
                        vX = -vX - (int)Math.Round((random.NextDouble() * 0.3 + 0.3) * GamePaddle.PaddleVX);
                    vY = -vY;
                }
            }
        }

        public void CheckColisionWithWalls()
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

            if (posY + vY < 0)
            {
                posY = 0;
                vY = -vY;
            }
        }

        public bool CheckRoundFail()
        {
            if (posY + height >= panelHeight)
                return true;
            return false;
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