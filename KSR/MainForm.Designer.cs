
namespace KSR
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.serviceStatusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.featur6 = new System.Windows.Forms.CheckBox();
            this.teStoplistPath = new System.Windows.Forms.TextBox();
            this.featur5 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.featur4 = new System.Windows.Forms.CheckBox();
            this.chNormalization = new System.Windows.Forms.CheckBox();
            this.featur3 = new System.Windows.Forms.CheckBox();
            this.chStemmization = new System.Windows.Forms.CheckBox();
            this.featur2 = new System.Windows.Forms.CheckBox();
            this.featur1 = new System.Windows.Forms.CheckBox();
            this.chStoplist = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbMetrics = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbParts = new System.Windows.Forms.ComboBox();
            this.cmbFrequeny = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbClassifer = new System.Windows.Forms.ComboBox();
            this.chSets = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.btnInstall);
            this.groupBox1.Controls.Add(this.serviceStatusLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(407, 187);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ustawienia usługi";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 118);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(395, 53);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Uruchom";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(6, 59);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(395, 53);
            this.btnInstall.TabIndex = 2;
            this.btnInstall.Text = "Zainstaluj";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // serviceStatusLabel
            // 
            this.serviceStatusLabel.AutoSize = true;
            this.serviceStatusLabel.Location = new System.Drawing.Point(171, 31);
            this.serviceStatusLabel.Name = "serviceStatusLabel";
            this.serviceStatusLabel.Size = new System.Drawing.Size(154, 25);
            this.serviceStatusLabel.TabIndex = 1;
            this.serviceStatusLabel.Text = "Odinstalowana";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status usługi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Klasyfikator";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chSets);
            this.groupBox3.Controls.Add(this.btnSaveSettings);
            this.groupBox3.Controls.Add(this.featur6);
            this.groupBox3.Controls.Add(this.teStoplistPath);
            this.groupBox3.Controls.Add(this.featur5);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.featur4);
            this.groupBox3.Controls.Add(this.chNormalization);
            this.groupBox3.Controls.Add(this.featur3);
            this.groupBox3.Controls.Add(this.chStemmization);
            this.groupBox3.Controls.Add(this.featur2);
            this.groupBox3.Controls.Add(this.featur1);
            this.groupBox3.Controls.Add(this.chStoplist);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbMetrics);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmbParts);
            this.groupBox3.Controls.Add(this.cmbFrequeny);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbClassifer);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(13, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(407, 612);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Konfiguracja aplikacji";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(12, 491);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(389, 55);
            this.btnSaveSettings.TabIndex = 16;
            this.btnSaveSettings.Text = "Zapisz";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // featur6
            // 
            this.featur6.AutoSize = true;
            this.featur6.Checked = true;
            this.featur6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.featur6.Location = new System.Drawing.Point(159, 455);
            this.featur6.Name = "featur6";
            this.featur6.Size = new System.Drawing.Size(124, 29);
            this.featur6.TabIndex = 5;
            this.featur6.Text = "Cecha 6";
            this.featur6.UseVisualStyleBackColor = true;
            // 
            // teStoplistPath
            // 
            this.teStoplistPath.Location = new System.Drawing.Point(159, 338);
            this.teStoplistPath.Name = "teStoplistPath";
            this.teStoplistPath.Size = new System.Drawing.Size(242, 31);
            this.teStoplistPath.TabIndex = 15;
            this.teStoplistPath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.teStoplistPath_MouseClick);
            // 
            // featur5
            // 
            this.featur5.AutoSize = true;
            this.featur5.Checked = true;
            this.featur5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.featur5.Location = new System.Drawing.Point(159, 420);
            this.featur5.Name = "featur5";
            this.featur5.Size = new System.Drawing.Size(124, 29);
            this.featur5.TabIndex = 4;
            this.featur5.Text = "Cecha 5";
            this.featur5.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 341);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 25);
            this.label7.TabIndex = 14;
            this.label7.Text = "Plik stoplisty";
            // 
            // featur4
            // 
            this.featur4.AutoSize = true;
            this.featur4.Checked = true;
            this.featur4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.featur4.Location = new System.Drawing.Point(159, 385);
            this.featur4.Name = "featur4";
            this.featur4.Size = new System.Drawing.Size(124, 29);
            this.featur4.TabIndex = 3;
            this.featur4.Text = "Cecha 4";
            this.featur4.UseVisualStyleBackColor = true;
            // 
            // chNormalization
            // 
            this.chNormalization.AutoSize = true;
            this.chNormalization.Checked = true;
            this.chNormalization.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chNormalization.Location = new System.Drawing.Point(159, 302);
            this.chNormalization.Name = "chNormalization";
            this.chNormalization.Size = new System.Drawing.Size(168, 29);
            this.chNormalization.TabIndex = 13;
            this.chNormalization.Text = "Normalizacja";
            this.chNormalization.UseVisualStyleBackColor = true;
            // 
            // featur3
            // 
            this.featur3.AutoSize = true;
            this.featur3.Checked = true;
            this.featur3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.featur3.Location = new System.Drawing.Point(12, 455);
            this.featur3.Name = "featur3";
            this.featur3.Size = new System.Drawing.Size(124, 29);
            this.featur3.TabIndex = 2;
            this.featur3.Text = "Cecha 3";
            this.featur3.UseVisualStyleBackColor = true;
            // 
            // chStemmization
            // 
            this.chStemmization.AutoSize = true;
            this.chStemmization.Location = new System.Drawing.Point(159, 267);
            this.chStemmization.Name = "chStemmization";
            this.chStemmization.Size = new System.Drawing.Size(166, 29);
            this.chStemmization.TabIndex = 12;
            this.chStemmization.Text = "Stemmizacja";
            this.chStemmization.UseVisualStyleBackColor = true;
            // 
            // featur2
            // 
            this.featur2.AutoSize = true;
            this.featur2.Checked = true;
            this.featur2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.featur2.Location = new System.Drawing.Point(12, 420);
            this.featur2.Name = "featur2";
            this.featur2.Size = new System.Drawing.Size(124, 29);
            this.featur2.TabIndex = 1;
            this.featur2.Text = "Cecha 2";
            this.featur2.UseVisualStyleBackColor = true;
            // 
            // featur1
            // 
            this.featur1.AutoSize = true;
            this.featur1.Checked = true;
            this.featur1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.featur1.Location = new System.Drawing.Point(12, 385);
            this.featur1.Name = "featur1";
            this.featur1.Size = new System.Drawing.Size(124, 29);
            this.featur1.TabIndex = 0;
            this.featur1.Text = "Cecha 1";
            this.featur1.UseVisualStyleBackColor = true;
            // 
            // chStoplist
            // 
            this.chStoplist.AutoSize = true;
            this.chStoplist.Location = new System.Drawing.Point(159, 232);
            this.chStoplist.Name = "chStoplist";
            this.chStoplist.Size = new System.Drawing.Size(140, 29);
            this.chStoplist.TabIndex = 11;
            this.chStoplist.Text = "Stop Lista";
            this.chStoplist.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "Metryka";
            // 
            // cmbMetrics
            // 
            this.cmbMetrics.FormattingEnabled = true;
            this.cmbMetrics.Location = new System.Drawing.Point(159, 154);
            this.cmbMetrics.Name = "cmbMetrics";
            this.cmbMetrics.Size = new System.Drawing.Size(242, 33);
            this.cmbMetrics.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Podział";
            // 
            // cmbParts
            // 
            this.cmbParts.FormattingEnabled = true;
            this.cmbParts.Location = new System.Drawing.Point(159, 115);
            this.cmbParts.Name = "cmbParts";
            this.cmbParts.Size = new System.Drawing.Size(242, 33);
            this.cmbParts.TabIndex = 6;
            // 
            // cmbFrequeny
            // 
            this.cmbFrequeny.FormattingEnabled = true;
            this.cmbFrequeny.Location = new System.Drawing.Point(159, 76);
            this.cmbFrequeny.Name = "cmbFrequeny";
            this.cmbFrequeny.Size = new System.Drawing.Size(242, 33);
            this.cmbFrequeny.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "TF/DF";
            // 
            // cmbClassifer
            // 
            this.cmbClassifer.FormattingEnabled = true;
            this.cmbClassifer.Location = new System.Drawing.Point(159, 38);
            this.cmbClassifer.Name = "cmbClassifer";
            this.cmbClassifer.Size = new System.Drawing.Size(242, 33);
            this.cmbClassifer.TabIndex = 1;
            // 
            // chSets
            // 
            this.chSets.AutoSize = true;
            this.chSets.Checked = true;
            this.chSets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chSets.Location = new System.Drawing.Point(159, 197);
            this.chSets.Name = "chSets";
            this.chSets.Size = new System.Drawing.Size(183, 29);
            this.chSets.TabIndex = 17;
            this.chSets.Text = "Osobne zbiory";
            this.chSets.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 831);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Panel konfiguracyjny";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label serviceStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbClassifer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbMetrics;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbParts;
        private System.Windows.Forms.ComboBox cmbFrequeny;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chStemmization;
        private System.Windows.Forms.CheckBox chStoplist;
        private System.Windows.Forms.CheckBox chNormalization;
        private System.Windows.Forms.TextBox teStoplistPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox featur6;
        private System.Windows.Forms.CheckBox featur5;
        private System.Windows.Forms.CheckBox featur4;
        private System.Windows.Forms.CheckBox featur3;
        private System.Windows.Forms.CheckBox featur2;
        private System.Windows.Forms.CheckBox featur1;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.CheckBox chSets;
    }
}