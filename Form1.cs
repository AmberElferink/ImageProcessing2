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
        private Bitmap OutputImage1;
        Color[,] Image;
        Color[,] newImage;

        int alow = 0;
        int ahigh = 0;

        public INFOIBV()
        {
            InitializeComponent();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
           if (openImageDialog.ShowDialog() == DialogResult.OK)             // Open File Dialog
            {
                string file = openImageDialog.FileName;                     // Get the file name
                imageFileName.Text = file;                                  // Show file name
                if (InputImage1 != null) InputImage1.Dispose();               // Reset image
                InputImage1 = new Bitmap(file);                              // Create new Bitmap from file
                if (InputImage1.Size.Height <= 0 || InputImage1.Size.Width <= 0 ||
                    InputImage1.Size.Height > 512 || InputImage1.Size.Width > 512) // Dimension check
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                    pictureBox1.Image = (Image) InputImage1;                 // Display input image
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (InputImage1 == null)
            {
                //MessageBox2.Text = "Please Load an image first.";
                return;
            }

            resetForApply();

            if (ErosionRadio.Checked == true)
                ApplyErosionDilationFilter(true);
            else if (DilationRadio.Checked == true)
                ApplyErosionDilationFilter(false);
            else if (OpeningRadio.Checked == true)
                ApplyOpeningClosingFilter(true);
            else if (ClosingRadio.Checked == true)
                ApplyOpeningClosingFilter(false);
            else if (ValueRadio.Checked == true)
                //kernelInput.Text = "Unique values: " + valueCount(generateHistogram(ref alow, ref ahigh));
                kernelInput.Text = "Foreground greyscale value: " + detectForeground(generateHistogram(ref alow, ref ahigh));

            toOutputBitmap();

            /*
            //==========================================================================================
            // TODO: include here your own code
            // example: create a negative image
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    Color pixelColor = Image[x, y];                         // Get the pixel color at coordinate (x,y)
                    Color updatedColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B); // Negative image
                    Image[x, y] = updatedColor;                             // Set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep();                              // Increment progress bar
                }
            }
            //==========================================================================================
            */
            
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
        
        void resetForApply()
        {
            if (InputImage1 == null) return;                                 // Get out if no input image
            if (OutputImage1 != null) OutputImage1.Dispose();                 // Reset output image
            OutputImage1 = new Bitmap(InputImage1.Size.Width, InputImage1.Size.Height); // Create new output image
            Image = new Color[InputImage1.Size.Width, InputImage1.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)
            newImage = new Color[InputImage1.Size.Width, InputImage1.Size.Height];

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
        }

        /// <summary>
        /// Generates an image in picturBox2 based on the color matrix newImage.
        /// </summary>
        void toOutputBitmap()
        {
            //Image = newImage;
            // Copy array to output Bitmap
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    //OutputImage1.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                  OutputImage1.SetPixel(x, y, newImage[x, y]);
                }
            }

            pictureBox2.Image = (Image)OutputImage1;                         // Display output image
            progressBar.Visible = false;                                    // Hide progress bar
        }

        /// <summary>
        /// Reads the input of textbox1 and returns a matrix generated from the input.
        /// </summary>
        /// <returns></returns>
        int[,] ParseMatrix()
        {
            try
            {
                // split the rows
                string input = kernelInput.Text;
                string[] rows = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                // split the columns and add to a 2D array

                int[,] matrix = new int[rows.Length, rows.Length]; //creer M x M matrix afhankelijk van ingevoerde values

                for (int i = 0; i < rows.Length; i++)
                {
                    // alle 3 de rijen parsen
                    int[] column = Array.ConvertAll(rows[i].Split(' '), int.Parse);

                    if (column.Length != rows.Length)
                    {
                        throw new Exception("Provide a square matrix, with equal number of rows and columns");
                    }
                    if (column.Length % 2 == 0)
                        throw new Exception("Provide a square matrix, with an odd number of columns and rows");


                    // deze kolom op de goede plek in de matrix zetten
                    for (int j = 0; j < rows.Length; j++)
                        matrix[i, j] = column[j];
                }

                return matrix;
                //de matrix is geparsed en de waardes zijn nu op te halen


            }
            catch (Exception e)
            {
                //DIT MOET EIGENLIJK MessageBox2 ZIJN KEEP THAT IN MIND PLS
                this.kernelInput.Text = e.Message;
                return null;
            }
        }

        /// <summary>
        /// Generates a histogram of the currently loaded image.
        /// </summary>
        /// <param name="alow"></param>
        /// <param name="ahigh"></param>
        /// <returns></returns>
        int[] generateHistogram(ref int alow, ref int ahigh)
        {
            int[] histogram = new int[256];     //histogram aanmaken, alow en ahigh initialiseren

            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    Color pixelColor = Image[x, y];                                 // Get the pixel color at coordinate (x,y)
                    int grey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;    // aanmaken grijswaarde op basis van RGB-values
                    Color updatedColor = Color.FromArgb(grey, grey, grey);          // toepassen grijswaarde

                    histogram[grey]++;                                              // histogram updaten

                    //kijken of er nieuwe alow/ahigh gevonden is
                    if (grey < alow)
                    {
                        alow = grey;
                    }
                    if (grey > ahigh)
                    {
                        ahigh = grey;
                    }

                    progressBar.PerformStep();                                      // Increment progress bar
                }
            }
            return histogram;
        }

        /// <summary>
        /// Returns the unique number of values in a histogram.
        /// </summary>
        /// <param name="histogram"></param>
        /// <returns></returns>
        int valueCount(int[] histogram)
        {
            int count = 0;
            for (int i = 0; i < histogram.Length; i++)
            {
                if (histogram[i] > 0) count++;
            }
            return count;
        }

        /// <summary>
        /// Checks if a number is binary (used to check the valuecount of a histogram for binary images).
        /// </summary>
        /// <returns></returns>
        bool isBinary(int input)
        {
            if (input == 2) return true;
            else return false;
        }

        /// <summary>
        /// Returns the foreground (smallest number of pixels) value of a binary image using its histogram as input.
        /// </summary>
        /// <param name="histogram"></param>
        /// <returns></returns>
        int detectForeground(int[] histogram)
        {
            if (!isBinary(valueCount(histogram))) return 256;

            int a = 0;
            int b = 0;
            for (int i = 0; i < histogram.Length; i++)
            {
                if (a == 0 && histogram[i] != 0) a = i;
                if (b == 0 && histogram[i] != 0 && i != a) b = i;
            }
            if (a < b) return a;
            else return b;
        }

        /// <summary>
        /// Takes an integer value and clamps it to either the minimum or maximum RGB-value (0-255).
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        int clamp(int i)
        {
            if (i < 0) return 0;
            else if (i > 255) return 255;
            else return i;
        }

        /// <summary>
        /// Dilates a binary image using the provided matrix as structuring element.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="matrix"></param>
        /// <param name="halfBoxSize"></param>
        void CalculateDilationBinary(int x, int y, int[,] matrix, int halfBoxSize)
        {
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    // every pixel that exists on the structuring element and is currently in the background gets transformed to the foreground
                    if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && Image[x + a, y + b].R == ahigh)
                    {
                        int newcolor = alow;
                        Color updatedColor = Color.FromArgb(newcolor, newcolor, newcolor);
                        newImage[x + a, y + b] = updatedColor;
                    }
                    // every pixel that doesn't meet these conditions retains its former color
                    else newImage[x, y] = Image[x, y];
                }
            }
        }

        /// <summary>
        /// Erodes a binary image using the provided matrix as a structuring element.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="matrix"></param>
        /// <param name="halfBoxSize"></param>
        void CalculateErosionBinary(int x, int y, int[,] matrix, int halfBoxSize)
        {
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    // if a pixel in the structuring element is detected that isn't in the foreground, 
                    // the hotspot gets transformed to the background and the function ends
                    if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && Image[x + a, y + b].R == ahigh)
                    {
                        int newcolor = ahigh;
                        Color updatedColor = Color.FromArgb(newcolor, newcolor, newcolor);
                        newImage[x, y] = updatedColor;
                        return;
                    }
                }
            }
            // if the surrounding pixels pass all checks of the structuring element, the hotspot can stay in the foreground
            newImage[x, y] = Image[x, y];
        }

        /// <summary>
        /// Dilates a greyscale image using the provided matrix as a structuring element.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="matrix"></param>
        /// <param name="halfBoxSize"></param>
        /// <returns></returns>
        int CalculateDilation(int x, int y, int[,] matrix, int halfBoxSize)
        {
            int newColor = 0;
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    // The maximum value of the structuring element added to the surrounding pixels is chosen and returned as the new greyscale value for the hotspot.
                    if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && (Image[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]) > newColor)
                    {
                        newColor = clamp(Image[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]);
                    }
                }
            }
            return newColor;
        }

        /// <summary>
        /// Erodes a greyscale image using the provided matrix as a structuring element.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="matrix"></param>
        /// <param name="halfBoxSize"></param>
        /// <returns></returns>
        int CalculateErosion(int x, int y, int[,] matrix, int halfBoxSize)
        {
            int newColor = int.MaxValue;
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    // The minimum value of the structuring element subtracted from the surrounding pixels is chosen and returned as the new greyscale value for the hotspot.
                    if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && (Image[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]) < newColor)
                    {
                        newColor = clamp(Image[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]);
                    }
                }
            }
            return newColor;
        }

        /// <summary>
        /// Apply an erosion or dilation filter to an input image using the matrix provided in textbox1 as a structuring element.
        /// </summary>
        /// <param name="isErosion"></param>
        void ApplyErosionDilationFilter(bool isErosion)
        {
            int[,] matrix = ParseMatrix();
            int newColor = 0;
            
            //check if the image is binary
            bool binary = isBinary(valueCount(generateHistogram(ref alow, ref ahigh)));

            if (matrix != null)
            {
                int boxsize = matrix.GetLength(0);                                           // length matrix
                int halfBoxSize = (boxsize - 1) / 2;                                        // help variable
                
                //loop through the image
                for (int x = halfBoxSize; x < InputImage1.Size.Width - halfBoxSize; x++)
                {
                    for (int y = halfBoxSize; y < InputImage1.Size.Height - halfBoxSize; y++)
                    {
                        // binary images: binary erosion/dilation
                        if (binary && Image[x, y].R == alow)
                        {
                            if (isErosion) CalculateErosionBinary(x, y, matrix, halfBoxSize);
                            else CalculateDilationBinary(x, y, matrix, halfBoxSize);
                        }
                        else newImage[x, y] = Image[x, y];
                        // greyscale images: greyscale erosion/dilation and apply new color
                        if (!binary)
                        {
                            if (isErosion) newColor = CalculateErosion(x, y, matrix, halfBoxSize);
                            else newColor = CalculateDilation(x, y, matrix, halfBoxSize);
                            Color updatedColor = Color.FromArgb(newColor, newColor, newColor);
                            newImage[x, y] = updatedColor;
                        }
                        progressBar.PerformStep();
                    }
                }
            }
        }

        /// <summary>
        /// Apply an opening or closing filter to an input image using the matrix provided in textbox1 as a structuring element.
        /// </summary>
        /// <param name="isOpening"></param>
        void ApplyOpeningClosingFilter(bool isOpening)
        {
            if (isOpening) ApplyErosionDilationFilter(true);
            else ApplyErosionDilationFilter(false);

            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    Image[x, y] = newImage[x, y];
                }
            }

            if (isOpening) ApplyErosionDilationFilter(false);
            else ApplyErosionDilationFilter(true);
        }
    }
}
