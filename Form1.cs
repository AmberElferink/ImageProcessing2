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

            NoAutoContrHistogram(ref alow, ref ahigh);

            // Copy array to output Bitmap
            for (int x = 0; x < InputImage1.Size.Width; x++)
            {
                for (int y = 0; y < InputImage1.Size.Height; y++)
                {
                    OutputImage1.SetPixel(x, y, Image[x, y]);               // Set the pixel color at coordinate (x,y)
                }
            }
            
            pictureBox2.Image = (Image)OutputImage1;                         // Display output image
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

        int NoAutoContrHistogram(ref int alow, ref int ahigh)
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
            return valueCount;
        }

        bool isBinary()
        {
            if (NoAutoContrHistogram(ref alow, ref ahigh) == 2) return true;
            else return false;
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

        void CalculateErosionBinary(int x, int y, int[,] matrix, int halfBoxSize)
        {
            if (Image[x, y].R == ahigh)
            {
                for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
                {
                    for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                    {
                        int newcolor = ahigh * matrix[a + halfBoxSize, b + halfBoxSize];
                        Color updatedColor = Color.FromArgb(newcolor, newcolor, newcolor);
                        newImage[x + a, y + b] = updatedColor;
                    }
                }
            }
        }

        void CalculateDilationBinary(int x, int y, int[,] matrix, int halfBoxSize)
        {
            if (Image[x, y].R == ahigh)
            {
                for (int a = (halfBoxSize * -1); a <= halfBoxSize; a++)
                {
                    for (int b = (halfBoxSize * -1); b <= halfBoxSize; b++)
                    {
                        if (Image[x + a, y + b].R == ahigh)
                        {
                            int newcolor = ahigh - (ahigh * matrix[a + halfBoxSize, b + halfBoxSize]);
                            Color updatedColor = Color.FromArgb(newcolor, newcolor, newcolor);
                            newImage[x + a, y + b] = updatedColor;
                        }
                    }
                }
            }
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

        void ApplyErosionDilationFilter(bool isErosion)
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
                        if (isBinary())
                        {
                            if (isErosion) CalculateErosionBinary(x, y, matrix, halfBoxSize);
                            else CalculateDilationBinary(x, y, matrix, halfBoxSize);
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
