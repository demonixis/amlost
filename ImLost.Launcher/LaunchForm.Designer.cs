namespace ImLost.Launcher
{
    partial class LaunchForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isFullScreen = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.screenHeight = new System.Windows.Forms.TextBox();
            this.screenWidth = new System.Windows.Forms.TextBox();
            this.detectBestResolution = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.enabledSpeech = new System.Windows.Forms.CheckBox();
            this.enabledMusic = new System.Windows.Forms.CheckBox();
            this.enabledSound = new System.Windows.Forms.CheckBox();
            this.playButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.isFullScreen);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.screenHeight);
            this.groupBox1.Controls.Add(this.screenWidth);
            this.groupBox1.Controls.Add(this.detectBestResolution);
            this.groupBox1.Location = new System.Drawing.Point(12, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 110);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Affichage";
            // 
            // isFullScreen
            // 
            this.isFullScreen.AutoSize = true;
            this.isFullScreen.Enabled = false;
            this.isFullScreen.Location = new System.Drawing.Point(8, 89);
            this.isFullScreen.Name = "isFullScreen";
            this.isFullScreen.Size = new System.Drawing.Size(125, 17);
            this.isFullScreen.TabIndex = 5;
            this.isFullScreen.Text = "Activer le plein écran";
            this.isFullScreen.UseVisualStyleBackColor = true;
            this.isFullScreen.CheckedChanged += new System.EventHandler(this.isFullScreen_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hauteur";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Largeur";
            // 
            // screenHeight
            // 
            this.screenHeight.Enabled = false;
            this.screenHeight.Location = new System.Drawing.Point(67, 63);
            this.screenHeight.Name = "screenHeight";
            this.screenHeight.Size = new System.Drawing.Size(113, 20);
            this.screenHeight.TabIndex = 2;
            this.screenHeight.Text = "720";
            // 
            // screenWidth
            // 
            this.screenWidth.Enabled = false;
            this.screenWidth.Location = new System.Drawing.Point(67, 39);
            this.screenWidth.Name = "screenWidth";
            this.screenWidth.Size = new System.Drawing.Size(113, 20);
            this.screenWidth.TabIndex = 1;
            this.screenWidth.Text = "1280";
            // 
            // detectBestResolution
            // 
            this.detectBestResolution.AutoSize = true;
            this.detectBestResolution.Checked = true;
            this.detectBestResolution.CheckState = System.Windows.Forms.CheckState.Checked;
            this.detectBestResolution.Location = new System.Drawing.Point(6, 19);
            this.detectBestResolution.Name = "detectBestResolution";
            this.detectBestResolution.Size = new System.Drawing.Size(174, 17);
            this.detectBestResolution.TabIndex = 0;
            this.detectBestResolution.Text = "Determiner la meilleur résolution";
            this.detectBestResolution.UseVisualStyleBackColor = true;
            this.detectBestResolution.CheckedChanged += new System.EventHandler(this.detectBestResolution_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkLabel1);
            this.groupBox2.Controls.Add(this.enabledSpeech);
            this.groupBox2.Controls.Add(this.enabledMusic);
            this.groupBox2.Controls.Add(this.enabledSound);
            this.groupBox2.Location = new System.Drawing.Point(289, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(252, 110);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Audio";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(162, 66);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(74, 13);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "(configuration)";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // enabledSpeech
            // 
            this.enabledSpeech.AutoSize = true;
            this.enabledSpeech.Location = new System.Drawing.Point(6, 65);
            this.enabledSpeech.Name = "enabledSpeech";
            this.enabledSpeech.Size = new System.Drawing.Size(150, 17);
            this.enabledSpeech.TabIndex = 2;
            this.enabledSpeech.Text = "Activer la synthèse vocale";
            this.enabledSpeech.UseVisualStyleBackColor = true;
            this.enabledSpeech.CheckedChanged += new System.EventHandler(this.audioCheckbox_CheckedChanged);
            // 
            // enabledMusic
            // 
            this.enabledMusic.AutoSize = true;
            this.enabledMusic.Checked = true;
            this.enabledMusic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledMusic.Location = new System.Drawing.Point(6, 42);
            this.enabledMusic.Name = "enabledMusic";
            this.enabledMusic.Size = new System.Drawing.Size(112, 17);
            this.enabledMusic.TabIndex = 1;
            this.enabledMusic.Text = "Activer la musique";
            this.enabledMusic.UseVisualStyleBackColor = true;
            this.enabledMusic.CheckedChanged += new System.EventHandler(this.audioCheckbox_CheckedChanged);
            // 
            // enabledSound
            // 
            this.enabledSound.AutoSize = true;
            this.enabledSound.Checked = true;
            this.enabledSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledSound.Location = new System.Drawing.Point(6, 19);
            this.enabledSound.Name = "enabledSound";
            this.enabledSound.Size = new System.Drawing.Size(90, 17);
            this.enabledSound.TabIndex = 0;
            this.enabledSound.Text = "Activer le son";
            this.enabledSound.UseVisualStyleBackColor = true;
            this.enabledSound.CheckedChanged += new System.EventHandler(this.audioCheckbox_CheckedChanged);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(466, 269);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "Jouer";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.button_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(370, 269);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "Par défaut";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(523, 120);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // LaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 300);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LaunchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A.M. Lost Launcher";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox screenHeight;
        private System.Windows.Forms.TextBox screenWidth;
        private System.Windows.Forms.CheckBox detectBestResolution;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox enabledSpeech;
        private System.Windows.Forms.CheckBox enabledMusic;
        private System.Windows.Forms.CheckBox enabledSound;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox isFullScreen;
    }
}

