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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizer));
            GamePanel = new System.Windows.Forms.Panel();
            gameWinLabel = new System.Windows.Forms.Label();
            gameOverLabel = new System.Windows.Forms.Label();
            pauseLabel = new System.Windows.Forms.Label();
            startLabel = new System.Windows.Forms.Label();
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
            Border = new System.Windows.Forms.Panel();
            Level = new System.Windows.Forms.Label();
            LevelValue = new System.Windows.Forms.Label();
            GamePanel.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // GamePanel
            // 
            GamePanel.BackColor = System.Drawing.Color.FromArgb(35, 35, 35);
            GamePanel.Controls.Add(gameWinLabel);
            GamePanel.Controls.Add(gameOverLabel);
            GamePanel.Controls.Add(pauseLabel);
            GamePanel.Controls.Add(startLabel);
            GamePanel.Location = new System.Drawing.Point(29, 127);
            GamePanel.Margin = new System.Windows.Forms.Padding(20, 40, 20, 40);
            GamePanel.Name = "GamePanel";
            GamePanel.Size = new System.Drawing.Size(400, 500);
            GamePanel.TabIndex = 0;
            GamePanel.Paint += GamePanel_Paint;
            // 
            // gameWinLabel
            // 
            gameWinLabel.BackColor = System.Drawing.Color.Transparent;
            gameWinLabel.Font = new System.Drawing.Font("Unispace", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            gameWinLabel.ForeColor = System.Drawing.Color.White;
            gameWinLabel.Location = new System.Drawing.Point(0, 0);
            gameWinLabel.Name = "gameWinLabel";
            gameWinLabel.Size = new System.Drawing.Size(400, 500);
            gameWinLabel.TabIndex = 3;
            gameWinLabel.Text = "Koniec gry, wygrałeś!";
            gameWinLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            gameWinLabel.Visible = false;
            // 
            // gameOverLabel
            // 
            gameOverLabel.BackColor = System.Drawing.Color.Transparent;
            gameOverLabel.Font = new System.Drawing.Font("Unispace", 11.9999981F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            gameOverLabel.ForeColor = System.Drawing.Color.White;
            gameOverLabel.Location = new System.Drawing.Point(0, 0);
            gameOverLabel.Name = "gameOverLabel";
            gameOverLabel.Size = new System.Drawing.Size(400, 500);
            gameOverLabel.TabIndex = 2;
            gameOverLabel.Text = "Koniec gry, przegrałeś!\r\n";
            gameOverLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            gameOverLabel.Visible = false;
            // 
            // pauseLabel
            // 
            pauseLabel.BackColor = System.Drawing.Color.Transparent;
            pauseLabel.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            pauseLabel.ForeColor = System.Drawing.Color.White;
            pauseLabel.Location = new System.Drawing.Point(0, 0);
            pauseLabel.Name = "pauseLabel";
            pauseLabel.Size = new System.Drawing.Size(400, 500);
            pauseLabel.TabIndex = 1;
            pauseLabel.Text = "PAUZA\r\n";
            pauseLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            pauseLabel.Visible = false;
            // 
            // startLabel
            // 
            startLabel.BackColor = System.Drawing.Color.Transparent;
            startLabel.Font = new System.Drawing.Font("Unispace", 11.9999981F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            startLabel.ForeColor = System.Drawing.Color.White;
            startLabel.Location = new System.Drawing.Point(0, 0);
            startLabel.Name = "startLabel";
            startLabel.Size = new System.Drawing.Size(400, 500);
            startLabel.TabIndex = 0;
            startLabel.Text = "Naciśnij spację, aby wystrzelić piłkę\r\n";
            startLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // Points
            // 
            Points.AutoSize = true;
            Points.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Points.ForeColor = System.Drawing.Color.White;
            Points.Location = new System.Drawing.Point(29, 64);
            Points.Margin = new System.Windows.Forms.Padding(3, 40, 0, 0);
            Points.Name = "Points";
            Points.Size = new System.Drawing.Size(94, 23);
            Points.TabIndex = 1;
            Points.Text = "Punkty:";
            // 
            // PointsValue
            // 
            PointsValue.AutoSize = true;
            PointsValue.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            PointsValue.ForeColor = System.Drawing.Color.White;
            PointsValue.Location = new System.Drawing.Point(123, 64);
            PointsValue.Margin = new System.Windows.Forms.Padding(0, 40, 3, 0);
            PointsValue.Name = "PointsValue";
            PointsValue.Size = new System.Drawing.Size(22, 23);
            PointsValue.TabIndex = 2;
            PointsValue.Text = "0";
            // 
            // Lifes
            // 
            Lifes.AutoSize = true;
            Lifes.Font = new System.Drawing.Font("Unispace", 11.9999981F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Lifes.ForeColor = System.Drawing.Color.White;
            Lifes.Location = new System.Drawing.Point(313, 44);
            Lifes.Margin = new System.Windows.Forms.Padding(3, 20, 5, 10);
            Lifes.Name = "Lifes";
            Lifes.Size = new System.Drawing.Size(69, 19);
            Lifes.TabIndex = 3;
            Lifes.Text = "Życia:";
            // 
            // LifesValue
            // 
            LifesValue.AutoSize = true;
            LifesValue.Font = new System.Drawing.Font("Unispace", 11.9999981F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            LifesValue.ForeColor = System.Drawing.Color.White;
            LifesValue.Location = new System.Drawing.Point(390, 44);
            LifesValue.Margin = new System.Windows.Forms.Padding(3, 20, 3, 10);
            LifesValue.Name = "LifesValue";
            LifesValue.Size = new System.Drawing.Size(39, 19);
            LifesValue.TabIndex = 4;
            LifesValue.Text = "0/0";
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
            NewGameToolStripMenuItem.Click += NewGameToolStripMenuItem_Click;
            // 
            // SaveToolStripMenuItem
            // 
            SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            SaveToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            SaveToolStripMenuItem.Text = "Zapisz grę";
            // 
            // LoadToolStripMenuItem
            // 
            LoadToolStripMenuItem.Enabled = false;
            LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            LoadToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            LoadToolStripMenuItem.Text = "Wczytaj zapis";
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
            BallTimer.Interval = 60;
            BallTimer.Tick += BallTimer_Tick;
            // 
            // Border
            // 
            Border.BackColor = System.Drawing.Color.White;
            Border.Location = new System.Drawing.Point(28, 126);
            Border.Name = "Border";
            Border.Size = new System.Drawing.Size(402, 502);
            Border.TabIndex = 6;
            // 
            // Level
            // 
            Level.AutoSize = true;
            Level.Font = new System.Drawing.Font("Unispace", 11.9999981F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Level.ForeColor = System.Drawing.Color.White;
            Level.Location = new System.Drawing.Point(303, 83);
            Level.Margin = new System.Windows.Forms.Padding(3, 10, 5, 0);
            Level.Name = "Level";
            Level.Size = new System.Drawing.Size(79, 19);
            Level.TabIndex = 7;
            Level.Text = "Poziom:";
            // 
            // LevelValue
            // 
            LevelValue.AutoSize = true;
            LevelValue.Font = new System.Drawing.Font("Unispace", 11.9999981F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            LevelValue.ForeColor = System.Drawing.Color.White;
            LevelValue.Location = new System.Drawing.Point(390, 83);
            LevelValue.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            LevelValue.Name = "LevelValue";
            LevelValue.Size = new System.Drawing.Size(39, 19);
            LevelValue.TabIndex = 8;
            LevelValue.Text = "0/0";
            // 
            // Visualizer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            ClientSize = new System.Drawing.Size(458, 676);
            Controls.Add(GamePanel);
            Controls.Add(LevelValue);
            Controls.Add(Level);
            Controls.Add(Border);
            Controls.Add(LifesValue);
            Controls.Add(Lifes);
            Controls.Add(PointsValue);
            Controls.Add(Points);
            Controls.Add(menuStrip);
            DoubleBuffered = true;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            MinimumSize = new System.Drawing.Size(474, 715);
            Name = "Visualizer";
            Text = "Arkanoid";
            KeyDown += Visualizer_KeyDown;
            KeyUp += Visualizer_KeyUp;
            Resize += Visualizer_Resize;
            GamePanel.ResumeLayout(false);
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
        private System.Windows.Forms.Panel Border;
        private System.Windows.Forms.Label Level;
        private System.Windows.Forms.Label LevelValue;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label pauseLabel;
        private System.Windows.Forms.Label gameOverLabel;
        private System.Windows.Forms.Label gameWinLabel;
    }
}
