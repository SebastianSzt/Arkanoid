using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Arkanoid
{
    internal class Grid
    {
        private int panelWidth;
        private int panelHeight;

        private Brick[,] bricksGrid;

        private int currentLevel;
        private int brickWidth;
        private int brickHeight;
        private int margin;

        private float xRatio;
        private float yRatio;

        public int Rows { get { return bricksGrid.GetLength(0); } }
        public int Columns { get { return bricksGrid.GetLength(1); } }

        public Grid(int panelWidth, int panelHeight, int rows, int columns)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;

            bricksGrid = new Brick[rows, columns];

            currentLevel = 1;
            brickWidth = (panelWidth - Columns + 1) / Columns;
            brickHeight = ((int)(panelHeight * 0.6) - Rows + 1) / Rows;
            margin = (panelWidth - brickWidth * Columns - Columns - 1) / 2;

            xRatio = 1;
            yRatio = 1;

            CreateMap();
        }

        public void DrawBricks(PaintEventArgs e)
        {
            foreach (Brick brick in bricksGrid)
            {
                if (brick != null)
                    brick.Draw(e);
            }
        }

        public void CreateMap()
        {
            string levelFileName = "level" + currentLevel + ".txt";

            if (File.Exists(levelFileName))
            {
                string[] lines = File.ReadAllLines(levelFileName);

                for (int row = 0; row < Rows; row++)
                {
                    if (row < lines.Length)
                    {
                        string[] words = lines[row].Split('\t');

                        for (int col = 0; col < Columns; col++)
                        {
                            Color color;
                            if (col < words.Length && IsValidHexColor(words[col], out color))
                            {
                                bricksGrid[row, col] = new Brick(margin + (brickWidth + 1) * col, margin + (brickHeight + 1) * row, brickWidth, brickHeight, color);
                            }
                        }
                    }
                }
            }
            else
            {
                GenerateLevel();
            }
        }

        private bool IsValidHexColor(string colorString, out Color color)
        {
            if (colorString.StartsWith("#") && colorString.Length == 7)
            {
                try
                {
                    int r = int.Parse(colorString.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                    int g = int.Parse(colorString.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                    int b = int.Parse(colorString.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                    color = Color.FromArgb(r, g, b);
                    return true;
                }
                catch (Exception)
                {
                    // Invalid hex color format
                }
            }

            color = Color.Transparent;
            return false;
        }

        public void GenerateLevel()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 6);

            switch (randomNumber)
            {
                case 1:
                    for (int row = 0; row < Rows; row++)
                    {
                        for (int col = 0; col < Columns; col++)
                        {
                            if (random.NextDouble() < 0.75)
                            {
                                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                                bricksGrid[row, col] = new Brick(margin + (brickWidth + 1) * col, margin + (brickHeight + 1) * row, brickWidth, brickHeight, randomColor);
                            }
                        }
                    }
                    break;
                case 2:
                    for (int row = 0; row < Rows; row++)
                    {
                        if (random.NextDouble() < 0.7)
                        {
                            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                            for (int col = 0; col < Columns; col++)
                            {
                                bricksGrid[row, col] = new Brick(margin + (brickWidth + 1) * col, margin + (brickHeight + 1) * row, brickWidth, brickHeight, randomColor);

                                int newR = Math.Max(0, Math.Min(255, randomColor.R + random.Next(-20, 21)));
                                int newG = Math.Max(0, Math.Min(255, randomColor.G + random.Next(-20, 21)));
                                int newB = Math.Max(0, Math.Min(255, randomColor.B + random.Next(-20, 21)));

                                randomColor = Color.FromArgb(newR, newG, newB);
                            }
                        }
                    }
                    break;
                case 3:
                    for (int col = 0; col < Columns; col++)
                    {
                        if (random.NextDouble() < 0.7)
                        {
                            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                            for (int row = 0; row < Rows; row++)
                            {
                                bricksGrid[row, col] = new Brick(margin + (brickWidth + 1) * col, margin + (brickHeight + 1) * row, brickWidth, brickHeight, randomColor);

                                int newR = Math.Max(0, Math.Min(255, randomColor.R + random.Next(-20, 21)));
                                int newG = Math.Max(0, Math.Min(255, randomColor.G + random.Next(-20, 21)));
                                int newB = Math.Max(0, Math.Min(255, randomColor.B + random.Next(-20, 21)));

                                randomColor = Color.FromArgb(newR, newG, newB);
                            }
                        }
                    }
                    break;
                case 4:
                case 5:
                    for (int row = 0; row < Rows; row++)
                    {
                        Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)); 
                        for (int col = 0; col < Columns / 2; col++)
                        {
                            if (random.Next(0,3) == 1)
                            {
                                bricksGrid[row, col] = new Brick(margin + (brickWidth + 1) * col, margin + (brickHeight + 1) * row, brickWidth, brickHeight, randomColor);
                                bricksGrid[row, Columns - col - 1] = new Brick(margin + (brickWidth + 1) * (Columns - col - 1), margin + (brickHeight + 1) * row, brickWidth, brickHeight, randomColor);

                                int newR = Math.Max(0, Math.Min(255, randomColor.R + random.Next(-20, 21)));
                                int newG = Math.Max(0, Math.Min(255, randomColor.G + random.Next(-20, 21)));
                                int newB = Math.Max(0, Math.Min(255, randomColor.B + random.Next(-20, 21)));

                                randomColor = Color.FromArgb(newR, newG, newB);
                            }
                        }

                        if (Columns % 2 == 1)
                            if (random.Next(0, 2) == 1)
                                bricksGrid[row, Columns / 2] = new Brick(margin + (brickWidth + 1) * (Columns - Columns / 2 - 1), margin + (brickHeight + 1) * row, brickWidth, brickHeight, randomColor);
                    }
                    break;
                default:
                    break;
            }
        }

        public void CreateNextLevel()
        {
            currentLevel++;
            bricksGrid = new Brick[Rows, Columns];
            CreateMap();
        }

        public bool CheckLevelEnd()
        {
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    if (bricksGrid[row, col] != null)
                        return false;
            return true;
        }

        public void ChangeBricsSize(float xRatio, float yRatio)
        {
            panelWidth = (int)Math.Round(Math.Round(panelWidth / this.xRatio) * xRatio);
            panelHeight = (int)Math.Round(Math.Round(panelHeight / this.yRatio) * yRatio);

            foreach (Brick brick in bricksGrid)
                if (brick != null)
                    brick.ChangeSize(xRatio, yRatio);
        }
    }
}