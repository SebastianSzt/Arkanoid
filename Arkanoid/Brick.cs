﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanoid
{
    [Serializable]
    internal class Brick : GameObject
    {
        private int row;
        private int column;

        public int BrickRow { get { return row; } }
        public int BrickColumn { get { return column; } }
        public int BrickPosX { get { return posX; } }
        public int BrickPosY { get { return posY; } }
        public int BrickWidth { get { return width; } }
        public int BrickHeight { get { return height; } }

        public Brick(int row, int column, int posX, int posY, int width, int height, Color color) : base(posX, posY, width, height, color) 
        { 
            this.row = row;
            this.column = column;
        }

        public override void Draw(PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(color);
            e.Graphics.FillRectangle(brush, new Rectangle(posX, posY, width, height));
            brush.Dispose();
        }

        public override void ChangeSize(float xRatio, float yRatio)
        {
            posX = (int)Math.Round(Math.Round(posX / this.xRatio) * xRatio);
            posY = (int)Math.Round(Math.Round(posY / this.yRatio) * yRatio);
            width = (int)Math.Round(Math.Round(width / this.xRatio) * xRatio);
            height = (int)Math.Round(Math.Round(height / this.yRatio) * yRatio);

            this.xRatio = xRatio;
            this.yRatio = yRatio;
        }
    }
}