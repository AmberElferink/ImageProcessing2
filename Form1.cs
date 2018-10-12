using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        private Bitmap InputImage1;
        private Bitmap InputImage2;
        private Bitmap OutputImage1;
        private Bitmap OutputImage2;

        public INFOIBV()
        {
            InitializeComponent();
        }

        private void LoadImageButton1_Click(object sender, EventArgs e) { LoadImage(1); }
        private void LoadImageButton2_Click(object sender, EventArgs e) { LoadImage(2); }



        void LoadImage(int imageNumber)
        {

            if (openImageDialog.ShowDialog() == DialogResult.OK)             // Open File Dialog
            {
                string file = openImageDialog.FileName;                     // Get the file name
                if (imageNumber == 1)
                    imageFileName1.Text = file;                                  // Show file name
                else
                    imageFileName2.Text = file;

                if (InputImage1 != null) InputImage1.Dispose();               // Reset image
                InputImage1 = new Bitmap(file);                              // Create new Bitmap from file
                if (InputImage1.Size.Height <= 0 || InputImage1.Size.Width <= 0 ||
                    InputImage1.Size.Height > 512 || InputImage1.Size.Width > 512) // Dimension check
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                {
                    if (imageNumber == 1)
                        pictureBox1.Image = (Image)InputImage1;                 // Display input imageageNumber == 1)
                    else
                        pictureBox2.Image = (Image)InputImage1;
                }

            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (InputImage1 == null) return;                                 // Get out if no input image
            if (OutputImage1 != null) OutputImage1.Dispose();                 // Reset output image
            OutputImage1 = new Bitmap(InputImage1.Size.Width, InputImage1.Size.Height); // Create new output image
            Color[,] Image = new Color[InputImage1.Size.Width, InputImage1.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)

            // Setup progress bar
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = InputImage1.Size.Width * InputImage1.Size.Height;
            progressBar.Value = 1;
            progressBar.Step = 1;

            // Copy input Bitmap to array            
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    Image[x, y] = InputImage1.GetPixel(x, y);                // Set pixel color in array at (x,y)
                }
            }

            //==========================================================================================
            // TODO: include here your own code
            // example: create a negative image
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    Color pixelColor = Image[x, y];                         // Get the pixel color at coordinate (x,y)
                    Color updatedColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B); // Negative image
                    Image[x, y] = updatedColor;                             // Set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep();                              // Increment progress bar
                }
            }
            //==========================================================================================

            // Copy array to output Bitmap
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    OutputImage1.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                }
            }
            
            outputBox1.Image = (Image)OutputImage1;                         // Display output image
            progressBar.Visible = false;                                    // Hide progress bar
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage1 == null) return;                                // Get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                OutputImage1.Save(saveImageDialog.FileName);                 // Save the output image
        }

        private void RightAsInput_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
