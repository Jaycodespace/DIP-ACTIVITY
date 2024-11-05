using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap? loaded, processed;
        Bitmap? LoadImage, LoadBackground;
        Bitmap? Result;
        public Form1()
        {
            InitializeComponent();
        }

        private void oPENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogForPictureBox1 = new OpenFileDialog();
            if (openFileDialogForPictureBox1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    loaded = new Bitmap(openFileDialogForPictureBox1.FileName);
                    pictureBox1.Image = loaded;
                }
                catch
                {
                    MessageBox.Show("An error occurred while loading the image.");
                }
            }
        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }

            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixel);
                }
            }

            pictureBox2.Image = processed;
        }

        private void sAVEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();

        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (processed == null)
            {
                MessageBox.Show("Might be null");
                return;
            }
            processed.Save(saveFileDialog1.FileName);
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }

            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int average;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    average = (pixel.R + pixel.G + pixel.B) / 3;
                    Color gray = Color.FromArgb(average, average, average);
                    processed.SetPixel(x, y, gray);
                }
            }

            pictureBox2.Image = processed;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }

            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);
                    Color invert = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    processed.SetPixel(x, y, invert);
                }
            }

            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {

            processed = new Bitmap(256, 800);
            BasicDIP.Histogram(ref loaded, ref processed);
            pictureBox2.Image = processed;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }

            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int tr, tg, tb;

            for (int x = 0; x < loaded.Width; x++)
            {
                for (int y = 0; y < loaded.Height; y++)
                {
                    pixel = loaded.GetPixel(x, y);


                    tr = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    tg = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    tb = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);


                    tr = tr > 255 ? 255 : tr;
                    tg = tg > 255 ? 255 : tg;
                    tb = tb > 255 ? 255 : tb;

                    processed.SetPixel(x, y, Color.FromArgb(tr, tg, tb));
                }
            }

            pictureBox2.Image = processed;
        }

        private void subtractToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialogForPictureBox3 = new OpenFileDialog();
            if (openFileDialogForPictureBox3.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LoadImage = new Bitmap(openFileDialogForPictureBox3.FileName);

                    Bitmap processedWithGreenScreen = new Bitmap(LoadImage.Width, LoadImage.Height);
                    Color greenColor = Color.FromArgb(0, 255, 0); 
                    Color pixel;
                    Color backgroundColor = LoadImage.GetPixel(0, 0); 
                    int threshold = 30; 

                    for (int x = 0; x < LoadImage.Width; x++)
                    {
                        for (int y = 0; y < LoadImage.Height; y++)
                        {
                            pixel = LoadImage.GetPixel(x, y);
                            int difference = Math.Abs(pixel.R - backgroundColor.R) +
                                             Math.Abs(pixel.G - backgroundColor.G) +
                                             Math.Abs(pixel.B - backgroundColor.B);

                            if (difference < threshold)
                            {
                                
                                processedWithGreenScreen.SetPixel(x, y, greenColor);
                            }
                            else
                            {
                                
                                processedWithGreenScreen.SetPixel(x, y, pixel);
                            }
                        }
                    }

                    pictureBox3.Image = processedWithGreenScreen;
                }
                catch
                {
                    MessageBox.Show("An error occurred while loading the image.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialogForPictureBox4 = new OpenFileDialog();
            if (openFileDialogForPictureBox4.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LoadBackground = new Bitmap(openFileDialogForPictureBox4.FileName);
                    pictureBox4.Image = LoadBackground;
                }
                catch
                {
                    MessageBox.Show("An error occurred while loading the image.");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (pictureBox3.Image == null || LoadBackground == null)
            {
                MessageBox.Show("Both images need to be loaded.");
                return;
            }

            Bitmap processedWithGreenScreen = (Bitmap)pictureBox3.Image;
            if (processedWithGreenScreen.Width != LoadBackground.Width || processedWithGreenScreen.Height != LoadBackground.Height)
            {
                MessageBox.Show("Images must be of the same dimensions.");
                return;
            }

            Bitmap result = new Bitmap(processedWithGreenScreen.Width, processedWithGreenScreen.Height);
            Color pixelImage, pixelBackground;
            int threshold = 30;  

            for (int x = 0; x < processedWithGreenScreen.Width; x++)
            {
                for (int y = 0; y < processedWithGreenScreen.Height; y++)
                {
                    pixelImage = processedWithGreenScreen.GetPixel(x, y);
                    pixelBackground = LoadBackground.GetPixel(x, y);

                    
                    int greenDifference = Math.Abs(pixelImage.G - 255);
                    int redDifference = Math.Abs(pixelImage.R - 0);     
                    int blueDifference = Math.Abs(pixelImage.B - 0);    

                    if (greenDifference < threshold && redDifference < threshold && blueDifference < threshold)
                    {
                        
                        result.SetPixel(x, y, pixelBackground);
                    }
                    else
                    {
                      
                        result.SetPixel(x, y, pixelImage);
                    }
                }
            }

            pictureBox5.Image = result;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}

