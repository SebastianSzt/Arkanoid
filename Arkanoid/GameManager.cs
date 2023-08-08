using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using System.Security.Cryptography;

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
        private int bonusPoints;
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

        private List<Ball> gameBallList = new List<Ball>();
        private List<Bonus> bonusBubbleList = new List<Bonus>();
        private List<Ball> specialBallList = new List<Ball>();

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

        public GameManager(int panelWidth, int panelHeight, int lifes, int level, int ballAccelerationInterval, float xRatio, float yRatio)
        {
            originalPanelWidth = (int)Math.Round(panelWidth / xRatio);
            originalPanelHeight = (int)Math.Round(panelHeight / yRatio);

            points = 0;
            this.lifes = lifes;
            this.levels = level;
            bonusPoints = 0;
            currentLifes = lifes;
            currentLevel = 1;

            paddleVelocity = (int)Math.Round(14 * xRatio);
            this.ballAccelerationInterval = ballAccelerationInterval;

            levelStart = true;
            gameOver = false;
            gameWin = false;

            this.xRatio = xRatio;
            this.yRatio = yRatio;

            GameGrid = new Grid(originalPanelWidth, originalPanelHeight, 14, 10);
            GamePaddle = new Paddle(originalPanelWidth, originalPanelWidth / 2 - 30, (int)(originalPanelHeight * 0.85) - 4, 60, 8, Color.White);

            Random random = new Random();
            int randomDirection = random.Next(2) == 0 ? -1 : 1;
            Ball GameBall = new Ball(originalPanelWidth, originalPanelHeight, originalPanelWidth / 2 - 5, (int)(originalPanelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 4), -2, GameGrid, GamePaddle);
            gameBallList.Add(GameBall);
        }

        public void DrawGameObjects(PaintEventArgs e)
        {
            GameGrid.DrawBricks(e);
            foreach (Bonus bubble in bonusBubbleList)
            {
                bubble.Draw(e);
            }
            foreach (Ball specialBall in specialBallList)
            {
                specialBall.Draw(e);
            }
            GamePaddle.Draw(e);
            foreach (Ball ball in gameBallList)
            {
                ball.Draw(e);
            }
        }

        public void MakeTick()
        {
            foreach (Ball ball in gameBallList)
            {
                ball.Move();
                int newPoints = ball.CheckColisionWithBricks();
                points += newPoints;
                bonusPoints += newPoints;
                if (bonusPoints >= 500)
                {
                    bonusPoints -= 500;
                    GenerateBonusBubble();
                }
                ball.CheckColisionWithPaddle();
                ball.CheckColisionWithWalls();
            }
    
            for (int i = bonusBubbleList.Count - 1; i >= 0; i--)
            {
                Bonus bubble = bonusBubbleList[i];
                bubble.Move();

                if (bubble.CheckCollisionWithObjects())
                {
                    bonusBubbleList.RemoveAt(i);
                    GenerateBonus();
                }

                if (bubble.CheckBubbleEnd())
                    bonusBubbleList.RemoveAt(i);
            }
            for (int i = specialBallList.Count - 1; i >= 0; i--)
            {
                Ball specialBall = specialBallList[i];
                specialBall.Move();
                specialBall.CheckColisionWithBricksNoBouncing();

                if (specialBall.CheckBallOnUpperWall())
                    specialBallList.RemoveAt(i);
            }

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

                    GamePaddle = new Paddle(originalPanelWidth, originalPanelWidth / 2 - 30, (int)(originalPanelHeight * 0.85) - 4, 60, 8, Color.White);

                    gameBallList.Clear();
                    Random random = new Random();
                    int randomDirection = random.Next(2) == 0 ? -1 : 1;
                    Ball GameBall = new Ball(originalPanelWidth, originalPanelHeight, originalPanelWidth / 2 - 5, (int)(originalPanelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 4), -2, GameGrid, GamePaddle);
                    gameBallList.Add(GameBall);
                    bonusBubbleList.Clear();
                    specialBallList.Clear();
                }
                else
                    gameWin = true;
            }
        }

        private void CheckGameOver()
        {
            for (int i = gameBallList.Count - 1; i >= 0; i--)
            {
                Ball ball = gameBallList[i];
                if (ball.CheckRoundFail())
                    gameBallList.RemoveAt(i);
            }
            if (gameBallList.Count == 0)
            {
                currentLifes--;
                if (currentLifes > 0)
                {
                    levelStart = true;

                    GamePaddle = new Paddle(originalPanelWidth, originalPanelWidth / 2 - 30, (int)(originalPanelHeight * 0.85) - 4, 60, 8, Color.White);

                    gameBallList.Clear();
                    Random random = new Random();
                    int randomDirection = random.Next(2) == 0 ? -1 : 1;
                    Ball GameBall = new Ball(originalPanelWidth, originalPanelHeight, originalPanelWidth / 2 - 5, (int)(originalPanelHeight * 0.85) - 14, 10, 10, Color.White, randomDirection * random.Next(2, 4), -2, GameGrid, GamePaddle);
                    gameBallList.Add(GameBall);
                    bonusBubbleList.Clear();
                    specialBallList.Clear();
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
                gameBallList[0].PositionWithPaddle();
        }

        private void GenerateBonusBubble()
        {
            Random random = new Random();
            if (random.Next(0, 2) == 0)
            {
                Bonus bonusBubble = new Bonus(originalPanelWidth, originalPanelHeight, random.Next(0, originalPanelWidth - 19), 0, 20, 20, Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)), GamePaddle, gameBallList, specialBallList);
                bonusBubble.ChangeSize(xRatio, yRatio);
                bonusBubbleList.Add(bonusBubble);
            }
        }

        private void GenerateBonus()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 6);
            if (randomNumber == 0)
            {
                currentLifes++;
            }
            else if (randomNumber == 1)
            {
                points += 500;
            }
            else if (randomNumber == 2)
            {
                Ball specialBall = new Ball(0, 0, 0, 0, 10, 10, Color.FromArgb(102, 255, 153), 0, -5, GameGrid, GamePaddle);
                specialBall.ChangeSize(xRatio, yRatio);
                specialBall.PositionWithPaddle();
                specialBallList.Add(specialBall);
            }
            else if (randomNumber == 3)
            {
                GamePaddle.MakeBigger();
            }
            else if (randomNumber == 4)
            {
                foreach (Ball ball in gameBallList)
                {
                    ball.MakeBigger();
                }
            }
            else if (randomNumber == 5)
            {
                int ballNumber = random.Next(0, gameBallList.Count);
                int randomDirectionVX = random.Next(2) == 0 ? -1 : 1;
                int randomDirectionVY = random.Next(2) == 0 ? -1 : 1;
                Ball nextBall = new Ball((int)Math.Round(originalPanelWidth * xRatio), (int)Math.Round(originalPanelHeight * yRatio), gameBallList[ballNumber].BallPosX, gameBallList[ballNumber].BallPosY, gameBallList[ballNumber].BallWidth, gameBallList[ballNumber].BallHeight, Color.White, randomDirectionVX * random.Next((int)Math.Round(2 * xRatio), (int)Math.Round(4 * xRatio)), randomDirectionVY * gameBallList[ballNumber].BallVY, GameGrid, GamePaddle);
                nextBall.ChangeRatio(xRatio, yRatio);
                gameBallList.Add(nextBall);
                randomDirectionVX = random.Next(2) == 0 ? -1 : 1;
                randomDirectionVY = random.Next(2) == 0 ? -1 : 1;
                nextBall = new Ball((int)Math.Round(originalPanelWidth * xRatio), (int)Math.Round(originalPanelHeight * yRatio), gameBallList[ballNumber].BallPosX, gameBallList[ballNumber].BallPosY, gameBallList[ballNumber].BallWidth, gameBallList[ballNumber].BallHeight, Color.White, randomDirectionVX * random.Next((int)Math.Round(2 * xRatio), (int)Math.Round(4 * xRatio)), randomDirectionVY * gameBallList[ballNumber].BallVY, GameGrid, GamePaddle);
                nextBall.ChangeRatio(xRatio, yRatio);
                gameBallList.Add(nextBall);
            }
        }

        public void AccelerateBallVY()
        {
            foreach (Ball ball in gameBallList)
            {
                ball.AccelerateVY = (int)Math.Round(1 * yRatio);
            }
        }

        public void ChangeObjectsSize(float xRatio, float yRatio)
        {
            paddleVelocity = (int)Math.Round(14 * xRatio);

            GameGrid.ChangeBricsSize(xRatio, yRatio);
            GamePaddle.ChangeSize(xRatio, yRatio);

            foreach (Ball ball in gameBallList)
            {
                ball.ChangeSize(xRatio, yRatio);
            }
            foreach (Bonus bubble in bonusBubbleList)
            {
                bubble.ChangeSize(xRatio, yRatio);
            }
            foreach (Ball specialBall in specialBallList)
            {
                specialBall.ChangeSize(xRatio, yRatio);
            }

            this.xRatio = xRatio;
            this.yRatio = yRatio;
        }
    }
}