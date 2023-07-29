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
        private int lifes;
        private int points;
        private bool gameOver;
        private Random random = new Random();

        public int lifesValue { get { return lifes; } }
        public int pointsValue { get { return points; } }
        public bool gameOverStatus { get { return gameOver; } }

        public GameManager(int panelWidth, int panelHeight, int lifes)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            int randomDirection = random.Next(2) == 0 ? -1 : 1;
            gameBall = new Ball(panelWidth / 2, panelHeight / 2, 10, 10, Color.White, randomDirection * 5, -5, panelWidth, panelHeight);
            this.lifes = lifes;
            this.points = 0;
            this.gameOver = false;
        }

        public void CheckGameOver()
        {
            if (!gameBall.CheckNextMove())
            {
                lifes--;
                if (lifes != 0)
                {
                    int randomDirection = random.Next(2) == 0 ? -1 : 1;
                    gameBall = new Ball(panelWidth / 2, panelHeight / 2, 10, 10, Color.White, randomDirection * 5, -5, panelWidth, panelHeight);
                }
                else
                    gameOver = true;

            }
        }

        public void Draw(PaintEventArgs e)
        {
            gameBall.Draw(e);
        }

        public void MakeTick()
        {
            gameBall.Move();
            CheckGameOver();
        }
    }
}
