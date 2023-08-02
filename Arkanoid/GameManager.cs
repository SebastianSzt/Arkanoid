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

        private Paddle GamePaddle;
        private Ball GameBall;
        
        private int points;
        private int lifes;
        private int level;
        private int currentLifes;
        private int currentLevel;

        private bool roundStart;
        private bool gameOver;

        private int paddleVelocity;
        private int ballAccelerationInterval;

        private float xRatio;
        private float yRatio;

        private Random random = new Random();

        public int PointsValue { get { return points; } }
        public int LifesValue { get { return lifes; } }
        public int LevelValue { get { return level; } }
        public int CurrentLifesValue { get { return currentLifes; } }
        public int CurrentLevelValue { get { return currentLevel; } }

        public bool GameOverStatus { get { return gameOver; } }
        public bool RoundStartStatus { get { return roundStart; } set { roundStart = value; } }

        public int ResetPaddleVX { get { return GamePaddle.PaddleVX; } set { GamePaddle.PaddleVX = 0; } }
        public int BallAccelerationIntervalValue { get { return ballAccelerationInterval; } }
        public int AccelerateBallVY { get { return GameBall.AccelerateVY; } set { GameBall.AccelerateVY = (int)Math.Round(value * yRatio); } }

        public GameManager(int panelWidth, int panelHeight, int lifes, int level, int ballAccelerationInterval)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;

            int randomDirection = random.Next(2) == 0 ? -1 : 1;
            GamePaddle = new Paddle(panelWidth / 2 - 30, (int)(panelHeight * 0.85) - 4, 60, 8, Color.White, panelWidth);
            GameBall = new Ball(panelWidth / 2 - 5, (int)(panelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(4, 14), -12, panelWidth, panelHeight, GamePaddle);

            points = 0;
            this.lifes = lifes;
            this.level = level;
            currentLifes = lifes;
            currentLevel = 1;

            roundStart = true;
            gameOver = false;

            paddleVelocity = 12;
            this.ballAccelerationInterval = ballAccelerationInterval;

            xRatio = 1;
            yRatio = 1;
        }

        public void DrawGameObjects(PaintEventArgs e)
        {
            GameBall.Draw(e);
            GamePaddle.Draw(e);
        }

        public void MakeTick()
        {
            GameBall.Move();
            GameBall.CheckColisionWithPaddle();
            GameBall.CheckColisionWithWalls();
            CheckGameOver();
        }

        public void CheckGameOver()
        {
            if (GameBall.CheckRoundFail())
            {
                currentLifes--;
                if (currentLifes > 0)
                {
                    int panelWidth = (int)Math.Round(this.panelWidth / xRatio);
                    int panelHeight = (int)Math.Round(this.panelHeight / yRatio);

                    int randomDirection = random.Next(2) == 0 ? -1 : 1;
                    GamePaddle = new Paddle(panelWidth / 2 - 30, (int)(panelHeight * 0.85) - 4, 60, 8, Color.White, panelWidth);
                    GameBall = new Ball(panelWidth / 2 - 5, (int)(panelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(4, 15), -10, panelWidth, panelHeight, GamePaddle);

                    roundStart = true;
                }
                else
                    gameOver = true;
            }
        }
        
        public void MovePaddle(Keys e)
        {
            GamePaddle.SetDirection(e, paddleVelocity);
            GamePaddle.Move();
            if (roundStart)
                GameBall.PositionWithPaddle();
        }

        public void ChangeObjectsSize(float xRatio, float yRatio)
        {
            panelWidth = (int)Math.Round(Math.Round(panelWidth / this.xRatio) * xRatio);
            panelHeight = (int)Math.Round(Math.Round(panelHeight / this.yRatio) * yRatio);

            paddleVelocity = (int)Math.Round(10 * xRatio);

            GameBall.ChangeSize(xRatio, yRatio);
            GamePaddle.ChangeSize(xRatio, yRatio);

            this.xRatio = xRatio;
            this.yRatio = yRatio;
        }
    }
}
