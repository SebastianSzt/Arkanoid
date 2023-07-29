namespace Arkanoid
{
    partial class Visualizer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            GamePanel = new System.Windows.Forms.Panel();
            Points = new System.Windows.Forms.Label();
            PointsValue = new System.Windows.Forms.Label();
            Lifes = new System.Windows.Forms.Label();
            LifesValue = new System.Windows.Forms.Label();
            menuStrip = new System.Windows.Forms.MenuStrip();
            NewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            InformationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            BallTimer = new System.Windows.Forms.Timer(components);
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // GamePanel
            // 
            GamePanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            GamePanel.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            GamePanel.Location = new System.Drawing.Point(29, 127);
            GamePanel.Margin = new System.Windows.Forms.Padding(20, 40, 20, 40);
            GamePanel.Name = "GamePanel";
            GamePanel.Size = new System.Drawing.Size(400, 500);
            GamePanel.TabIndex = 0;
            GamePanel.Paint += GamePanel_Paint;
            // 
            // Points
            // 
            Points.Anchor = System.Windows.Forms.AnchorStyles.Top;
            Points.AutoSize = true;
            Points.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Points.ForeColor = System.Drawing.Color.White;
            Points.Location = new System.Drawing.Point(29, 64);
            Points.Margin = new System.Windows.Forms.Padding(3, 40, 5, 0);
            Points.Name = "Points";
            Points.Size = new System.Drawing.Size(94, 23);
            Points.TabIndex = 1;
            Points.Text = "Punkty:";
            // 
            // PointsValue
            // 
            PointsValue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            PointsValue.AutoSize = true;
            PointsValue.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            PointsValue.ForeColor = System.Drawing.Color.White;
            PointsValue.Location = new System.Drawing.Point(131, 64);
            PointsValue.Margin = new System.Windows.Forms.Padding(3, 40, 3, 0);
            PointsValue.Name = "PointsValue";
            PointsValue.Size = new System.Drawing.Size(22, 23);
            PointsValue.TabIndex = 2;
            PointsValue.Text = "0";
            // 
            // Lifes
            // 
            Lifes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            Lifes.AutoSize = true;
            Lifes.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Lifes.ForeColor = System.Drawing.Color.White;
            Lifes.Location = new System.Drawing.Point(317, 64);
            Lifes.Margin = new System.Windows.Forms.Padding(3, 40, 5, 0);
            Lifes.Name = "Lifes";
            Lifes.Size = new System.Drawing.Size(82, 23);
            Lifes.TabIndex = 3;
            Lifes.Text = "Życia:";
            // 
            // LifesValue
            // 
            LifesValue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            LifesValue.AutoSize = true;
            LifesValue.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            LifesValue.ForeColor = System.Drawing.Color.White;
            LifesValue.Location = new System.Drawing.Point(407, 64);
            LifesValue.Margin = new System.Windows.Forms.Padding(3, 40, 3, 0);
            LifesValue.Name = "LifesValue";
            LifesValue.Size = new System.Drawing.Size(22, 23);
            LifesValue.TabIndex = 4;
            LifesValue.Text = "0";
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { NewGameToolStripMenuItem, SaveToolStripMenuItem, LoadToolStripMenuItem, SettingsToolStripMenuItem, InformationsToolStripMenuItem });
            menuStrip.Location = new System.Drawing.Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new System.Drawing.Size(458, 24);
            menuStrip.TabIndex = 5;
            menuStrip.Text = "menuStrip1";
            // 
            // NewGameToolStripMenuItem
            // 
            NewGameToolStripMenuItem.Name = "NewGameToolStripMenuItem";
            NewGameToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            NewGameToolStripMenuItem.Text = "Nowa gra";
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            SaveToolStripMenuItem.Text = "Zapisz";
            // 
            // LoadToolStripMenuItem
            // 
            LoadToolStripMenuItem.Enabled = false;
            LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            LoadToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            LoadToolStripMenuItem.Text = "Wczytaj";
            // 
            // SettingsToolStripMenuItem
            // 
            SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            SettingsToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            SettingsToolStripMenuItem.Text = "Ustawienia";
            SettingsToolStripMenuItem.Click += ShowSettings;
            // 
            // InformationsToolStripMenuItem
            // 
            InformationsToolStripMenuItem.Name = "InformationsToolStripMenuItem";
            InformationsToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            InformationsToolStripMenuItem.Text = "Informacje";
            InformationsToolStripMenuItem.Click += ShowGameInformations;
            // 
            // BallTimer
            // 
            BallTimer.Enabled = true;
            BallTimer.Interval = 50;
            BallTimer.Tick += BallTimer_Tick;
            // 
            // Visualizer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            ClientSize = new System.Drawing.Size(458, 676);
            Controls.Add(LifesValue);
            Controls.Add(Lifes);
            Controls.Add(PointsValue);
            Controls.Add(Points);
            Controls.Add(GamePanel);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            MinimumSize = new System.Drawing.Size(474, 715);
            Name = "Visualizer";
            Text = "Arkanoid";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel GamePanel;
        private System.Windows.Forms.Label Points;
        private System.Windows.Forms.Label PointsValue;
        private System.Windows.Forms.Label Lifes;
        private System.Windows.Forms.Label LifesValue;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem NewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InformationsToolStripMenuItem;
        private System.Windows.Forms.Timer BallTimer;
    }
}
