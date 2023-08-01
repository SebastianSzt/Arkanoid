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
    internal class GameManager
    {
        private int panelWidth;
        private int panelHeight;

        private Ball gameBall;
        private Paddle gamePaddle;

        private int paddleVelocity;
        private int points;
        private int lifes;
        private int currentLifes;
        private int level;
        private int currentLevel;
        private int ballAccelerationInterval;

        public bool ballStart;
        private bool gameOver;

        private float xRatio;
        private float yRatio;

        private Random random = new Random();

        public int pointsValue { get { return points; } }
        public int lifesValue { get { return lifes; } }
        public int currentLifesValue { get { return currentLifes; } }
        public int levelValue { get { return level; } }
        public int currentLevelValue { get { return currentLevel; } }
        public int ballAccelerationIntervalValue { get { return ballAccelerationInterval; } }
        public bool gameOverStatus { get { return gameOver; } }

        public GameManager(int panelWidth, int panelHeight, int lifes, int level, int ballAccelerationInterval)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            this.paddleVelocity = 10;
            int randomDirection = random.Next(2) == 0 ? -1 : 1;
            gameBall = new Ball(panelWidth / 2 - 5, (int)(panelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 15), -10, panelWidth, panelHeight);
            gamePaddle = new Paddle(panelWidth / 2 - 30, (int)(panelHeight * 0.85) - 4, 60, 8, Color.White, 0, panelWidth);
            this.lifes = lifes;
            this.currentLifes = lifes;
            this.points = 0;
            this.level = level;
            this.currentLevel = 1;
            this.ballAccelerationInterval = ballAccelerationInterval;
            this.gameOver = false;
            this.ballStart = true;
            xRatio = 1;
            yRatio = 1;
        }

        public void CheckGameOver()
        {
            if (!gameBall.CheckNextMove())
            {
                currentLifes--;
                if (currentLifes > 0)
                {
                    int panelWidth = (int)Math.Round(this.panelWidth / xRatio);
                    int panelHeight = (int)Math.Round(this.panelHeight / yRatio);
                    int randomDirection = random.Next(2) == 0 ? -1 : 1;
                    gameBall = new Ball(panelWidth / 2 - 5, (int)(panelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 15), -10, panelWidth, panelHeight);
                    gamePaddle = new Paddle(panelWidth / 2 - 30, (int)(panelHeight * 0.85) - 4, 60, 8, Color.White, 0, panelWidth);
                    ballStart = true;
                }
                else
                    gameOver = true;
            }
        }

        public void DrawGameObjects(PaintEventArgs e)
        {
            gameBall.Draw(e);
            gamePaddle.Draw(e);
        }

        public void MakeTick()
        {
            gameBall.Move();
            CheckGameOver();
        }

        public void MovePaddle(Keys e)
        {
            if (e == Keys.A)
            {
                gamePaddle.vX = -paddleVelocity;
                if (ballStart)
                {
                    if (gamePaddle.posXValue - paddleVelocity < 0)
                        gameBall.MoveX(-gamePaddle.posXValue);
                    else
                        gameBall.MoveX(-paddleVelocity);
                }
            }
            else if (e == Keys.D)
            {
                gamePaddle.vX = paddleVelocity;
                if (ballStart)
                {
                    if (gamePaddle.posXValue + gamePaddle.widthValue + gamePaddle.vX > panelWidth)
                        gameBall.MoveX(panelWidth - (gamePaddle.posXValue + gamePaddle.widthValue));
                    else 
                        gameBall.MoveX(paddleVelocity);
                }
            }
            gamePaddle.Move();
        }

        public void ResetPaddleVX()
        {
            gamePaddle.vX = 0;
        }

        public void AccelerateBall()
        {
            gameBall.AccelerateBall -= 1;
        }

        public void ChangeObjectsSize(float xRatio, float yRatio)
        {
            panelWidth = (int)Math.Round(Math.Round(panelWidth / this.xRatio) * xRatio);
            panelHeight = (int)Math.Round(Math.Round(panelHeight / this.yRatio) * yRatio);

            paddleVelocity = (int)(10 * xRatio);

            gameBall.ChangeSize(xRatio, yRatio);
            gamePaddle.ChangeSize(xRatio, yRatio);

            this.xRatio = xRatio;
            this.yRatio = yRatio;
        }
    }
}
