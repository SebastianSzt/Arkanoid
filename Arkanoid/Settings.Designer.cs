namespace Arkanoid
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            SettingsLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            NewGameSettingsButton = new System.Windows.Forms.Button();
            CancelSettingsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            SuspendLayout();
            // 
            // SettingsLabel
            // 
            SettingsLabel.AutoSize = true;
            SettingsLabel.Location = new System.Drawing.Point(19, 29);
            SettingsLabel.Margin = new System.Windows.Forms.Padding(10, 20, 10, 10);
            SettingsLabel.Name = "SettingsLabel";
            SettingsLabel.Size = new System.Drawing.Size(107, 18);
            SettingsLabel.TabIndex = 0;
            SettingsLabel.Text = "Ustawienia:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(19, 67);
            label1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(206, 18);
            label1.TabIndex = 1;
            label1.Text = "Ilość poziomów: (1-10)";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(238, 60);
            numericUpDown1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(120, 25);
            numericUpDown1.TabIndex = 2;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(19, 95);
            label2.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(152, 18);
            label2.TabIndex = 3;
            label2.Text = "Ilość żyć: (1-5)";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new System.Drawing.Point(184, 88);
            numericUpDown2.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new System.Drawing.Size(120, 25);
            numericUpDown2.TabIndex = 4;
            numericUpDown2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(19, 123);
            label3.Margin = new System.Windows.Forms.Padding(10, 5, 10, 20);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(395, 18);
            label3.TabIndex = 5;
            label3.Text = "Interwał przyśpieszania piłki: (60s - 120s)";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new System.Drawing.Point(409, 121);
            numericUpDown3.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            numericUpDown3.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 60, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new System.Drawing.Size(120, 25);
            numericUpDown3.TabIndex = 6;
            numericUpDown3.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // NewGameSettingsButton
            // 
            NewGameSettingsButton.ForeColor = System.Drawing.Color.Black;
            NewGameSettingsButton.Location = new System.Drawing.Point(130, 164);
            NewGameSettingsButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            NewGameSettingsButton.Name = "NewGameSettingsButton";
            NewGameSettingsButton.Size = new System.Drawing.Size(95, 26);
            NewGameSettingsButton.TabIndex = 7;
            NewGameSettingsButton.Text = "Nowa gra";
            NewGameSettingsButton.UseVisualStyleBackColor = true;
            NewGameSettingsButton.Click += NewGameSettingsButton_Click;
            // 
            // CancelSettingsButton
            // 
            CancelSettingsButton.ForeColor = System.Drawing.Color.Black;
            CancelSettingsButton.Location = new System.Drawing.Point(301, 164);
            CancelSettingsButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            CancelSettingsButton.Name = "CancelSettingsButton";
            CancelSettingsButton.Size = new System.Drawing.Size(95, 26);
            CancelSettingsButton.TabIndex = 8;
            CancelSettingsButton.Text = "Anuluj";
            CancelSettingsButton.UseVisualStyleBackColor = true;
            CancelSettingsButton.Click += CancelSettingsButton_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            ClientSize = new System.Drawing.Size(548, 219);
            Controls.Add(CancelSettingsButton);
            Controls.Add(NewGameSettingsButton);
            Controls.Add(numericUpDown3);
            Controls.Add(label3);
            Controls.Add(numericUpDown2);
            Controls.Add(label2);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            Controls.Add(SettingsLabel);
            Font = new System.Drawing.Font("Unispace", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            ForeColor = System.Drawing.Color.White;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4);
            Name = "Settings";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Ustawienia";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label SettingsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button NewGameSettingsButton;
        private System.Windows.Forms.Button CancelSettingsButton;
    }
}