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
    public partial class Settings : Form
    {
        private bool NewSettings = false;

        public int levelValue { get; set; }
        public int lifesValue { get; set; }
        public int ballAccelerationIntervalValue { get; set; }

        public bool NewSettingsStatus { get { return NewSettings; } }

        public Settings()
        {
            InitializeComponent();
        }

        public Settings(int levelValue, int lifesValue, int ballAccelerationIntervalValue)
        {
            InitializeComponent();

            this.levelValue = levelValue;
            this.lifesValue = lifesValue;
            this.ballAccelerationIntervalValue = ballAccelerationIntervalValue;

            numericUpDown1.Value = levelValue;
            numericUpDown2.Value = lifesValue;
            numericUpDown3.Value = ballAccelerationIntervalValue;
        }

        private void NewGameSettingsButton_Click(object sender, EventArgs e)
        {
            levelValue = ((int)numericUpDown1.Value);
            lifesValue = ((int)numericUpDown2.Value);
            ballAccelerationIntervalValue = ((int)numericUpDown3.Value);
            NewSettings = true;
            this.Close();
        }

        private void CancelSettingsButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
