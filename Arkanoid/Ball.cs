using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Drawing.Printing;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Arkanoid
{
    [Serializable]
    internal class Ball : GameObject
    {
        private int panelWidth;
        private int panelHeight;

        private int vX;
        private int vY;

        private Grid GameGrid;
        private Paddle GamePaddle;

        public int BallWidth { get { return width; } }
        public int BallHeight { get { return height; } }

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

        public Ball(int panelWidth, int panelHeight, int posX, int posY, int width, int height, Color color, int vX, int vY, Grid GameGrid, Paddle GamePaddle) : base(posX, posY, width, height, color) 
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;

            this.vX = vX;
            this.vY = vY;
            
            this.GameGrid = GameGrid;
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

            double paddlePower = 0.3;

            if (ballRect.IntersectsWith(paddleRect))
            {
                if (posX < GamePaddle.PaddlePosX + GamePaddle.PaddleWidth / 2 && posY + height * 3 / 4 >= GamePaddle.PaddlePosY)
                {
                    //Debug.WriteLine("Interakcja lewo");
                    posX = GamePaddle.PaddlePosX - width;
                    posY += -vY / 2;

                    if (vX < 0 && GamePaddle.PaddleVX < 0)
                        vX = vX + (int)Math.Round((1.0f - paddlePower) * GamePaddle.PaddleVX);
                    else if (vX > 0 && GamePaddle.PaddleVX < 0)
                        vX = -vX + (int)Math.Round(paddlePower * GamePaddle.PaddleVX);
                    else
                        vX = -vX;
                }
                else if (posX + width > GamePaddle.PaddlePosX + GamePaddle.PaddleWidth / 2 && posY + height * 3 / 4 >= GamePaddle.PaddlePosY)
                {
                    //Debug.WriteLine("Interakcja Prawo");
                    posX = GamePaddle.PaddlePosX + GamePaddle.PaddleWidth;
                    posY += -vY / 2;

                    if (vX < 0 && GamePaddle.PaddleVX > 0)
                        vX = -vX + (int)Math.Round(paddlePower * GamePaddle.PaddleVX);
                    else if (vX > 0 && GamePaddle.PaddleVX > 0)
                        vX = vX + (int)Math.Round((1.0f - paddlePower) * GamePaddle.PaddleVX);
                    else
                        vX = -vX;
                }
                else
                {
                    Random random = new Random();

                    posX += -vX / 2;
                    posY = GamePaddle.PaddlePosY - height;
                   
                    if ((vX < 0 && GamePaddle.PaddleVX < 0) || (vX > 0 && GamePaddle.PaddleVX > 0))
                        vX += (int)Math.Round((random.NextDouble() * 0.10 + 0.05) * vX);
                    else if ((vX < 0 && GamePaddle.PaddleVX > 0) || (vX > 0 && GamePaddle.PaddleVX < 0))
                        vX = -vX + (int)Math.Round((random.NextDouble() * 0.25 + 0.10) * vX);
                    else
                        vX = vX + (int)Math.Round((random.NextDouble() * 0.35 - 0.10) * vX);
                    vY = -vY;
                }
            }
        }

        public int CheckColisionWithBricks()
        {
            int points = 0;
            RectangleF ballRect = new RectangleF(posX - 1, posY - 1, width + 2, height + 2);

            Queue<Brick> collidingBricks = new Queue<Brick>();

            for (int row = 0; row < GameGrid.Rows; row++)
            {
                for (int col = 0; col < GameGrid.Columns; col++)
                {
                    if (GameGrid[row, col] != null)
                    {
                        Brick brick = GameGrid[row, col];
                        Rectangle brickRect = new Rectangle(brick.BrickPosX, brick.BrickPosY, brick.BrickWidth, brick.BrickHeight);

                        if (ballRect.IntersectsWith(brickRect))
                            collidingBricks.Enqueue(brick);
                    }
                }
            }

            Brick closestBrick = null;
            double closestDistance = double.MaxValue;

            foreach (Brick brick in collidingBricks)
            {
                double distance = Math.Sqrt(Math.Pow(brick.BrickPosX  - posX, 2) + Math.Pow(brick.BrickPosY - posY, 2));
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBrick = brick;
                }
            }

            if (closestBrick != null)
            {
                Random random = new Random();

                bool collisionHandled = false;

                if (!collisionHandled && vX > 0 && posX < closestBrick.BrickPosX + closestBrick.BrickWidth / 2)
                {
                    if (posY + height * 2 / 3 >= closestBrick.BrickPosY && posY + height / 3 <= closestBrick.BrickPosY + closestBrick.BrickHeight)
                    {
                        //Debug.WriteLine("Interakcja lewo");
                        posX = closestBrick.BrickPosX - width;
                        posY += -vY / 2;
                        vX = -Math.Abs(vX);
                        collisionHandled = true;
                    }
                    //else
                    //    Debug.WriteLine("Lewy nie reaguje");
                }
                if (!collisionHandled && vX < 0 && posX + width > closestBrick.BrickPosX + closestBrick.BrickWidth / 2)
                {
                    if (posY + height * 2 / 3 >= closestBrick.BrickPosY && posY + height / 3 <= closestBrick.BrickPosY + closestBrick.BrickHeight)
                    {
                        //Debug.WriteLine("Interakcja prawo");
                        posX = closestBrick.BrickPosX + closestBrick.BrickWidth;
                        posY += -vY / 2;
                        vX = Math.Abs(vX);
                        collisionHandled = true;
                    }
                    //else
                    //    Debug.WriteLine("Prawy nie reaguje");
                }
                if (!collisionHandled && vY < 0 && posY + height > closestBrick.BrickPosY + closestBrick.BrickHeight / 2)
                {
                    //Debug.WriteLine("Interakcja dol");
                    posX += -vX / 2;
                    posY = closestBrick.BrickPosY + closestBrick.BrickHeight;
                    vY = Math.Abs(vY);
                    collisionHandled = true;
                }
                if (!collisionHandled && vY > 0 && posY <= closestBrick.BrickPosY + closestBrick.BrickHeight / 2)
                {
                    //Debug.WriteLine("Interakcja gora");
                    posX += -vX / 2;
                    posY = closestBrick.BrickPosY - height;
                    vY = -Math.Abs(vY);
                    collisionHandled = true;
                }

                points += 50;

                GameGrid[closestBrick.BrickRow, closestBrick.BrickColumn] = null;
            }

            return points;
        }

        public void CheckColisionWithWalls()
        {
            if (posX < 0)
            {
                posX = 0;
                posY += -vY / 2;
                vX = -vX;
            }
            else if (posX + width > panelWidth)
            {
                posX = panelWidth - width;
                posY += -vY / 2;
                vX = -vX;
            }

            if (posY < 0)
            {
                posX += -vX / 2;
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