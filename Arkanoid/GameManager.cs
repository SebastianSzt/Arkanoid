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
    [Serializable]
    internal class GameManager
    {
        private int originalPanelWidth;
        private int originalPanelHeight;

        private int points;
        private int lifes;
        private int levels;
        private int currentLifes;
        private int currentLevel;

        private int paddleVelocity;
        private int ballAccelerationInterval;

        private bool levelStart;
        private bool gameOver;
        private bool gameWin;

        private float xRatio;
        private float yRatio;

        private Grid GameGrid;
        private Paddle GamePaddle;
        private Ball GameBall;

        public int PointsValue { get { return points; } }
        public int LifesValue { get { return lifes; } }
        public int LevelsValue { get { return levels; } }
        public int CurrentLifesValue { get { return currentLifes; } }
        public int CurrentLevelValue { get { return currentLevel; } }

        public bool RoundStartStatus { get { return levelStart; } set { levelStart = value; } }
        public bool GameOverStatus { get { return gameOver; } }
        public bool GameWinStatus { get { return gameWin; } }

        public int ResetPaddleVX { get { return GamePaddle.PaddleVX; } set { GamePaddle.PaddleVX = 0; } }
        public int BallAccelerationIntervalValue { get { return ballAccelerationInterval; } }
        public int AccelerateBallVY { get { return GameBall.AccelerateVY; } set { GameBall.AccelerateVY = (int)Math.Round(value * yRatio); } }

        public GameManager(int panelWidth, int panelHeight, int lifes, int level, int ballAccelerationInterval, float xRatio, float yRatio)
        {
            originalPanelWidth = (int)Math.Round(panelWidth / xRatio);
            originalPanelHeight = (int)Math.Round(panelHeight / yRatio);

            points = 0;
            this.lifes = lifes;
            this.levels = level;
            currentLifes = lifes;
            currentLevel = 1;

            paddleVelocity = (int)Math.Round(14 * xRatio);
            this.ballAccelerationInterval = ballAccelerationInterval;

            levelStart = true;
            gameOver = false;
            gameWin = false;

            this.xRatio = xRatio;
            this.yRatio = yRatio;

            Random random = new Random();

            int randomDirection = random.Next(2) == 0 ? -1 : 1;
            GameGrid = new Grid(originalPanelWidth, originalPanelHeight, 14, 10);
            GamePaddle = new Paddle(originalPanelWidth, originalPanelWidth / 2 - 30, (int)(originalPanelHeight * 0.85) - 4, 60, 8, Color.White);
            GameBall = new Ball(originalPanelWidth, originalPanelHeight, originalPanelWidth / 2 - 5, (int)(originalPanelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 4), -2, GameGrid, GamePaddle);
        }

        public void DrawGameObjects(PaintEventArgs e)
        {
            GameGrid.DrawBricks(e);
            GamePaddle.Draw(e);
            GameBall.Draw(e);
        }

        public void MakeTick()
        {
            GameBall.Move();
            points += GameBall.CheckColisionWithBricks();
            GameBall.CheckColisionWithPaddle();
            GameBall.CheckColisionWithWalls();
            CheckGameWin();
            CheckGameOver();
        }

        private void CheckGameWin()
        {
            if (GameGrid.CheckLevelEnd())
            {
                if (currentLevel < levels)
                {
                    points += 1000;
                    if (currentLifes < lifes)
                        currentLifes++;
                    currentLevel++;
                    levelStart = true;

                    GameGrid.CreateNextLevel();

                    Random random = new Random();
                    int randomDirection = random.Next(2) == 0 ? -1 : 1;
                    GamePaddle = new Paddle(originalPanelWidth, originalPanelWidth / 2 - 30, (int)(originalPanelHeight * 0.85) - 4, 60, 8, Color.White);
                    GameBall = new Ball(originalPanelWidth, originalPanelHeight, originalPanelWidth / 2 - 5, (int)(originalPanelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 4), -2, GameGrid, GamePaddle);
                }
                else
                    gameWin = true;
            }
        }

        private void CheckGameOver()
        {
            if (GameBall.CheckRoundFail())
            {
                currentLifes--;
                if (currentLifes > 0)
                {
                    levelStart = true;

                    Random random = new Random();
                    int randomDirection = random.Next(2) == 0 ? -1 : 1;
                    GamePaddle = new Paddle(originalPanelWidth, originalPanelWidth / 2 - 30, (int)(originalPanelHeight * 0.85) - 4, 60, 8, Color.White);
                    GameBall = new Ball(originalPanelWidth, originalPanelHeight, originalPanelWidth / 2 - 5, (int)(originalPanelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 4), -2, GameGrid, GamePaddle);
                }
                else
                    gameOver = true;
            }
        }

        public void MovePaddle(Keys e)
        {
            GamePaddle.SetDirection(e, paddleVelocity);
            GamePaddle.Move();
            if (levelStart)
                GameBall.PositionWithPaddle();
        }

        public void ChangeObjectsSize(float xRatio, float yRatio)
        {
            paddleVelocity = (int)Math.Round(14 * xRatio);

            this.xRatio = xRatio;
            this.yRatio = yRatio;

            GameGrid.ChangeBricsSize(xRatio, yRatio);
            GameBall.ChangeSize(xRatio, yRatio);
            GamePaddle.ChangeSize(xRatio, yRatio);
        }
    }
}