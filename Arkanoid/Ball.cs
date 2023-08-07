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

            if (ballRect.IntersectsWith(paddleRect))
            {
                Random random = new Random();

                double paddleBeta = (GamePaddle.PaddleWidth / 2.0) / (GamePaddle.PaddleHeight / 2.0);
                double paddleAlpha = (GamePaddle.PaddleHeight / 2.0) / (GamePaddle.PaddleWidth / 2.0);
                double ballBeta;
                double ballAlpha;
                if (Math.Abs((GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2.0)) - (posX + (width / 2.0))) == 0 || Math.Abs((GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2.0)) - (posY + (height / 2.0))) == 0)
                {
                    ballBeta = 0;
                    ballAlpha = 0;
                }
                else
                {
                    ballBeta = Math.Abs((GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2.0)) - (posX + (width / 2.0))) / Math.Abs((GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2.0)) - (posY + (height / 2.0)));
                    ballAlpha = Math.Abs((GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2.0)) - (posY + (height / 2.0))) / Math.Abs((GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2.0)) - (posX + (width / 2.0)));
                }

                if (ballBeta == 0 || ballAlpha == 0)
                {
                    if ((GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2)) == (posY + (height / 2)))
                    {
                        if ((posX + (width / 2)) < (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)))
                        {
                            //Debug.WriteLine("Interakcja lewo równo");
                            posX = GamePaddle.PaddlePosX - width;
                            if (vX < 0 && GamePaddle.PaddleVX < 0)
                                vX = vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else if (vX > 0 && GamePaddle.PaddleVX < 0)
                                vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else
                                vX = -vX;
                        }
                        else if ((posX + (width / 2)) > (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)))
                        {
                            //Debug.WriteLine("Interakcja prawo równo");
                            posX = GamePaddle.PaddlePosX + GamePaddle.PaddleWidth;
                            if (vX < 0 && GamePaddle.PaddleVX > 0)
                                vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else if (vX > 0 && GamePaddle.PaddleVX > 0)
                                vX = vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else
                                vX = -vX;
                        }
                    }
                    else if ((GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)) == (posX + (width / 2)))
                    {
                        //Debug.WriteLine("Interakcja góra równo");
                        posX += -vX / 2;
                        posY = GamePaddle.PaddlePosY - height;

                        if ((vX < 0 && GamePaddle.PaddleVX < 0) || (vX > 0 && GamePaddle.PaddleVX > 0))
                            vX += (int)Math.Round((random.NextDouble() * 0.25 + 0.10) * vX);
                        else if ((vX < 0 && GamePaddle.PaddleVX > 0) || (vX > 0 && GamePaddle.PaddleVX < 0))
                            vX = -vX + (int)Math.Round((random.NextDouble() * 0.25 + 0.01) * vX);
                        else
                            vX = vX + (int)Math.Round((random.NextDouble() * 0.35 - 0.10) * vX);
                        vY = -vY;
                    }
                }
                else if (paddleBeta == ballBeta || paddleAlpha == ballAlpha)
                {
                    if ((posX + (width / 2)) < (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)) && (posY + (height / 2)) < (GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2)))
                    {
                        if (vX > 0 && vY > 0)
                        {
                            //Debug.WriteLine("Interakcja lewy górny róg");
                            posX = GamePaddle.PaddlePosX - width;
                            posY = GamePaddle.PaddlePosY - height;
                            if (GamePaddle.PaddleVX < 0)
                                vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else
                                vX = vX + (int)Math.Round((random.NextDouble() * 0.35 - 0.10) * vX);
                            vY = - vY;
                        }
                        else
                        {
                            if (vY > 0)
                            {
                                //Debug.WriteLine("Interakcja lewy górny róg - góra");
                                posX += -vX / 2;
                                posY = GamePaddle.PaddlePosY - height;

                                if (GamePaddle.PaddleVX < 0)
                                    vX += (int)Math.Round((random.NextDouble() * 0.30 + 0.05) * vX);
                                else if (GamePaddle.PaddleVX > 0)
                                    vX = -vX + (int)Math.Round((random.NextDouble() * 0.25 + 0.01) * vX);
                                else
                                    vX = vX + (int)Math.Round((random.NextDouble() * 0.35 - 0.10) * vX);
                                vY = -vY;
                            }
                        }
                    }
                    else if ((posX + (width / 2)) < (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)) && (posY + (height / 2)) > (GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2)))
                    {
                        if (vX > 0)
                        {
                            //Debug.WriteLine("Interakcja lewy dolny róg - lewo");
                            posX = GamePaddle.PaddlePosX - width;

                            if (GamePaddle.PaddleVX < 0)
                                vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else
                                vX = -vX;
                        }
                    }
                    else if ((posX + (width / 2)) > (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)) && (posY + (height / 2)) < (GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2)))
                    {
                        if (vX < 0 && vY > 0)
                        {
                            //Debug.WriteLine("Interakcja prawy górny róg");
                            posX = GamePaddle.PaddlePosX + GamePaddle.PaddleWidth;
                            posY = GamePaddle.PaddlePosY - height;
                            if (GamePaddle.PaddleVX < 0)
                                vX += (int)Math.Round((random.NextDouble() * 0.30 + 0.05) * vX);
                            else if (GamePaddle.PaddleVX > 0)
                                vX = -vX + (int)Math.Round((random.NextDouble() * 0.25 + 0.01) * vX);
                            else
                                vX = vX + (int)Math.Round((random.NextDouble() * 0.35 - 0.10) * vX);
                            vY = -vY;
                        }
                        else
                        {
                            if (vY > 0)
                            {
                                //Debug.WriteLine("Interakcja prawy górny róg - góra");
                                posX += -vX / 2;
                                posY = GamePaddle.PaddlePosY - height;
                                if (GamePaddle.PaddleVX > 0)
                                    vX += (int)Math.Round((random.NextDouble() * 0.30 + 0.05) * vX);
                                else if (GamePaddle.PaddleVX < 0)
                                    vX = -vX + (int)Math.Round((random.NextDouble() * 0.25 + 0.01) * vX);
                                else
                                    vX = vX + (int)Math.Round((random.NextDouble() * 0.35 - 0.10) * vX);
                                vY = -vY;
                            }
                        }
                    }
                    else if ((posX + (width / 2)) > (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)) && (posY + (height / 2)) > (GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2)))
                    {
                        if (vX < 0)
                        {
                            //Debug.WriteLine("Interakcja prawy dolny róg - prawo");
                            posX = GamePaddle.PaddlePosX + GamePaddle.PaddleWidth;

                            if (GamePaddle.PaddleVX > 0)
                                vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else
                                vX = -vX;
                        }
                    }
                }
                else if (ballBeta > paddleBeta)
                {
                    if ((posX + (width / 2)) < (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)))
                    {
                        //Debug.WriteLine("Interakcja lewo");
                        posX = GamePaddle.PaddlePosX - width;

                        if (vX < 0 && GamePaddle.PaddleVX < 0)
                            vX = vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                        else if (vX > 0 && GamePaddle.PaddleVX < 0)
                            vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                        else
                            vX = -vX;
                    }
                    else if ((posX + (width / 2)) > (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)))
                    {
                        //Debug.WriteLine("Interakcja prawo");
                        posX = GamePaddle.PaddlePosX + GamePaddle.PaddleWidth;

                        if (vX < 0 && GamePaddle.PaddleVX > 0)
                            vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                        else if (vX > 0 && GamePaddle.PaddleVX > 0)
                            vX = vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                        else
                            vX = -vX;
                    }
                    else
                    {
                        //Debug.WriteLine("Interakcja lewo / prawo - błąd narożnika");
                        posX += -vX / 2;
                        posY = GamePaddle.PaddlePosY - height;

                        if (GamePaddle.PaddleVX > 0)
                            vX += (int)Math.Round((random.NextDouble() * 0.30 + 0.05) * vX);
                        else if (GamePaddle.PaddleVX < 0)
                            vX = -vX + (int)Math.Round((random.NextDouble() * 0.25 + 0.01) * vX);
                        else
                            vX = vX + (int)Math.Round((random.NextDouble() * 0.35 - 0.10) * vX);
                        vY = -vY;
                    }
                }
                else if (ballAlpha > paddleAlpha && vY > 0)
                {
                    if ((posY + (height / 2)) < (GamePaddle.PaddlePosY + (GamePaddle.PaddleHeight / 2)))
                    {
                        //Debug.WriteLine("Interakcja góra");
                        posX += -vX / 2;
                        posY = GamePaddle.PaddlePosY - height;

                        if ((vX < 0 && GamePaddle.PaddleVX < 0) || (vX > 0 && GamePaddle.PaddleVX > 0))
                            vX += (int)Math.Round((random.NextDouble() * 0.30 + 0.05) * vX);
                        else if ((vX < 0 && GamePaddle.PaddleVX > 0) || (vX > 0 && GamePaddle.PaddleVX < 0))
                            vX = -vX + (int)Math.Round((random.NextDouble() * 0.25 + 0.01) * vX);
                        else
                            vX = vX + (int)Math.Round((random.NextDouble() * 0.35 - 0.10) * vX);
                        vY = -vY;
                    }
                    else
                    {
                        if ((posX + (width / 2)) < (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)))
                        {
                            //Debug.WriteLine("Interakcja lewo");
                            posX = GamePaddle.PaddlePosX - width;

                            if (vX < 0 && GamePaddle.PaddleVX < 0)
                                vX = vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else if (vX > 0 && GamePaddle.PaddleVX < 0)
                                vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else
                                vX = -vX;
                        }
                        else if ((posX + (width / 2)) > (GamePaddle.PaddlePosX + (GamePaddle.PaddleWidth / 2)))
                        {
                            //Debug.WriteLine("Interakcja prawo");
                            posX = GamePaddle.PaddlePosX + GamePaddle.PaddleWidth;

                            if (vX < 0 && GamePaddle.PaddleVX > 0)
                                vX = -vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else if (vX > 0 && GamePaddle.PaddleVX > 0)
                                vX = vX + (int)Math.Round(0.5 * GamePaddle.PaddleVX);
                            else
                                vX = -vX;
                        }
                    }
                }
                //else
                //{
                //    Debug.WriteLine("Błąd");
                //}
            }
        }

        public int CheckColisionWithBricks()
        {
            int points = 0;
            RectangleF ballRect = new RectangleF(posX, posY, width, height);

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
                double brickBeta = (closestBrick.BrickWidth / 2.0) / (closestBrick.BrickHeight / 2.0);
                double brickAlpha = (closestBrick.BrickHeight / 2.0) / (closestBrick.BrickWidth / 2.0);
                double ballBeta;
                double ballAlpha;
                if (Math.Abs((closestBrick.BrickPosX + (closestBrick.BrickWidth / 2.0)) - (posX + (width / 2.0))) == 0 || Math.Abs((closestBrick.BrickPosY + (closestBrick.BrickHeight / 2.0)) - (posY + (height / 2.0))) == 0)
                {
                    ballBeta = 0;
                    ballAlpha = 0;
                }
                else
                {
                    ballBeta = Math.Abs((closestBrick.BrickPosX + (closestBrick.BrickWidth / 2.0)) - (posX + (width / 2.0))) / Math.Abs((closestBrick.BrickPosY + (closestBrick.BrickHeight / 2.0)) - (posY + (height / 2.0)));
                    ballAlpha = Math.Abs((closestBrick.BrickPosY + (closestBrick.BrickHeight / 2.0)) - (posY + (height / 2.0))) / Math.Abs((closestBrick.BrickPosX + (closestBrick.BrickWidth / 2.0)) - (posX + (width / 2.0)));
                }

                if (ballBeta == 0 || ballAlpha == 0)
                {
                    if ((closestBrick.BrickPosY + (closestBrick.BrickHeight / 2)) == (posY + (height / 2)))
                    {
                        if (vX > 0)
                        {
                            //Debug.WriteLine("Interakcja lewo równo");
                            posX = closestBrick.BrickPosX - width;
                            posY += -vY / 2;
                            vX = -vX;
                        }
                        else if (vX < 0)
                        {
                            //Debug.WriteLine("Interakcja prawo równo");
                            posX = closestBrick.BrickPosX + closestBrick.BrickWidth;
                            posY += -vY / 2;
                            vX = -vX;
                        }
                    }
                    else if ((closestBrick.BrickPosX + (closestBrick.BrickWidth / 2)) == (posX + (width / 2)))
                    {
                        if (vY > 0)
                        {
                            //Debug.WriteLine("Interakcja góra równo");
                            posX += -vX / 2;
                            posY = closestBrick.BrickPosY - height;
                            vY = -vY;
                        }
                        else if (vY < 0)
                        {
                            //Debug.WriteLine("Interakcja dół równo");
                            posX += -vX / 2;
                            posY = closestBrick.BrickPosY + closestBrick.BrickHeight;
                            vY = -vY;
                        }
                    }
                }
                else if (brickBeta == ballBeta || brickAlpha == ballAlpha)
                {
                    if ((posX + (width / 2)) < (closestBrick.BrickPosX + (closestBrick.BrickWidth / 2)) && (posY + (height / 2)) < (closestBrick.BrickPosY + (closestBrick.BrickHeight / 2)))
                    {
                        if (vX > 0 && vY > 0)
                        {
                            //Debug.WriteLine("Interakcja lewy górny róg");
                            posX = closestBrick.BrickPosX - width;
                            posY = closestBrick.BrickPosY - height;
                            vX = -vX;
                            vY = -vY;
                        }
                        else
                        {
                            if (vX > 0)
                            {
                                //Debug.WriteLine("Interakcja lewy górny róg - lewo");
                                posX = closestBrick.BrickPosX - width;
                                posY += -vY / 2;
                                vX = -vX;
                            }
                            else if (vY > 0)
                            {
                                //Debug.WriteLine("Interakcja lewy górny róg - góra");
                                posX += -vX / 2;
                                posY = closestBrick.BrickPosY - height;
                                vY = -vY;
                            }
                        }
                    }
                    else if ((posX + (width / 2)) < (closestBrick.BrickPosX + (closestBrick.BrickWidth / 2)) && (posY + (height / 2)) > (closestBrick.BrickPosY + (closestBrick.BrickHeight / 2)))
                    {
                        if (vX > 0 && vY < 0)
                        {
                            //Debug.WriteLine("Interakcja lewy dolny róg");
                            posX = closestBrick.BrickPosX - width;
                            posY = closestBrick.BrickPosY + closestBrick.BrickHeight;
                            vX = -vX;
                            vY = -vY;
                        }
                        else
                        {
                            if (vX > 0)
                            {
                                //Debug.WriteLine("Interakcja lewy dolny róg - lewo");
                                posX = closestBrick.BrickPosX - width;
                                posY += -vY / 2;
                                vX = -vX;
                            }
                            else if (vY < 0)
                            {
                                //Debug.WriteLine("Interakcja lewy dolny róg - dół");
                                posX += -vX / 2;
                                posY = closestBrick.BrickPosY + closestBrick.BrickHeight;
                                vY = -vY;
                            }
                        }
                    }
                    else if ((posX + (width / 2)) > (closestBrick.BrickPosX + (closestBrick.BrickWidth / 2)) && (posY + (height / 2)) < (closestBrick.BrickPosY + (closestBrick.BrickHeight / 2)))
                    {
                        if (vX < 0 && vY > 0)
                        {
                            //Debug.WriteLine("Interakcja prawy górny róg");
                            posX = closestBrick.BrickPosX + closestBrick.BrickWidth;
                            posY = closestBrick.BrickPosY - height;
                            vX = -vX;
                            vY = -vY;
                        }
                        else
                        {
                            if (vX < 0)
                            {
                                //Debug.WriteLine("Interakcja prawy górny róg - prawo");
                                posX = closestBrick.BrickPosX + closestBrick.BrickWidth;
                                posY += -vY / 2;
                                vX = -vX;
                            }
                            else if (vY > 0)
                            {
                                //Debug.WriteLine("Interakcja prawy górny róg - góra");
                                posX += -vX / 2;
                                posY = closestBrick.BrickPosY - height;
                                vY = -vY;
                            }
                        }
                    }
                    else if ((posX + (width / 2)) > (closestBrick.BrickPosX + (closestBrick.BrickWidth / 2)) && (posY + (height / 2)) > (closestBrick.BrickPosY + (closestBrick.BrickHeight / 2)))
                    {
                        if (vX > 0 && vY < 0)
                        {
                            //Debug.WriteLine("Interakcja prawy dolny róg");
                            posX = closestBrick.BrickPosX + closestBrick.BrickWidth;
                            posY = closestBrick.BrickPosY + closestBrick.BrickHeight;
                            vX = -vX;
                            vY = -vY;
                        }
                        else
                        {
                            if (vX < 0)
                            {
                                //Debug.WriteLine("Interakcja prawy dolny róg - prawo");
                                posX = closestBrick.BrickPosX + closestBrick.BrickWidth;
                                posY += -vY / 2;
                                vX = -vX;
                            }
                            else if (vY < 0)
                            {
                                //Debug.WriteLine("Interakcja prawy dolny róg - dół");
                                posX += -vX / 2;
                                posY = closestBrick.BrickPosY + closestBrick.BrickHeight;
                                vY = -vY;
                            }
                        }
                    }
                }
                else if (ballBeta > brickBeta && vX > 0)
                {
                    if ((posX + (width / 2)) < (closestBrick.BrickPosX + (closestBrick.BrickWidth / 2)))
                    {
                        //Debug.WriteLine("Interakcja lewo");
                        posX = closestBrick.BrickPosX - width;
                        posY += -vY / 2;
                        vX = -vX;
                    }
                    else
                    {
                        //Debug.WriteLine("Interakcja lewo - błąd narożnika");
                        posX += -vX / 2;
                        posY += -vY / 2;
                        vY = -vY;
                    }
                }
                else if (ballBeta > brickBeta && vX < 0)
                { 
                    if ((posX + (width / 2)) > (closestBrick.BrickPosX + (closestBrick.BrickWidth / 2)))
                    {
                        //Debug.WriteLine("Interakcja prawo");
                        posX = closestBrick.BrickPosX + closestBrick.BrickWidth;
                        posY += -vY / 2;
                        vX = -vX;
                    }
                    else
                    {
                        //Debug.WriteLine("Interakcja prawo - błąd narożnika");
                        posX += -vX / 2;
                        posY += -vY / 2;
                        vY = -vY;
                    }
                }
                else if (ballAlpha > brickAlpha && vY > 0)
                {
                    if ((posY + (height / 2)) < (closestBrick.BrickPosY + (closestBrick.BrickHeight / 2)))
                    {
                        //Debug.WriteLine("Interakcja góra");
                        posX += -vX / 2;
                        posY = closestBrick.BrickPosY - height;
                        vY = -vY;
                    }
                    else
                    {
                        //Debug.WriteLine("Interakcja góra - błąd narożnika");
                        posX += -vX / 2;
                        posY += -vY / 2;
                        vX = -vX;
                    }
                }
                else if (ballAlpha > brickAlpha && vY < 0)
                {    
                    if ((posY + (height / 2)) > (closestBrick.BrickPosY + (closestBrick.BrickHeight / 2)))
                    {
                        //Debug.WriteLine("Interakcja dół");
                        posX += -vX / 2;
                        posY = closestBrick.BrickPosY + closestBrick.BrickHeight;
                        vY = -vY;
                    }
                    else
                    {
                        //Debug.WriteLine("Interakcja dół - błąd narożnika");
                        posX += -vX / 2;
                        posY += -vY / 2;
                        vX = -vX;
                    }
                }
                //else
                //{
                //    Debug.WriteLine("Błąd");
                //}

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