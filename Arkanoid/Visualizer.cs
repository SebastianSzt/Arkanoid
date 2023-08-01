using System;
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
        private GameManager gameManager;
        private bool _moveLeft;
        private bool _moveRight;
        private Keys _lastKey;
        private Timer _movementTimer;
        private double timeCounter = 0.0;
        private Rectangle VisualizerOrginalRectangle;
        private Rectangle GamePanelOrginalRectangle;
        float xRatio;
        float yRatio;

        public Visualizer()
        {
            InitializeComponent();

            gameManager = new GameManager(GamePanel.Width, GamePanel.Height, 2, 2, 45);

            VisualizerOrginalRectangle = new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
            GamePanelOrginalRectangle = new Rectangle(GamePanel.Location.X, GamePanel.Location.Y, GamePanel.Size.Width, GamePanel.Size.Height);

            _movementTimer = new Timer { Interval = 50 };
            _movementTimer.Tick += _movementTimer_Tick;

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

        private void _movementTimer_Tick(object sender, EventArgs e)
        {
            _DoMovement();
        }

        private void _DoMovement()
        {
            if (_lastKey == Keys.A)
            {
                if (_moveLeft)
                    gameManager.MovePaddle(Keys.A);
                else if (_moveRight)
                    gameManager.MovePaddle(Keys.D);
            }
            else if (_lastKey == Keys.D)
            {
                if (_moveRight)
                    gameManager.MovePaddle(Keys.D);
                else if (_moveLeft)
                    gameManager.MovePaddle(Keys.A);
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

        void RefreshEnvironment()
        {
            if (File.Exists("save.bin") && File.Exists("save.txt"))
                LoadToolStripMenuItem.Enabled = true;

            PointsValue.Text = gameManager.pointsValue.ToString();
            LifesValue.Text = gameManager.currentLifesValue.ToString() + "/" + gameManager.lifesValue.ToString();
            LevelValue.Text = gameManager.currentLevelValue.ToString() + "/" + gameManager.levelValue.ToString();
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            BallTimer.Stop();
            _ResetMovement();
            pauseLabel.Visible = true;

            Settings SettingsForm = new Settings(gameManager.levelValue, gameManager.lifesValue, gameManager.ballAccelerationIntervalValue);

            SettingsForm.ShowDialog();

            if (SettingsForm.IsNewSettings)
            {
                startLabel.Visible = true;
                pauseLabel.Visible = false;
                gameManager = new GameManager(GamePanel.Width, GamePanel.Height, SettingsForm.lifesValue, SettingsForm.levelValue, SettingsForm.ballAccelerationIntervalValue);
                RefreshEnvironment();
                GamePanel.Refresh();
            }
        }

        private void ShowGameInformations(object sender, EventArgs e)
        {
            BallTimer.Stop();
            _ResetMovement();
            pauseLabel.Visible = true;

            Informations InformationsForm = new Informations();

            InformationsForm.ShowDialog();
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gameManager.DrawGameObjects(e);
        }

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            timeCounter += BallTimer.Interval / 1000.0;

            if (timeCounter >= gameManager.ballAccelerationIntervalValue)
            {
                timeCounter -= gameManager.ballAccelerationIntervalValue;

                gameManager.AccelerateBall();
            }

            gameManager.MakeTick();

            GamePanel.Refresh();

            RefreshEnvironment();

            CheckGameStatus();
        }

        private void CheckGameStatus()
        {
            if (gameManager.gameOverStatus || gameManager.ballStart)
            {
                BallTimer.Stop();
                _ResetMovement();
            }

            if (gameManager.ballStart)
            {
                startLabel.Visible = true;

                gameManager.ChangeObjectsSize(GamePanel.Size.Width, GamePanel.Size.Height, xRatio, yRatio);
            }
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BallTimer.Stop();
            _ResetMovement();
            startLabel.Visible = true;
            pauseLabel.Visible = false;
            gameManager = new GameManager(GamePanel.Width, GamePanel.Height, gameManager.lifesValue, gameManager.levelValue, gameManager.ballAccelerationIntervalValue);
            gameManager.ChangeObjectsSize(GamePanel.Size.Width, GamePanel.Size.Height, xRatio, yRatio);
            RefreshEnvironment();
            GamePanel.Refresh();
        }

        private void Visualizer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !gameManager.gameOverStatus)
            {
                if (!gameManager.ballStart)
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
                    gameManager.ballStart = false;
                    startLabel.Visible = false;
                }
            }
            else if (BallTimer.Enabled || gameManager.ballStart)
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
                    gameManager.ResetPaddleVX();
                }
            }
            else if (e.KeyCode == Keys.D)
            {
                _moveRight = false;
                if (!_moveLeft && !_moveRight)
                {
                    _movementTimer.Stop();
                    gameManager.ResetPaddleVX();
                }
            }
        }

        private void Visualizer_Resize(object sender, EventArgs e)
        {
            xRatio = (float)(this.Width) / (float)(VisualizerOrginalRectangle.Width);
            yRatio = (float)(this.Height) / (float)(VisualizerOrginalRectangle.Height);

            GamePanel.Location = new Point((int)(GamePanelOrginalRectangle.Location.X * xRatio), GamePanelOrginalRectangle.Location.Y);
            GamePanel.Size = new Size((int)(GamePanelOrginalRectangle.Size.Width * xRatio), (int)(GamePanelOrginalRectangle.Size.Height * yRatio));
            Border.Location = new Point(GamePanel.Location.X - 1, GamePanel.Location.Y - 1);
            Border.Size = new Size(GamePanel.Size.Width + 2, GamePanel.Size.Height + 2);

            startLabel.Size = new Size(GamePanel.Size.Width, GamePanel.Size.Height);
            pauseLabel.Size = new Size(GamePanel.Size.Width, GamePanel.Size.Height);
            Points.Left = GamePanel.Location.X;
            PointsValue.Left = GamePanel.Location.X + Points.Size.Width;

            LifesValue.Left = GamePanel.Location.X + GamePanel.Size.Width - LifesValue.Size.Width;
            Lifes.Left = LifesValue.Left - LifesValue.Margin.Left - Lifes.Margin.Right - Lifes.Size.Width;
            LevelValue.Left = GamePanel.Location.X + GamePanel.Size.Width - LevelValue.Size.Width;
            Level.Left = LevelValue.Left - LevelValue.Margin.Left - Level.Margin.Right - Level.Size.Width;

            gameManager.ChangeObjectsSize(GamePanel.Size.Width, GamePanel.Size.Height, xRatio, yRatio);
        }
    }
}