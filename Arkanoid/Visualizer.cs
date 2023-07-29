using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

            gameManager = new GameManager(GamePanel.Width, GamePanel.Height, 3);

            CreateEnvironment();
        }

        void CreateEnvironment()
        {
            if (File.Exists("save.bin") && File.Exists("save.txt"))
                LoadToolStripMenuItem.Enabled = true;

            PointsValue.Text = gameManager.pointsValue.ToString();
            LifesValue.Text = gameManager.lifesValue.ToString();
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            BallTimer.Stop();

            Settings SettingsForm = new Settings();

            SettingsForm.ShowDialog();

            //if (SettingsForm.IsNewSettings)
            //{
            //    // Nowa gra z nowymi ustawieniami
            //}
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
            gameManager.Draw(e);
        }

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            gameManager.MakeTick();

            PointsValue.Text = gameManager.pointsValue.ToString();
            LifesValue.Text = gameManager.lifesValue.ToString();
            GamePanel.Refresh();

            CheckGameOver();
        }

        private void CheckGameOver()
        {
            if (gameManager.gameOverStatus)
            {
                BallTimer.Stop();
            }
        }
    }
}
