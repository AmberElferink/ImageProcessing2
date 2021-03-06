﻿using System;
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
        Color[,] Image1;
        Color[,] Image2;
        Color[,] newImage1;
        Color[,] newImage2;

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
                 String file = openImageDialog.FileName;                     // Get the file name

                if (imageNumber == 1)
                {
                    imageFileName1.Text = file;

                    if (InputImage1 != null) InputImage1.Dispose();               // Reset image
                    InputImage1 = new Bitmap(file);                              // Create new Bitmap from file
                    if (InputImage1.Size.Height <= 0 || InputImage1.Size.Width <= 0 ||
                        InputImage1.Size.Height > 512 || InputImage1.Size.Width > 512) // Dimension check
                        MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                    pictureBox1.Image = (Image)InputImage1;
                }
                else if (imageNumber == 2)
                {
                    imageFileName2.Text = file;

                    if (InputImage2 != null) InputImage2.Dispose();               // Reset image
                    InputImage2 = new Bitmap(file);                              // Create new Bitmap from file
                    if (InputImage2.Size.Height <= 0 || InputImage2.Size.Width <= 0 ||
                        InputImage2.Size.Height > 512 || InputImage2.Size.Width > 512) // Dimension check
                        MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                    pictureBox2.Image = (Image)InputImage2;
                }
            }
        }







        private void applyButton_Click(object sender, EventArgs e)
        {
            resetForApply();

            if (InputImage1 == null)
            {
                MessageBox2.Text = "Please Load an image first.";
                return;
            }
            if( forceBinary.Checked)
            {

            }
            if (BoundaryRadio.Checked)
            {
                Point[] boundary = TraceBoundary();
                kernelInput.Text = WritePointArr(boundary);
                BoundaryToOutput(boundary);
            }
            else
            {
                if (ErosionRadio.Checked)
                    ApplyErosionDilationFilter(true, false);
                else if (DilationRadio.Checked)
                    ApplyErosionDilationFilter(false, false);
                else if (OpeningRadio.Checked)
                    ApplyOpeningClosingFilter(true);
                else if (ClosingRadio.Checked)
                    ApplyOpeningClosingFilter(false);
                else if (ValueRadio.Checked)
                    kernelInput.Text = "Unique values: " + valueCount(generateHistogram(Image1));
                else if (MinMaxRadio.Checked)
                    ApplyErosionDilationFilter(true, true);
                else if (complementRadio.Checked)
                    //kernelInput.Text = detectBackground(generateHistogram(ref alow, ref ahigh)).ToString();
                    GenerateComplement();
                
                toOutputBitmap();

            }
        }







        private void ApplyThresholdFilter(Image image)
        {


            int thresholdColor = 0;
            int thresholdLimit = 125;      // threshold wordt afgelezen van trackbar
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    if (Image1[x, y].R < thresholdLimit)
                    {
                        thresholdColor = 0;
                    }
                    else
                    {
                        thresholdColor = 255;
                    }
                    Color updatedColor = Color.FromArgb(thresholdColor, thresholdColor, thresholdColor);
                    newImage1[x, y] = updatedColor;
                }
            }

            for (int x = 0; x < InputImage1.Width; x++)
                for (int y = 0; y < InputImage1.Height; y++)
                {
                        InputImage1.SetPixel(x, y, newImage1[x, y]);
                }
        }




        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage1 == null) return;                                // Get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                OutputImage1.Save(saveImageDialog.FileName);                 // Save the output image
        }



        
        void resetForApply()
        {
            if (InputImage1 == null) return;                                 // Get out if no input image
            if (OutputImage1 != null) OutputImage1.Dispose();                 // Reset output image
            OutputImage1 = new Bitmap(InputImage1.Size.Width, InputImage1.Size.Height); // Create new output image
            Image1 = new Color[InputImage1.Size.Width, InputImage1.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)
            newImage1 = new Color[InputImage1.Size.Width, InputImage1.Size.Height];

    
            if(pictureBox2.Image != null)
            {
                OutputImage2 = new Bitmap(InputImage2.Size.Width, InputImage2.Size.Height); // Create new output image
                Image2 = new Color[InputImage2.Size.Width, InputImage2.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)
                newImage2 = new Color[InputImage2.Size.Width, InputImage2.Size.Height];
            }

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
                    Image1[x, y] = InputImage1.GetPixel(x, y);                // Set pixel color in array at (x,y)
                }
            }


            // Copy input Bitmap to array            
            for (int x = 0; x < InputImage2.Size.Width; x++)
            {
                for (int y = 0; y < InputImage2.Size.Height; y++)
                {
                    Image2[x, y] = InputImage2.GetPixel(x, y);                // Set pixel color in array at (x,y)
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
                  OutputImage1.SetPixel(x, y, newImage1[x, y]);
                }
            }

            outputBox1.Image = (Image)OutputImage1;                         // Display output image
            progressBar.Visible = false;                                    // Hide progress bar

            if (InputImage2 != null)
            {
                //Image = newImage;
                // Copy array to output Bitmap
                for (int x = 0; x < InputImage2.Size.Width; x++)
                {
                    for (int y = 0; y < InputImage2.Size.Height; y++)
                    {
                        //OutputImage1.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                        OutputImage2.SetPixel(x, y, newImage2[x, y]);
                    }
                }

                outputBox2.Image = (Image)OutputImage2;                         // Display output image
            }
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
        int[] generateHistogram(Color[,] image)
        {
            int[] histogram = new int[256];     //histogram aanmaken, alow en ahigh initialiseren

            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    Color pixelColor = image[x, y];                                 // Get the pixel color at coordinate (x,y)
                    int grey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;    // aanmaken grijswaarde op basis van RGB-values
                    Color updatedColor = Color.FromArgb(grey, grey, grey);          // toepassen grijswaarde
                    histogram[grey]++;                                              // histogram updaten
                    progressBar.PerformStep();                                      // Increment progress bar
                }
            }
            return histogram;
        }





        /// <summary>
        /// Returns the unique number of values in a histogram.
        /// </summary>
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
        bool isBinary(int input)
        {
            if (input == 2) return true;
            else return false;
        }




        /// <summary>
        /// Returns the foreground (smallest number of pixels) value of a binary image using its histogram as input.
        /// </summary>
        int detectBackground(int[] histogram)
        {
            if (!isBinary(valueCount(histogram))) return 256;

            int a = 0;
            int b = 0;
            for (int i = 0; i < histogram.Length; i++)
            {
                if (a == 0 && histogram[i] != 0) a = i;
                if (b == 0 && histogram[i] != 0 && i != a) b = i;
            }
            if (a < b) return b;
            else return a;
        }



        /// <summary>
        /// Takes an integer value and clamps it to either the minimum or maximum RGB-value (0-255).
        /// </summary>
        int clamp(int i)
        {
            if (i < 0) return 0;
            else if (i > 255) return 255;
            else return i;
        }

        void GenerateComplement()
        {
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    int newColor = Image1[x, y].R;                         // Get the pixel color at coordinate (x,y)
                    newColor = 255 - newColor; // Negative image
                    newImage1[x, y] = Color.FromArgb(newColor, newColor, newColor);                             // Set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep();                              // Increment progress bar
                }
            }
        }






        /// <summary>
        /// Dilates a binary image using the provided matrix as structuring element.
        /// </summary>
        void CalculateDilationBinary(int x, int y, int[,] matrix, int halfBoxSize, bool isMinMax)
        {
            int newColor = 0;
            Color updatedColor = Color.FromArgb(newColor, newColor, newColor);
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    if (!isMinMax)
                    {
                        // every pixel that exists on the structuring element and is currently in the background gets transformed to the foreground
                        if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && Image1[x + a, y + b].R == 255)
                        {
                            newImage1[x + a, y + b] = updatedColor;
                        }
                        // every pixel that doesn't meet these conditions retains its former color
                        else newImage1[x, y] = updatedColor;
                    }
                    else
                    {
                        if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && (Image1[x + a, y + b].R == 255 || Image2[x + a, y + b].R == 255))
                        {
                            newImage2[x + a, y + b] = updatedColor;
                        }
                        else newImage2[x, y] = updatedColor;
                    }
                }
            }
        }




        /// <summary>
        /// Erodes a binary image using the provided matrix as a structuring element.
        /// </summary>
        void CalculateErosionBinary(int x, int y, int[,] matrix, int halfBoxSize, bool isMinMax)
        {
            int newcolor = 255;
            Color updatedColor = Color.FromArgb(newcolor, newcolor, newcolor);
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    if (!isMinMax)
                    {
                        // if a pixel in the structuring element is detected that isn't in the foreground, 
                        // the hotspot gets transformed to the background and the function ends
                        if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && Image1[x + a, y + b].R == 255)
                        {
                            newImage1[x, y] = updatedColor;
                            return;
                        }
                    }
                    else
                    {
                        if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && (Image1[x + a, y + b].R == 255 || Image2[x + a, y + b].R == 255))
                        {
                            newImage1[x, y] = updatedColor;
                            return;
                        }
                    }
                }
            }
            // if the surrounding pixels pass all checks of the structuring element, the hotspot can stay in the foreground
            newImage1[x, y] = Image1[x, y];
        }




        /// <summary>
        /// Dilates a greyscale image using the provided matrix as a structuring element.
        /// </summary>
        int CalculateDilation(int x, int y, int[,] matrix, int halfBoxSize, bool isMinMax)
        {
            int newColor = 0;
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    if (!isMinMax)
                    {
                        // The maximum value of the structuring element added to the surrounding pixels is chosen and returned as the new greyscale value for the hotspot.
                        if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && (Image1[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]) > newColor)
                        {
                            newColor = clamp(Image1[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]);
                        }
                    }
                    else
                    {
                        if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && ((Image1[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]) > newColor || (Image2[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]) > newColor))
                        {
                            if ((Image1[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]) > (Image2[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]))
                            {
                                newColor = clamp(Image1[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]);
                            }
                            else
                            {
                                newColor = clamp(Image2[x + a, y + b].R + matrix[a + halfBoxSize, b + halfBoxSize]);
                            }
                        }
                    }
                }
            }
            return newColor;
        }




        /// <summary>
        /// Erodes a greyscale image using the provided matrix as a structuring element.
        /// </summary>
        int CalculateErosion(int x, int y, int[,] matrix, int halfBoxSize, bool isMinMax)
        {
            int newColor = int.MaxValue;
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    if (!isMinMax)
                    {
                        // The minimum value of the structuring element subtracted from the surrounding pixels is chosen and returned as the new greyscale value for the hotspot.
                        if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && (Image1[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]) < newColor)
                        {
                            newColor = clamp(Image1[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]);
                        }
                    }
                    else
                    {
                        if (matrix[a + halfBoxSize, b + halfBoxSize] != -1 && ((Image1[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]) < newColor || (Image2[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]) < newColor))
                        {
                            if ((Image1[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]) < (Image2[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]))
                            {
                                newColor = clamp(Image1[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]);
                            }
                            else
                            {
                                newColor = clamp(Image2[x + a, y + b].R - matrix[a + halfBoxSize, b + halfBoxSize]);
                            }
                        }
                    }
                }
            }
            return newColor;
        }



        /// <summary>
        /// Apply an erosion or dilation filter to an input image using the matrix provided in textbox1 as a structuring element.
        /// </summary>
        void ApplyErosionDilationFilter(bool isErosion, bool isMinMax)
        {
            int[,] matrix = ParseMatrix();
            int newColor1 = 0;
            int newColor2 = 0;
            bool binary2 = false;
            
            //check if the image is binary
            bool binary = isBinary(valueCount(generateHistogram(Image1)));

            if (isMinMax)
            {
                if (InputImage1.Size.Width != InputImage2.Size.Width || InputImage1.Size.Height != InputImage2.Size.Height) return;
                else
                {
                    binary2 = isBinary(valueCount(generateHistogram(Image2)));
                }
            }

            if (matrix != null)
            {
                int boxsize = matrix.GetLength(0);                                           // length matrix
                int halfBoxSize = (boxsize - 1) / 2;                                        // help variable
                
                //loop through the image
                for (int x = halfBoxSize; x < InputImage1.Size.Width - halfBoxSize; x++)
                {
                    for (int y = halfBoxSize; y < InputImage1.Size.Height - halfBoxSize; y++)
                    {
                        if (!isMinMax)
                        {
                            // binary images: binary erosion/dilation
                            if (binary)
                            {
                                if (Image1[x, y].R == 0)
                                {
                                    if (isErosion) CalculateErosionBinary(x, y, matrix, halfBoxSize, false);
                                    else CalculateDilationBinary(x, y, matrix, halfBoxSize, false);
                                }
                                else
                                {
                                    newColor1 = 255;
                                    Color UpdatedColor = Color.FromArgb(newColor1, newColor1, newColor1);
                                    newImage1[x, y] = UpdatedColor;
                                }
                            }
                            // greyscale images: greyscale erosion/dilation and apply new color
                            else
                            {
                                if (isErosion) newColor1 = CalculateErosion(x, y, matrix, halfBoxSize, false);
                                else newColor1 = CalculateDilation(x, y, matrix, halfBoxSize, false);
                                Color updatedColor = Color.FromArgb(newColor1, newColor1, newColor1);
                                newImage1[x, y] = updatedColor;
                            }
                            progressBar.PerformStep();
                        }
                        else
                        {
                            if (binary && binary2)
                            {
                                if (Image1[x, y].R == 0 || Image2[x, y].R == 0) {
                                    CalculateErosionBinary(x, y, matrix, halfBoxSize, true);
                                    CalculateDilationBinary(x, y, matrix, halfBoxSize, true);
                                }
                                else
                                {
                                    newColor1 = 255;
                                    Color UpdatedColor = Color.FromArgb(newColor1, newColor1, newColor1);
                                    newImage1[x, y] = UpdatedColor;
                                    newImage2[x, y] = UpdatedColor;
                                }
                            }
                            else
                            {
                                newColor1 = CalculateErosion(x, y, matrix, halfBoxSize, true);
                                newColor2 = CalculateDilation(x, y, matrix, halfBoxSize, true);
                                Color updatedColor1 = Color.FromArgb(newColor1, newColor1, newColor1);
                                newImage1[x, y] = updatedColor1;
                                Color updatedColor2 = Color.FromArgb(newColor2, newColor2, newColor2);
                                newImage2[x, y] = updatedColor2;
                            }
                            progressBar.PerformStep();
                        }
                    }
                }
            }
        }





        /// <summary>
        /// Apply an opening or closing filter to an input image using the matrix provided in textbox1 as a structuring element.
        /// </summary>
        void ApplyOpeningClosingFilter(bool isOpening)
        {
            if (isOpening) ApplyErosionDilationFilter(true, false);
            else ApplyErosionDilationFilter(false, false);

            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    Image1[x, y] = newImage1[x, y];
                }
            }

            if (isOpening) ApplyErosionDilationFilter(false, false);
            else ApplyErosionDilationFilter(true, false);
        }








        // -------------------------------------------- TRACE BOUNDARY CODE --------------------------------------------

        Point[] TraceBoundary()
        {
            Color backGrC = Color.FromArgb(255, 0, 0, 0);
            //Color foreGrC = Color.FromArgb(255, 255, 255, 255);

            Color previousColor = backGrC;

            List<Point> returnValue = new List<Point>();

            for (int v = 0; v < InputImage1.Height; v++)
                for (int u = 0; u < InputImage1.Width; u++) //x moet 'snelst' doorlopen
                {
                    Color currentColor = Image1[u, v];
                    if (previousColor == backGrC && currentColor != backGrC)
                        returnValue = TransFgBg(backGrC, returnValue, u, v);
                }

            return returnValue.ToArray();
        }



        /// <summary>
        /// Handles the case that there is a transition from background to foreground in the image, and traces the figure found.
        /// </summary>
        /// <param name="backgrC">background color</param>
        List<Point> TransFgBg(Color backgrC, List<Point> ContourPixels, int u, int v)
        {

            if (isContourPix(backgrC, u, v))
            {
                if (Image1[u, v] != backgrC)
                {
                    ContourPixels.Add(new Point(u, v));

                    //This is N8 chain code, for N4 only consider the 4 pixels straight up, below, left and right
                    for (int x = -1; x <= 1; x++) //kijk van rechtsonder naar linksboven, dan loop je minder snel terug.
                        for (int y = -1; y <= 1; y++)
                        {
                            if (u + x >= 0 && v + y >= 0 && u + x < InputImage1.Width && v + y < InputImage1.Height)
                                if (!ContourPixels.Contains(new Point(u + x, v + y)))
                                {
                                    TransFgBg(backgrC, ContourPixels, u + x, v + y);
                                }
                        }
                }
            }
            //If all edge pixels are traced, return the complete list of contourpixels 
            return ContourPixels;
        }



        Boolean isContourPix(Color backgrC, int u, int v)
        {
            for (int x = -1; x <= 1; x++) //check if neighbouringpixels are background
                for (int y = -1; y <= 1; y++)
                {
                    if (u + x >= 0 && v + y >= 0 && u + x < InputImage1.Width && v + y < InputImage1.Height)
                        if (Image1[u + x, v + y] == backgrC)
                            return true;
                }

            return false;
        }



        String WritePointArr(Point[] points)
        {
            String output = "{";
            for (int i = 0; i < points.Length; i++)
            {
                output = output + "(" + points[i].X + "," + points[i].Y + "), ";
            }
            output += "}";
            return output;
        }



        void BoundaryToOutput(Point[] points)
        {
            for (int x = 0; x < InputImage1.Width; x++)
                for (int y = 0; y < InputImage1.Height; y++)
                {
                    if (points.Contains(new Point(x, y)))
                        OutputImage1.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
                    else
                        OutputImage1.SetPixel(x, y, Color.FromArgb(255, 255, 255, 255));
                }

            outputBox1.Image = (Image)OutputImage1;                         // Display output image
            progressBar.Visible = false;
        }

    }
}
