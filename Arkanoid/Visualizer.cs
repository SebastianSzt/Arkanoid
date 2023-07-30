using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public Visualizer()
        {
            InitializeComponent();

            gameManager = new GameManager(GamePanel.Width, GamePanel.Height, 2, 2, 8);

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

            Settings SettingsForm = new Settings(gameManager.levelValue, gameManager.lifesValue, gameManager.ballAccelerationIntervalValue);

            SettingsForm.ShowDialog();

            if (SettingsForm.IsNewSettings)
            {
                gameManager = new GameManager(GamePanel.Width, GamePanel.Height, SettingsForm.lifesValue, SettingsForm.levelValue, SettingsForm.ballAccelerationIntervalValue);
                RefreshEnvironment();
                GamePanel.Refresh();
            }
        }

        private void ShowGameInformations(object sender, EventArgs e)
        {
            BallTimer.Stop();

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
            gameManager.MakeTick();

            GamePanel.Refresh();

            CheckGameOver();

            RefreshEnvironment();
        }

        private void CheckGameOver()
        {
            if (gameManager.gameOverStatus || gameManager.ballStart)
            {
                BallTimer.Stop();
            }
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BallTimer.Stop();
            gameManager = new GameManager(GamePanel.Width, GamePanel.Height, gameManager.lifesValue, gameManager.levelValue, gameManager.ballAccelerationIntervalValue);
            RefreshEnvironment();
            GamePanel.Refresh();
        }

        private void Visualizer_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.A || e.KeyCode == Keys.D) && (BallTimer.Enabled || gameManager.ballStart))
            {
                gameManager.MovePaddle(e.KeyCode);
                GamePanel.Refresh();
            }
            else if (e.KeyCode == Keys.Space && !gameManager.gameOverStatus)
            {
                if (!gameManager.ballStart)
                {
                    if (BallTimer.Enabled == true)
                        BallTimer.Stop();
                    else if (BallTimer.Enabled == false)
                        BallTimer.Start();
                }
                else
                {
                    BallTimer.Start();
                    gameManager.ballStart = false;
                }
            }
        }

        private void Visualizer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                gameManager.ResetPaddleVX();
            }
        }
    }
}