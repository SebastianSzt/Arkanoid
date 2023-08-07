using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Arkanoid
{
    [Serializable]
    internal class Bonus : GameObject
    {
        private int panelWidth;
        private int panelHeight;

        private int vX;

        readonly private Paddle GamePaddle;

        readonly private List<Ball> gameballList = new List<Ball>();
        readonly private List<Ball> specialBallList = new List<Ball>();

        public Bonus(int panelWidth, int panelHeight, int posX, int posY, int width, int height, Color color, Paddle GamePaddle, List<Ball> gameBallList, List<Ball> specialBallList) : base(posX, posY, width, height, color)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;

            Random random = new Random();
            vX = random.Next(-1, 2);

            this.GamePaddle = GamePaddle;

            this.gameballList = gameBallList;
            this.specialBallList = specialBallList;
        }

        public override void Draw(PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            e.Graphics.FillEllipse(brush, new RectangleF(posX, posY, width, height));
            brush.Color = color;
            e.Graphics.FillEllipse(brush, new RectangleF(posX + 2, posY + 2, width - 4, height - 4));
            brush.Dispose();
        }

        public override void Move()
        {
            Random random = new Random();

            if (random.Next(0, 30) == 0)
            {
                if (vX >= 0)
                    vX = -random.Next(0, 2);
                else
                    vX = random.Next(0, 2);
            }

            posX += vX;
            posY += 1;

            if (posX < 0)
                posX = 0;
            else if (posX + width > panelWidth)
                posX = panelWidth - width;
        }

        public bool CheckBubbleEnd()
        {
            if (posY >= panelHeight)
                return true;
            return false;
        }

        public bool CheckCollisionWithObjects()
        {
            RectangleF bubbleRect = new RectangleF(posX - 1, posY - 1, width + 2, height + 2);
            Rectangle paddleRect = new Rectangle(GamePaddle.PaddlePosX - 1, GamePaddle.PaddlePosY - 1, GamePaddle.PaddleWidth + 2, GamePaddle.PaddleHeight + 2);

            if (bubbleRect.IntersectsWith(paddleRect))
                return true;

            foreach (Ball ball in gameballList)
            {
                RectangleF ballRect = new RectangleF(ball.BallPosX - 1, ball.BallPosY - 1, ball.BallWidth + 2, ball.BallHeight + 2);
                if (bubbleRect.IntersectsWith(ballRect))
                    return true;
            }

            foreach (Ball specialBall in specialBallList)
            {
                RectangleF specialBallRect = new RectangleF(specialBall.BallPosX - 1, specialBall.BallPosY - 1, specialBall.BallWidth + 2, specialBall.BallHeight + 2);
                if (bubbleRect.IntersectsWith(specialBallRect))
                    return true;
            }

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

            this.xRatio = xRatio;
            this.yRatio = yRatio;
        }
    }
}
