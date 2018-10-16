namespace INFOIBV
{
    partial class INFOIBV
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
            this.LoadImageButton1 = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFileName1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.outputBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.kernelInput = new System.Windows.Forms.TextBox();
            this.RightAsInput = new System.Windows.Forms.CheckBox();
            this.ErosionRadio = new System.Windows.Forms.RadioButton();
            this.DilationRadio = new System.Windows.Forms.RadioButton();
            this.OpeningRadio = new System.Windows.Forms.RadioButton();
            this.ClosingRadio = new System.Windows.Forms.RadioButton();
            this.MinRadio = new System.Windows.Forms.RadioButton();
            this.MaxRadio = new System.Windows.Forms.RadioButton();
            this.ValueRadio = new System.Windows.Forms.RadioButton();
            this.BoundaryRadio = new System.Windows.Forms.RadioButton();
            this.FourierRadio = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.outputBox2 = new System.Windows.Forms.PictureBox();
            this.LoadImageButton2 = new System.Windows.Forms.Button();
            this.imageFileName2 = new System.Windows.Forms.TextBox();
            this.MessageBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton1
            // 
            this.LoadImageButton1.Location = new System.Drawing.Point(12, 12);
            this.LoadImageButton1.Name = "LoadImageButton1";
            this.LoadImageButton1.Size = new System.Drawing.Size(98, 23);
            this.LoadImageButton1.TabIndex = 0;
            this.LoadImageButton1.Text = "Load image 1...";
            this.LoadImageButton1.UseVisualStyleBackColor = true;
            this.LoadImageButton1.Click += new System.EventHandler(this.LoadImageButton1_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" +
    "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // imageFileName1
            // 
            this.imageFileName1.Location = new System.Drawing.Point(116, 14);
            this.imageFileName1.Name = "imageFileName1";
            this.imageFileName1.ReadOnly = true;
            this.imageFileName1.Size = new System.Drawing.Size(316, 20);
            this.imageFileName1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(714, 694);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(103, 23);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
            this.saveImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(867, 694);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(103, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save as BMP...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // outputBox1
            // 
            this.outputBox1.Location = new System.Drawing.Point(366, 45);
            this.outputBox1.Name = "outputBox1";
            this.outputBox1.Size = new System.Drawing.Size(320, 320);
            this.outputBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.outputBox1.TabIndex = 5;
            this.outputBox1.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(455, 15);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(475, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // kernelInput
            // 
            this.kernelInput.Location = new System.Drawing.Point(714, 118);
            this.kernelInput.Multiline = true;
            this.kernelInput.Name = "kernelInput";
            this.kernelInput.Size = new System.Drawing.Size(216, 128);
            this.kernelInput.TabIndex = 7;
            // 
            // RightAsInput
            // 
            this.RightAsInput.AutoSize = true;
            this.RightAsInput.Location = new System.Drawing.Point(714, 85);
            this.RightAsInput.Name = "RightAsInput";
            this.RightAsInput.Size = new System.Drawing.Size(134, 17);
            this.RightAsInput.TabIndex = 8;
            this.RightAsInput.Text = "Right image(s) as Input";
            this.RightAsInput.UseVisualStyleBackColor = true;
            this.RightAsInput.CheckedChanged += new System.EventHandler(this.RightAsInput_CheckedChanged);
            // 
            // ErosionRadio
            // 
            this.ErosionRadio.AutoSize = true;
            this.ErosionRadio.Location = new System.Drawing.Point(714, 262);
            this.ErosionRadio.Name = "ErosionRadio";
            this.ErosionRadio.Size = new System.Drawing.Size(60, 17);
            this.ErosionRadio.TabIndex = 9;
            this.ErosionRadio.TabStop = true;
            this.ErosionRadio.Text = "Erosion";
            this.ErosionRadio.UseVisualStyleBackColor = true;
            // 
            // DilationRadio
            // 
            this.DilationRadio.AutoSize = true;
            this.DilationRadio.Location = new System.Drawing.Point(714, 286);
            this.DilationRadio.Name = "DilationRadio";
            this.DilationRadio.Size = new System.Drawing.Size(60, 17);
            this.DilationRadio.TabIndex = 10;
            this.DilationRadio.TabStop = true;
            this.DilationRadio.Text = "Dilation";
            this.DilationRadio.UseVisualStyleBackColor = true;
            // 
            // OpeningRadio
            // 
            this.OpeningRadio.AutoSize = true;
            this.OpeningRadio.Location = new System.Drawing.Point(714, 331);
            this.OpeningRadio.Name = "OpeningRadio";
            this.OpeningRadio.Size = new System.Drawing.Size(65, 17);
            this.OpeningRadio.TabIndex = 11;
            this.OpeningRadio.TabStop = true;
            this.OpeningRadio.Text = "Opening";
            this.OpeningRadio.UseVisualStyleBackColor = true;
            // 
            // ClosingRadio
            // 
            this.ClosingRadio.AutoSize = true;
            this.ClosingRadio.Location = new System.Drawing.Point(714, 355);
            this.ClosingRadio.Name = "ClosingRadio";
            this.ClosingRadio.Size = new System.Drawing.Size(59, 17);
            this.ClosingRadio.TabIndex = 12;
            this.ClosingRadio.TabStop = true;
            this.ClosingRadio.Text = "Closing";
            this.ClosingRadio.UseVisualStyleBackColor = true;
            // 
            // MinRadio
            // 
            this.MinRadio.AutoSize = true;
            this.MinRadio.Location = new System.Drawing.Point(714, 402);
            this.MinRadio.Name = "MinRadio";
            this.MinRadio.Size = new System.Drawing.Size(42, 17);
            this.MinRadio.TabIndex = 13;
            this.MinRadio.TabStop = true;
            this.MinRadio.Text = "Min";
            this.MinRadio.UseVisualStyleBackColor = true;
            // 
            // MaxRadio
            // 
            this.MaxRadio.AutoSize = true;
            this.MaxRadio.Location = new System.Drawing.Point(714, 426);
            this.MaxRadio.Name = "MaxRadio";
            this.MaxRadio.Size = new System.Drawing.Size(45, 17);
            this.MaxRadio.TabIndex = 14;
            this.MaxRadio.TabStop = true;
            this.MaxRadio.Text = "Max";
            this.MaxRadio.UseVisualStyleBackColor = true;
            // 
            // ValueRadio
            // 
            this.ValueRadio.AutoSize = true;
            this.ValueRadio.Location = new System.Drawing.Point(714, 472);
            this.ValueRadio.Name = "ValueRadio";
            this.ValueRadio.Size = new System.Drawing.Size(97, 17);
            this.ValueRadio.TabIndex = 15;
            this.ValueRadio.TabStop = true;
            this.ValueRadio.Text = "Value Counting";
            this.ValueRadio.UseVisualStyleBackColor = true;
            // 
            // BoundaryRadio
            // 
            this.BoundaryRadio.AutoSize = true;
            this.BoundaryRadio.Location = new System.Drawing.Point(714, 513);
            this.BoundaryRadio.Name = "BoundaryRadio";
            this.BoundaryRadio.Size = new System.Drawing.Size(101, 17);
            this.BoundaryRadio.TabIndex = 16;
            this.BoundaryRadio.TabStop = true;
            this.BoundaryRadio.Text = "Boundary Trace";
            this.BoundaryRadio.UseVisualStyleBackColor = true;
            // 
            // FourierRadio
            // 
            this.FourierRadio.AutoSize = true;
            this.FourierRadio.Location = new System.Drawing.Point(714, 537);
            this.FourierRadio.Name = "FourierRadio";
            this.FourierRadio.Size = new System.Drawing.Size(138, 17);
            this.FourierRadio.TabIndex = 17;
            this.FourierRadio.TabStop = true;
            this.FourierRadio.Text = "Fourier shape descriptor";
            this.FourierRadio.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(13, 397);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(320, 320);
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // outputBox2
            // 
            this.outputBox2.Location = new System.Drawing.Point(366, 397);
            this.outputBox2.Name = "outputBox2";
            this.outputBox2.Size = new System.Drawing.Size(320, 320);
            this.outputBox2.TabIndex = 19;
            this.outputBox2.TabStop = false;
            // 
            // LoadImageButton2
            // 
            this.LoadImageButton2.Location = new System.Drawing.Point(13, 371);
            this.LoadImageButton2.Name = "LoadImageButton2";
            this.LoadImageButton2.Size = new System.Drawing.Size(98, 23);
            this.LoadImageButton2.TabIndex = 20;
            this.LoadImageButton2.Text = "Load image 2...";
            this.LoadImageButton2.UseVisualStyleBackColor = true;
            this.LoadImageButton2.Click += new System.EventHandler(this.LoadImageButton2_Click);
            // 
            // imageFileName2
            // 
            this.imageFileName2.Location = new System.Drawing.Point(117, 373);
            this.imageFileName2.Name = "imageFileName2";
            this.imageFileName2.ReadOnly = true;
            this.imageFileName2.Size = new System.Drawing.Size(316, 20);
            this.imageFileName2.TabIndex = 21;
            // 
            // MessageBox2
            // 
            this.MessageBox2.Location = new System.Drawing.Point(714, 577);
            this.MessageBox2.Name = "MessageBox2";
            this.MessageBox2.ReadOnly = true;
            this.MessageBox2.Size = new System.Drawing.Size(256, 20);
            this.MessageBox2.TabIndex = 23;
            // 
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 729);
            this.Controls.Add(this.MessageBox2);
            this.Controls.Add(this.imageFileName2);
            this.Controls.Add(this.LoadImageButton2);
            this.Controls.Add(this.outputBox2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.FourierRadio);
            this.Controls.Add(this.BoundaryRadio);
            this.Controls.Add(this.ValueRadio);
            this.Controls.Add(this.MaxRadio);
            this.Controls.Add(this.MinRadio);
            this.Controls.Add(this.ClosingRadio);
            this.Controls.Add(this.OpeningRadio);
            this.Controls.Add(this.DilationRadio);
            this.Controls.Add(this.ErosionRadio);
            this.Controls.Add(this.RightAsInput);
            this.Controls.Add(this.kernelInput);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.outputBox1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageFileName1);
            this.Controls.Add(this.LoadImageButton1);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "INFOIBV";
            this.ShowIcon = false;
            this.Text = "INFOIBV";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton1;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.TextBox imageFileName1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox outputBox1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox kernelInput;
        private System.Windows.Forms.CheckBox RightAsInput;
        private System.Windows.Forms.RadioButton ErosionRadio;
        private System.Windows.Forms.RadioButton DilationRadio;
        private System.Windows.Forms.RadioButton OpeningRadio;
        private System.Windows.Forms.RadioButton ClosingRadio;
        private System.Windows.Forms.RadioButton MinRadio;
        private System.Windows.Forms.RadioButton MaxRadio;
        private System.Windows.Forms.RadioButton ValueRadio;
        private System.Windows.Forms.RadioButton BoundaryRadio;
        private System.Windows.Forms.RadioButton FourierRadio;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox outputBox2;
        private System.Windows.Forms.Button LoadImageButton2;
        private System.Windows.Forms.TextBox imageFileName2;
        private System.Windows.Forms.TextBox MessageBox2;
    }
}

