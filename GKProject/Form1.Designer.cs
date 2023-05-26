
namespace GKProject
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fogDensityLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fogDensityTrackBar = new System.Windows.Forms.TrackBar();
            this.gameSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.runButton = new System.Windows.Forms.Button();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.sphereParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.lngStepsTextBox = new System.Windows.Forms.Label();
            this.latStepsTextBox = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.latStepsTrackBar = new System.Windows.Forms.TrackBar();
            this.lngStepsTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.phongShadingRadioButton = new System.Windows.Forms.RadioButton();
            this.goraudShadingRadioButton = new System.Windows.Forms.RadioButton();
            this.flatShadingRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.attachedCameraRadioButton = new System.Windows.Forms.RadioButton();
            this.followingCameraRadioButton = new System.Windows.Forms.RadioButton();
            this.staticCameraRadioButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fogDensityTrackBar)).BeginInit();
            this.sphereParametersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.latStepsTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lngStepsTrackBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.gameSelectionComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.runButton);
            this.splitContainer1.Panel2.Controls.Add(this.fpsLabel);
            this.splitContainer1.Panel2.Controls.Add(this.sphereParametersGroupBox);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1167, 630);
            this.splitContainer1.SplitterDistance = 880;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fogDensityLabel);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.fogDensityTrackBar);
            this.groupBox3.Location = new System.Drawing.Point(3, 449);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 100);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Display";
            // 
            // fogDensityLabel
            // 
            this.fogDensityLabel.AutoSize = true;
            this.fogDensityLabel.Location = new System.Drawing.Point(242, 37);
            this.fogDensityLabel.Name = "fogDensityLabel";
            this.fogDensityLabel.Size = new System.Drawing.Size(19, 15);
            this.fogDensityLabel.TabIndex = 8;
            this.fogDensityLabel.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Fog Density";
            // 
            // fogDensityTrackBar
            // 
            this.fogDensityTrackBar.Location = new System.Drawing.Point(6, 37);
            this.fogDensityTrackBar.Maximum = 100;
            this.fogDensityTrackBar.Name = "fogDensityTrackBar";
            this.fogDensityTrackBar.Size = new System.Drawing.Size(230, 45);
            this.fogDensityTrackBar.TabIndex = 7;
            this.fogDensityTrackBar.Value = 10;
            this.fogDensityTrackBar.Scroll += new System.EventHandler(this.fogDensityTrackBar_Scroll);
            // 
            // gameSelectionComboBox
            // 
            this.gameSelectionComboBox.FormattingEnabled = true;
            this.gameSelectionComboBox.Items.AddRange(new object[] {
            "Mate in 2",
            "Sandomierz Gambit",
            "Légal Mate"});
            this.gameSelectionComboBox.Location = new System.Drawing.Point(9, 595);
            this.gameSelectionComboBox.Name = "gameSelectionComboBox";
            this.gameSelectionComboBox.Size = new System.Drawing.Size(181, 23);
            this.gameSelectionComboBox.TabIndex = 4;
            this.gameSelectionComboBox.Text = "Select animation...";
            this.gameSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.gameSelectionComboBox_SelectedIndexChanged);
            // 
            // runButton
            // 
            this.runButton.Enabled = false;
            this.runButton.Location = new System.Drawing.Point(196, 595);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Run!";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Location = new System.Drawing.Point(9, 9);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(38, 15);
            this.fpsLabel.TabIndex = 1;
            this.fpsLabel.Text = "FPS: 0";
            // 
            // sphereParametersGroupBox
            // 
            this.sphereParametersGroupBox.Controls.Add(this.lngStepsTextBox);
            this.sphereParametersGroupBox.Controls.Add(this.latStepsTextBox);
            this.sphereParametersGroupBox.Controls.Add(this.label3);
            this.sphereParametersGroupBox.Controls.Add(this.latStepsTrackBar);
            this.sphereParametersGroupBox.Controls.Add(this.lngStepsTrackBar);
            this.sphereParametersGroupBox.Controls.Add(this.label2);
            this.sphereParametersGroupBox.Controls.Add(this.label1);
            this.sphereParametersGroupBox.Location = new System.Drawing.Point(3, 284);
            this.sphereParametersGroupBox.Name = "sphereParametersGroupBox";
            this.sphereParametersGroupBox.Size = new System.Drawing.Size(277, 159);
            this.sphereParametersGroupBox.TabIndex = 2;
            this.sphereParametersGroupBox.TabStop = false;
            this.sphereParametersGroupBox.Text = "Sphere Parameters";
            // 
            // lngStepsTextBox
            // 
            this.lngStepsTextBox.AutoSize = true;
            this.lngStepsTextBox.Location = new System.Drawing.Point(242, 60);
            this.lngStepsTextBox.Name = "lngStepsTextBox";
            this.lngStepsTextBox.Size = new System.Drawing.Size(19, 15);
            this.lngStepsTextBox.TabIndex = 6;
            this.lngStepsTextBox.Text = "10";
            // 
            // latStepsTextBox
            // 
            this.latStepsTextBox.AutoSize = true;
            this.latStepsTextBox.Location = new System.Drawing.Point(242, 108);
            this.latStepsTextBox.Name = "latStepsTextBox";
            this.latStepsTextBox.Size = new System.Drawing.Size(19, 15);
            this.latStepsTextBox.TabIndex = 5;
            this.latStepsTextBox.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lat";
            // 
            // latStepsTrackBar
            // 
            this.latStepsTrackBar.Location = new System.Drawing.Point(39, 108);
            this.latStepsTrackBar.Maximum = 100;
            this.latStepsTrackBar.Minimum = 5;
            this.latStepsTrackBar.Name = "latStepsTrackBar";
            this.latStepsTrackBar.Size = new System.Drawing.Size(208, 45);
            this.latStepsTrackBar.TabIndex = 3;
            this.latStepsTrackBar.Value = 10;
            this.latStepsTrackBar.Scroll += new System.EventHandler(this.latStepsTrackBar_Scroll);
            // 
            // lngStepsTrackBar
            // 
            this.lngStepsTrackBar.Location = new System.Drawing.Point(39, 57);
            this.lngStepsTrackBar.Maximum = 100;
            this.lngStepsTrackBar.Minimum = 5;
            this.lngStepsTrackBar.Name = "lngStepsTrackBar";
            this.lngStepsTrackBar.Size = new System.Drawing.Size(208, 45);
            this.lngStepsTrackBar.TabIndex = 2;
            this.lngStepsTrackBar.Value = 10;
            this.lngStepsTrackBar.Scroll += new System.EventHandler(this.lngStepsTrackBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Steps along:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.phongShadingRadioButton);
            this.groupBox2.Controls.Add(this.goraudShadingRadioButton);
            this.groupBox2.Controls.Add(this.flatShadingRadioButton);
            this.groupBox2.Location = new System.Drawing.Point(3, 179);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 99);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shading";
            // 
            // phongShadingRadioButton
            // 
            this.phongShadingRadioButton.AutoSize = true;
            this.phongShadingRadioButton.Location = new System.Drawing.Point(6, 72);
            this.phongShadingRadioButton.Name = "phongShadingRadioButton";
            this.phongShadingRadioButton.Size = new System.Drawing.Size(60, 19);
            this.phongShadingRadioButton.TabIndex = 2;
            this.phongShadingRadioButton.Text = "Phong";
            this.phongShadingRadioButton.UseVisualStyleBackColor = true;
            this.phongShadingRadioButton.CheckedChanged += new System.EventHandler(this.phongShadingRadioButton_CheckedChanged);
            // 
            // goraudShadingRadioButton
            // 
            this.goraudShadingRadioButton.AutoSize = true;
            this.goraudShadingRadioButton.Location = new System.Drawing.Point(6, 47);
            this.goraudShadingRadioButton.Name = "goraudShadingRadioButton";
            this.goraudShadingRadioButton.Size = new System.Drawing.Size(64, 19);
            this.goraudShadingRadioButton.TabIndex = 1;
            this.goraudShadingRadioButton.Text = "Goraud";
            this.goraudShadingRadioButton.UseVisualStyleBackColor = true;
            this.goraudShadingRadioButton.CheckedChanged += new System.EventHandler(this.goraudShadingRadioButton_CheckedChanged);
            // 
            // flatShadingRadioButton
            // 
            this.flatShadingRadioButton.AutoSize = true;
            this.flatShadingRadioButton.Checked = true;
            this.flatShadingRadioButton.Location = new System.Drawing.Point(6, 22);
            this.flatShadingRadioButton.Name = "flatShadingRadioButton";
            this.flatShadingRadioButton.Size = new System.Drawing.Size(44, 19);
            this.flatShadingRadioButton.TabIndex = 0;
            this.flatShadingRadioButton.TabStop = true;
            this.flatShadingRadioButton.Text = "Flat";
            this.flatShadingRadioButton.UseVisualStyleBackColor = true;
            this.flatShadingRadioButton.CheckedChanged += new System.EventHandler(this.flatShadingRadioButton_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.attachedCameraRadioButton);
            this.groupBox1.Controls.Add(this.followingCameraRadioButton);
            this.groupBox1.Controls.Add(this.staticCameraRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 140);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // attachedCameraRadioButton
            // 
            this.attachedCameraRadioButton.AutoSize = true;
            this.attachedCameraRadioButton.Location = new System.Drawing.Point(6, 74);
            this.attachedCameraRadioButton.Name = "attachedCameraRadioButton";
            this.attachedCameraRadioButton.Size = new System.Drawing.Size(167, 19);
            this.attachedCameraRadioButton.TabIndex = 2;
            this.attachedCameraRadioButton.Text = "Attached to moving object";
            this.attachedCameraRadioButton.UseVisualStyleBackColor = true;
            this.attachedCameraRadioButton.CheckedChanged += new System.EventHandler(this.attachedCameraRadioButton_CheckedChanged);
            // 
            // followingCameraRadioButton
            // 
            this.followingCameraRadioButton.AutoSize = true;
            this.followingCameraRadioButton.Location = new System.Drawing.Point(6, 48);
            this.followingCameraRadioButton.Name = "followingCameraRadioButton";
            this.followingCameraRadioButton.Size = new System.Drawing.Size(157, 19);
            this.followingCameraRadioButton.TabIndex = 1;
            this.followingCameraRadioButton.Text = "Following moving object";
            this.followingCameraRadioButton.UseVisualStyleBackColor = true;
            this.followingCameraRadioButton.CheckedChanged += new System.EventHandler(this.followingCameraRadioButton_CheckedChanged);
            // 
            // staticCameraRadioButton
            // 
            this.staticCameraRadioButton.AutoSize = true;
            this.staticCameraRadioButton.Checked = true;
            this.staticCameraRadioButton.Location = new System.Drawing.Point(6, 22);
            this.staticCameraRadioButton.Name = "staticCameraRadioButton";
            this.staticCameraRadioButton.Size = new System.Drawing.Size(54, 19);
            this.staticCameraRadioButton.TabIndex = 0;
            this.staticCameraRadioButton.TabStop = true;
            this.staticCameraRadioButton.Text = "Static";
            this.staticCameraRadioButton.UseVisualStyleBackColor = true;
            this.staticCameraRadioButton.CheckedChanged += new System.EventHandler(this.staticCameraRadioButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 630);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Chess Underground Police – Sharp Fog Shadows Edition";
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fogDensityTrackBar)).EndInit();
            this.sphereParametersGroupBox.ResumeLayout(false);
            this.sphereParametersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.latStepsTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lngStepsTrackBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton phongShadingRadioButton;
        private System.Windows.Forms.RadioButton goraudShadingRadioButton;
        private System.Windows.Forms.RadioButton flatShadingRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox sphereParametersGroupBox;
        private System.Windows.Forms.Label lngStepsTextBox;
        private System.Windows.Forms.Label latStepsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar latStepsTrackBar;
        private System.Windows.Forms.TrackBar lngStepsTrackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.ComboBox gameSelectionComboBox;
        private System.Windows.Forms.RadioButton attachedCameraRadioButton;
        private System.Windows.Forms.RadioButton followingCameraRadioButton;
        private System.Windows.Forms.RadioButton staticCameraRadioButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label fogDensityLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar fogDensityTrackBar;
    }
}

