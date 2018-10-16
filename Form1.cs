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
        Color[,] Image;
        Color[,] newImage;

        int alow = 0;
        int ahigh = 0;
        bool isBinary = false;

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
            resetForApply();

            if (InputImage1 == null)
            {
                MessageBox2.Text = "Please Load an image first.";
                return;
            }
            if (BoundaryRadio.Checked)
            {
                Point[] boundary = TraceBoundary();
                kernelInput.Text = WritePointArr(boundary);
                BoundaryToOutput(boundary);
            }



            /*
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
            */

            NoAutoContrHistogram();

   /*         // Copy array to output Bitmap
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    OutputImage1.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                }
            }
            
            outputBox1.Image = (Image)OutputImage1;                         // Display output image
            progressBar.Visible = false;           */                         // Hide progress bar
        }


        String WritePointArr(Point[] points)
        {
            String output = "{";
            for( int i = 0; i < points.Length; i++)
            {
                output = output + "(" + points[i].X + "," + points[i].Y + "), ";
            }
            output += "}";
            return output;
        }


        void BoundaryToOutput(Point[] points)
        {
            for(int x = 0; x < InputImage1.Width; x++)
                for (int y = 0; y < InputImage1.Height; y++)
                {
                    if (points.Contains(new Point(x, y)))
                        OutputImage1.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
                    else
                        OutputImage1.SetPixel(x, y, Color.FromArgb(255, 255, 255, 255));
                }

            outputBox1.Image = (Image) OutputImage1;                         // Display output image
            progressBar.Visible = false;             
      
        }



        Point[] TraceBoundary()
        {
            Color backGrC = Color.FromArgb(255, 0, 0, 0);
            //Color foreGrC = Color.FromArgb(255, 255, 255, 255);

            Color previousColor = backGrC;

            List<Point> returnValue = new List<Point>();

             for( int v = 0; v < InputImage1.Height; v++)
                for (int u = 0; u < InputImage1.Width; u++) //x moet 'snelst' doorlopen
                {
                        Color currentColor = Image[u, v];
                        if (previousColor == backGrC && currentColor != backGrC)
                           returnValue = TransFgBg(backGrC, returnValue, u, v);
                }
           
                return returnValue.ToArray();
        }



        /// <summary>
        /// Handles the case that there is a transition from background to foreground in the image, and traces the figure found.
        /// </summary>
        /// <param name="backgrC">background color</param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        List<Point> TransFgBg(Color backgrC, List<Point> ContourPixels, int u, int v)
        {

            if (isContourPix(backgrC, u, v))
            {
                if (Image[u, v] != backgrC)
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
            for(int x = -1; x <= 1; x++ ) //check if neighbouringpixels are background
                for (int y = -1; y <= 1; y++)
                {
                    if(u+x >= 0 && v+y >= 0 && u+x < InputImage1.Width && v+y < InputImage1.Height)
                    if (Image[u + x, v + y] == backgrC)
                        return true;
                }

            return false;
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

        void toOutputBitmap()
        {
            Image = newImage;
            // Copy array to output Bitmap
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    OutputImage1.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                    //OutputImage.SetPixel(x, y, newImage[x, y]);
                }
            }

            pictureBox2.Image = (Image)OutputImage1;                         // Display output image
            progressBar.Visible = false;                                    // Hide progress bar
        }

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

        void NoAutoContrHistogram()
        {
            int[] histogram = new int[256];     //histogram aanmaken, alow en ahigh initialiseren
            int valueCount = 0;


            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    Color pixelColor = Image[x, y];                                 // Get the pixel color at coordinate (x,y)
                    int grey = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;    // aanmaken grijswaarde op basis van RGB-values
                    Color updatedColor = Color.FromArgb(grey, grey, grey);          // toepassen grijswaarde

                    histogram[grey]++;                                              // histogram updaten
                    progressBar.PerformStep();                                      // Increment progress bar
                }
            }

            /*
            Console.WriteLine("Histogram:");                                        // histogram wordt geprint
            for (int a = 0; a < 256; a++)
            {
                Console.WriteLine(a + ": " + histogram[a]);
            }
            */


            // LATER NOG NAAR ALOW/AHIGH KIJKEN VOOR BINAIRE IMAGES DIE NIET 0/255 ZIJN
            for (int i = 0; i < histogram.Length; i++)
            {
                if (histogram[i] > 0) valueCount++;
            }
            if (valueCount == 2)
            {
                isBinary = true;
            }
        }

        int CalculateNewColor(int x, int y, int[,] matrix, int halfBoxSize, bool divideByTotal = true)
        {
            int linearColor = 0;
            int matrixTotal = 0;                // totale waarde van alle weights van de matrix bij elkaar opgeteld
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                    linearColor = linearColor + (Image[x + a, y + b].R * matrix[a + halfBoxSize, b + halfBoxSize]);
                    // weight van filter wordt per kernel pixel toegepast op image pixel
                    matrixTotal = matrixTotal + matrix[a + halfBoxSize, b + halfBoxSize];
                    // weight wordt opgeteld bij totaalsom van weights
                }
            }
            if (divideByTotal == true) // Voor Edgestrength moet niet door het totaal gedeeld, dus kan hij uitgezet worden.
                linearColor = linearColor / matrixTotal;

            return linearColor;
        }

        int CalculateErosionBinary(int x, int y, int[,] matrix, int halfBoxSize)
        {
            int newColor = 0;
            for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
            {
                for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                {
                }
            }
                    return 0;
        }

        int CalculateDilationBinary(int x, int y, int[,] matrix, int halfBoxSize)
        {
            int newColor = 0;
            return 0;
        }

        int CalculateErosion(int x, int y, int[,] matrix, int halfBoxSize)
        {
            int newColor = 0;
            return 0;
        }

        int CalculateDilation(int x, int y, int[,] matrix, int halfBoxSize)
        {
            int newColor = 0;
            return 0;
        }

        void ApplyErosionDilationFilter(bool isErosion, bool isBinary)
        {
            int[,] matrix = ParseMatrix();
            int newColor = 0;
            if (matrix != null)
            {
                // linear boxfilter
                int boxsize = matrix.GetLength(0);                                           // lengte matrix wordt uitgelezen
                int halfBoxSize = (boxsize - 1) / 2;                                        // hulpvariabele voor berekeningen

                //loop over de image heen
                for (int x = halfBoxSize; x < InputImage1.Size.Width - halfBoxSize; x++)
                {
                    for (int y = halfBoxSize; y < InputImage1.Size.Height - halfBoxSize; y++)
                    {
                        if (isBinary)
                        {
                            if (isErosion) newColor = CalculateErosionBinary(x, y, matrix, halfBoxSize);
                            else newColor = CalculateDilationBinary(x, y, matrix, halfBoxSize);
                        }
                        else
                        {
                            if (isErosion) newColor = CalculateErosion(x, y, matrix, halfBoxSize);
                            else newColor = CalculateDilation(x, y, matrix, halfBoxSize);
                        }
                        //bekijk voor elke pixel op de image wat de nieuwe kleur moet worden
                        Color updatedColor = Color.FromArgb(newColor, newColor, newColor);
                        newImage[x, y] = updatedColor;
                        progressBar.PerformStep();
                    }
                }
            }
            toOutputBitmap();
        }   
    }
}
