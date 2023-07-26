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

        public bool IsNewSettings { get { return NewSettings; } }

        public Settings()
        {
            InitializeComponent();
        }

        private void NewGameSettingsButton_Click(object sender, EventArgs e)
        {
            NewSettings = true;
            this.Close();
        }

        private void CancelSettingsButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
