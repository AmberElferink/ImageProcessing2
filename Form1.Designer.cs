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
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFileName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(16, 15);
            this.LoadImageButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(131, 28);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image...";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" +
    "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // imageFileName
            // 
            this.imageFileName.Location = new System.Drawing.Point(155, 17);
            this.imageFileName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.imageFileName.Name = "imageFileName";
            this.imageFileName.ReadOnly = true;
            this.imageFileName.Size = new System.Drawing.Size(420, 22);
            this.imageFileName.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(17, 55);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(683, 630);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(1439, 14);
            this.applyButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(137, 28);
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
            this.saveButton.Location = new System.Drawing.Point(1600, 14);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(127, 28);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save as BMP...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(708, 55);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(683, 630);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(783, 17);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(368, 25);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // kernelInput
            // 
            this.kernelInput.Location = new System.Drawing.Point(1439, 96);
            this.kernelInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.kernelInput.Multiline = true;
            this.kernelInput.Name = "kernelInput";
            this.kernelInput.Size = new System.Drawing.Size(287, 157);
            this.kernelInput.TabIndex = 7;
            // 
            // RightAsInput
            // 
            this.RightAsInput.AutoSize = true;
            this.RightAsInput.Location = new System.Drawing.Point(1439, 55);
            this.RightAsInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RightAsInput.Name = "RightAsInput";
            this.RightAsInput.Size = new System.Drawing.Size(176, 21);
            this.RightAsInput.TabIndex = 8;
            this.RightAsInput.Text = "Right image(s) as Input";
            this.RightAsInput.UseVisualStyleBackColor = true;
            this.RightAsInput.CheckedChanged += new System.EventHandler(this.RightAsInput_CheckedChanged);
            // 
            // ErosionRadio
            // 
            this.ErosionRadio.AutoSize = true;
            this.ErosionRadio.Location = new System.Drawing.Point(1439, 273);
            this.ErosionRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ErosionRadio.Name = "ErosionRadio";
            this.ErosionRadio.Size = new System.Drawing.Size(77, 21);
            this.ErosionRadio.TabIndex = 9;
            this.ErosionRadio.TabStop = true;
            this.ErosionRadio.Text = "Erosion";
            this.ErosionRadio.UseVisualStyleBackColor = true;
            // 
            // DilationRadio
            // 
            this.DilationRadio.AutoSize = true;
            this.DilationRadio.Location = new System.Drawing.Point(1439, 303);
            this.DilationRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DilationRadio.Name = "DilationRadio";
            this.DilationRadio.Size = new System.Drawing.Size(76, 21);
            this.DilationRadio.TabIndex = 10;
            this.DilationRadio.TabStop = true;
            this.DilationRadio.Text = "Dilation";
            this.DilationRadio.UseVisualStyleBackColor = true;
            // 
            // OpeningRadio
            // 
            this.OpeningRadio.AutoSize = true;
            this.OpeningRadio.Location = new System.Drawing.Point(1439, 358);
            this.OpeningRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OpeningRadio.Name = "OpeningRadio";
            this.OpeningRadio.Size = new System.Drawing.Size(83, 21);
            this.OpeningRadio.TabIndex = 11;
            this.OpeningRadio.TabStop = true;
            this.OpeningRadio.Text = "Opening";
            this.OpeningRadio.UseVisualStyleBackColor = true;
            // 
            // ClosingRadio
            // 
            this.ClosingRadio.AutoSize = true;
            this.ClosingRadio.Location = new System.Drawing.Point(1439, 388);
            this.ClosingRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClosingRadio.Name = "ClosingRadio";
            this.ClosingRadio.Size = new System.Drawing.Size(75, 21);
            this.ClosingRadio.TabIndex = 12;
            this.ClosingRadio.TabStop = true;
            this.ClosingRadio.Text = "Closing";
            this.ClosingRadio.UseVisualStyleBackColor = true;
            // 
            // MinRadio
            // 
            this.MinRadio.AutoSize = true;
            this.MinRadio.Location = new System.Drawing.Point(1439, 446);
            this.MinRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinRadio.Name = "MinRadio";
            this.MinRadio.Size = new System.Drawing.Size(51, 21);
            this.MinRadio.TabIndex = 13;
            this.MinRadio.TabStop = true;
            this.MinRadio.Text = "Min";
            this.MinRadio.UseVisualStyleBackColor = true;
            // 
            // MaxRadio
            // 
            this.MaxRadio.AutoSize = true;
            this.MaxRadio.Location = new System.Drawing.Point(1439, 475);
            this.MaxRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaxRadio.Name = "MaxRadio";
            this.MaxRadio.Size = new System.Drawing.Size(54, 21);
            this.MaxRadio.TabIndex = 14;
            this.MaxRadio.TabStop = true;
            this.MaxRadio.Text = "Max";
            this.MaxRadio.UseVisualStyleBackColor = true;
            // 
            // ValueRadio
            // 
            this.ValueRadio.AutoSize = true;
            this.ValueRadio.Location = new System.Drawing.Point(1439, 532);
            this.ValueRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ValueRadio.Name = "ValueRadio";
            this.ValueRadio.Size = new System.Drawing.Size(125, 21);
            this.ValueRadio.TabIndex = 15;
            this.ValueRadio.TabStop = true;
            this.ValueRadio.Text = "Value Counting";
            this.ValueRadio.UseVisualStyleBackColor = true;
            // 
            // BoundaryRadio
            // 
            this.BoundaryRadio.AutoSize = true;
            this.BoundaryRadio.Location = new System.Drawing.Point(1439, 582);
            this.BoundaryRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BoundaryRadio.Name = "BoundaryRadio";
            this.BoundaryRadio.Size = new System.Drawing.Size(131, 21);
            this.BoundaryRadio.TabIndex = 16;
            this.BoundaryRadio.TabStop = true;
            this.BoundaryRadio.Text = "Boundary Trace";
            this.BoundaryRadio.UseVisualStyleBackColor = true;
            // 
            // FourierRadio
            // 
            this.FourierRadio.AutoSize = true;
            this.FourierRadio.Location = new System.Drawing.Point(1439, 612);
            this.FourierRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FourierRadio.Name = "FourierRadio";
            this.FourierRadio.Size = new System.Drawing.Size(184, 21);
            this.FourierRadio.TabIndex = 17;
            this.FourierRadio.TabStop = true;
            this.FourierRadio.Text = "Fourier shape descriptor";
            this.FourierRadio.UseVisualStyleBackColor = true;
            // 
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1915, 709);
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
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageFileName);
            this.Controls.Add(this.LoadImageButton);
            this.Location = new System.Drawing.Point(10, 10);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "INFOIBV";
            this.ShowIcon = false;
            this.Text = "INFOIBV";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.TextBox imageFileName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox pictureBox2;
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
    }
}

