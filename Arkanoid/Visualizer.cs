using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanoid
{
    public partial class Visualizer : Form
    {
        public Visualizer()
        {
            InitializeComponent();
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
    }
}
