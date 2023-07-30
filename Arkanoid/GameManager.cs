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

        public int paddleVelocity { get; }
        private int points;
        private int lifes;
        private int currentLifes;
        private int level;
        private int currentLevel;
        private int ballAccelerationInterval;

        public bool ballStart;
        private bool gameOver;

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
            gameBall = new Ball(panelWidth / 2 - 5, (int)(panelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 15), -7, panelWidth, panelHeight);
            gamePaddle = new Paddle(panelWidth / 2 - 30, (int)(panelHeight * 0.85) - 4, 60, 8, Color.White, paddleVelocity, panelWidth);
            this.lifes = lifes;
            this.currentLifes = lifes;
            this.points = 0;
            this.level = level;
            this.currentLevel = 1;
            this.ballAccelerationInterval = ballAccelerationInterval;
            this.gameOver = false;
            this.ballStart = true;
        }

        public void CheckGameOver()
        {
            if (!gameBall.CheckNextMove())
            {
                currentLifes--;
                if (currentLifes > 0)
                {
                    int randomDirection = random.Next(2) == 0 ? -1 : 1;
                    gameBall = new Ball(panelWidth / 2 - 5, (int)(panelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 15), -7, panelWidth, panelHeight);
                    gamePaddle = new Paddle(panelWidth / 2 - 30, (int)(panelHeight * 0.85) - 4, 60, 8, Color.White, paddleVelocity, panelWidth);
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
                    if (gamePaddle.posXValue - paddleVelocity >= 0)
                        gameBall.MoveX(-paddleVelocity);
                    else
                        gameBall.MoveX(-gamePaddle.posXValue);
                }
            }
            else if (e == Keys.D)
            {
                gamePaddle.vX = paddleVelocity;
                if (ballStart)
                {
                    if (gamePaddle.posXValue + gamePaddle.widthValue + gamePaddle.vX <= panelWidth)
                        gameBall.MoveX(paddleVelocity);
                    else
                        gameBall.MoveX(panelWidth - (gamePaddle.posXValue + gamePaddle.widthValue));
                }
            }
            gamePaddle.Move();
        }

        public void ResetPaddleVX()
        {
            gamePaddle.vX = 0;
        }
    }
}
