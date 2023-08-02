﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Arkanoid
{
    public partial class Visualizer : Form
    {
        private GameManager GameManager;

        private bool _moveLeft;
        private bool _moveRight;
        private Keys _lastKey;
        private Timer _movementTimer;

        private double timeCounter = 0.0;

        private Rectangle VisualizerOrginalRectangle;
        private Rectangle GamePanelOrginalRectangle;
        private float xRatio = 1;
        private float yRatio = 1;

        public Visualizer()
        {
            InitializeComponent();

            GameManager = new GameManager(GamePanel.Width, GamePanel.Height, 2, 2, 45);

            _movementTimer = new Timer { Interval = 50 };
            _movementTimer.Tick += _movementTimer_Tick;

            VisualizerOrginalRectangle = new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            GamePanelOrginalRectangle = new Rectangle(GamePanel.Location.X, GamePanel.Location.Y, GamePanel.Width, GamePanel.Height);

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, GamePanel, new object[] { true });
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, Border, new object[] { true });
            this.DoubleBuffered = true;

            RefreshEnvironment();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            _ResetMovement();
            if (BallTimer.Enabled)
            {
                BallTimer.Stop();
                pauseLabel.Visible = true;
            }

            Settings SettingsForm = new Settings(GameManager.LevelValue, GameManager.LifesValue, GameManager.BallAccelerationIntervalValue);

            SettingsForm.ShowDialog();

            if (SettingsForm.NewSettingsStatus)
            {
                startLabel.Visible = true;
                pauseLabel.Visible = false;
                gameOverLabel.Visible = false;
                GameManager = new GameManager((int)Math.Round(GamePanel.Width / xRatio), (int)Math.Round(GamePanel.Height / yRatio), SettingsForm.lifesValue, SettingsForm.levelValue, SettingsForm.ballAccelerationIntervalValue);
                timeCounter = 0;
                GameManager.ChangeObjectsSize(xRatio, yRatio);
                RefreshEnvironment();
                GamePanel.Refresh();
            }
        }

        private void ShowGameInformations(object sender, EventArgs e)
        {
            _ResetMovement();
            if (BallTimer.Enabled)
            {
                BallTimer.Stop();
                pauseLabel.Visible = true;
            }

            Informations InformationsForm = new Informations();

            InformationsForm.ShowDialog();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BallTimer.Stop();
            _ResetMovement();
            startLabel.Visible = true;
            pauseLabel.Visible = false;
            gameOverLabel.Visible = false;
            GameManager = new GameManager((int)Math.Round(GamePanel.Width / xRatio), (int)Math.Round(GamePanel.Height / yRatio), GameManager.LifesValue, GameManager.LevelValue, GameManager.BallAccelerationIntervalValue);
            timeCounter = 0;
            GameManager.ChangeObjectsSize(xRatio, yRatio);
            RefreshEnvironment();
            GamePanel.Refresh();
        }

        private void RefreshEnvironment()
        {
            if (File.Exists("save.bin") && File.Exists("save.txt"))
                LoadToolStripMenuItem.Enabled = true;

            PointsValue.Text = GameManager.PointsValue.ToString();
            LifesValue.Text = GameManager.CurrentLifesValue.ToString() + "/" + GameManager.LifesValue.ToString();
            LevelValue.Text = GameManager.CurrentLevelValue.ToString() + "/" + GameManager.LevelValue.ToString();
        }

        private void _movementTimer_Tick(object sender, EventArgs e)
        {
            _DoMovement();
        }

        private void _DoMovement()
        {
            if (_lastKey == Keys.A)
            {
                if (_moveLeft)
                    GameManager.MovePaddle(Keys.A);
                else if (_moveRight)
                    GameManager.MovePaddle(Keys.D);
            }
            else if (_lastKey == Keys.D)
            {
                if (_moveRight)
                    GameManager.MovePaddle(Keys.D);
                else if (_moveLeft)
                    GameManager.MovePaddle(Keys.A);
            }

            GamePanel.Refresh();
        }

        private void _ResetMovement()
        {
            _moveLeft = false;
            _moveRight = false;
            _lastKey = 0;
            _movementTimer.Stop();
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            GameManager.DrawGameObjects(e);
        }

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            timeCounter += BallTimer.Interval / 1000.0;

            if (timeCounter >= GameManager.BallAccelerationIntervalValue)
            {
                timeCounter -= GameManager.BallAccelerationIntervalValue;

                GameManager.AccelerateBallVY = 1;
            }

            GameManager.MakeTick();

            GamePanel.Refresh();

            CheckGameStatus();

            RefreshEnvironment();
        }

        private void CheckGameStatus()
        {
            if (GameManager.GameOverStatus || GameManager.RoundStartStatus)
            {
                BallTimer.Stop();
                _ResetMovement();
            }

            if (GameManager.GameOverStatus)
            {
                gameOverLabel.Visible = true;
            }
            else if (GameManager.RoundStartStatus)
            {
                startLabel.Visible = true;
                timeCounter = 0;
                GameManager.ChangeObjectsSize(xRatio, yRatio);
            }
        }

        private void Visualizer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !GameManager.GameOverStatus)
            {
                if (!GameManager.RoundStartStatus)
                {
                    if (BallTimer.Enabled == true)
                    {
                        BallTimer.Stop();
                        _movementTimer.Stop();
                        pauseLabel.Visible = true;
                    }
                    else if (BallTimer.Enabled == false)
                    {
                        BallTimer.Start();
                        if (_moveLeft || _moveRight)
                            _movementTimer.Start();
                        pauseLabel.Visible = false;
                    }

                }
                else
                {
                    BallTimer.Start();
                    GameManager.RoundStartStatus = false;
                    startLabel.Visible = false;
                }
            }
            else if (BallTimer.Enabled || GameManager.RoundStartStatus)
            {
                if (!_moveLeft && e.KeyCode == Keys.A)
                {
                    _moveLeft = true;
                    _lastKey = Keys.A;
                    _DoMovement();
                    _movementTimer.Start();
                }
                else if (!_moveRight && e.KeyCode == Keys.D)
                {
                    _moveRight = true;
                    _lastKey = Keys.D;
                    _DoMovement();
                    _movementTimer.Start();
                }
            }
        }

        private void Visualizer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                _moveLeft = false;
                if (!_moveLeft && !_moveRight)
                {
                    _movementTimer.Stop();
                    GameManager.ResetPaddleVX = 0;
                }
            }
            else if (e.KeyCode == Keys.D)
            {
                _moveRight = false;
                if (!_moveLeft && !_moveRight)
                {
                    _movementTimer.Stop();
                    GameManager.ResetPaddleVX = 0;
                }
            }
        }

        private void Visualizer_Resize(object sender, EventArgs e)
        {
            _ResetMovement();

            if (WindowState == FormWindowState.Minimized)
            {
                if (BallTimer.Enabled)
                    BallTimer.Stop();
                if (!GameManager.RoundStartStatus)
                    pauseLabel.Visible = true;
            }
            else
            {
                xRatio = (float)(this.Width) / (float)(VisualizerOrginalRectangle.Width);
                yRatio = (float)(this.Height) / (float)(VisualizerOrginalRectangle.Height);

                GamePanel.Location = new Point((int)(GamePanelOrginalRectangle.Location.X * xRatio), GamePanelOrginalRectangle.Location.Y);
                GamePanel.Size = new Size((int)(GamePanelOrginalRectangle.Size.Width * xRatio), (int)(GamePanelOrginalRectangle.Size.Height * yRatio));
                Border.Location = new Point(GamePanel.Location.X - 1, GamePanel.Location.Y - 1);
                Border.Size = new Size(GamePanel.Width + 2, GamePanel.Height + 2);

                startLabel.Size = new Size(GamePanel.Width, GamePanel.Height);
                pauseLabel.Size = new Size(GamePanel.Width, GamePanel.Height);
                gameOverLabel.Size = new Size(GamePanel.Width, GamePanel.Height);
                Points.Left = GamePanel.Location.X;
                PointsValue.Left = GamePanel.Location.X + Points.Size.Width;

                LifesValue.Left = GamePanel.Location.X + GamePanel.Width - LifesValue.Size.Width;
                Lifes.Left = LifesValue.Left - LifesValue.Margin.Left - Lifes.Margin.Right - Lifes.Size.Width;
                LevelValue.Left = GamePanel.Location.X + GamePanel.Width - LevelValue.Size.Width;
                Level.Left = LevelValue.Left - LevelValue.Margin.Left - Level.Margin.Right - Level.Size.Width;

                GameManager.ChangeObjectsSize(xRatio, yRatio);
            }
        }
    }
}