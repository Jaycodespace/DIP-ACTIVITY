using ImageProcess2;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WebCamLib;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap? loaded, processed;
        Bitmap? LoadImage, LoadBackground;
        Bitmap? Result;
        Device[] mgaDevice;
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
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
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
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }
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

        private void mirrorVerticalToolStripMenuItem_Click(object sender, EventArgs e)
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
                    processed.SetPixel(x, loaded.Height - 1 - y, pixel);
                }
            }

            pictureBox2.Image = processed;
        }

        private void mirrorHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
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
                    processed.SetPixel(loaded.Width - 1 - x, y, pixel);
                }
            }
            pictureBox2.Image = processed;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mgaDevice = DeviceManager.GetAllDevices();
        }

        private void oNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mgaDevice[0].ShowWindow(pictureBox1);

        }

        private void oFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mgaDevice[0].Stop();
        }

        public static bool Conv3x3(Bitmap b, ConvMatrix m)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor) return false;

            Bitmap bSrc = (Bitmap)b.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            int stride2 = stride * 2;
            System.IntPtr Scan0 = bmData.Scan0;
            System.IntPtr SrcScan0 = bmSrc.Scan0;

            int topLeft = m.TopLeft;
            int topMid = m.TopMid;
            int topRight = m.TopRight;
            int midLeft = m.MidLeft;
            int midRight = m.MidRight;
            int pixel = m.Pixel;
            int bottomLeft = m.BottomLeft;
            int bottomMid = m.BottomMid;
            int bottomRight = m.BottomRight;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                byte* pSrc = (byte*)(void*)SrcScan0;

                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * topLeft) + (pSrc[5] * topMid) + (pSrc[8] * topRight) +
                            (pSrc[2 + stride] * midLeft) + (pSrc[5 + stride] * pixel) + (pSrc[8 + stride] * midRight) +
                            (pSrc[2 + stride2] * bottomLeft) + (pSrc[5 + stride2] * bottomMid) + (pSrc[8 + stride2] * bottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * topLeft) + (pSrc[4] * topMid) + (pSrc[7] * topRight) +
                            (pSrc[1 + stride] * midLeft) + (pSrc[4 + stride] * pixel) + (pSrc[7 + stride] * midRight) +
                            (pSrc[1 + stride2] * bottomLeft) + (pSrc[4 + stride2] * bottomMid) + (pSrc[7 + stride2] * bottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * topLeft) + (pSrc[3] * topMid) + (pSrc[6] * topRight) +
                            (pSrc[0 + stride] * midLeft) + (pSrc[3 + stride] * pixel) + (pSrc[6 + stride] * midRight) +
                            (pSrc[0 + stride2] * bottomLeft) + (pSrc[3 + stride2] * bottomMid) + (pSrc[6 + stride2] * bottomRight)) / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;

                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }
                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return true;
        }
        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }

            processed = new Bitmap(loaded);
            BitmapFilter.Sharpen(processed,11);

            pictureBox2.Image = processed;
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }

            processed = new Bitmap(loaded);
            BitmapFilter.GaussianBlur(processed,4);

            pictureBox2.Image = processed;
        }

        private void edgeEnhanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }
            byte threshold = 20;
            processed = new Bitmap(loaded);
            BitmapFilter.EdgeEnhance(processed,threshold);

            pictureBox2.Image = processed;
        }

        private void edgeDetectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }
            
            processed = new Bitmap(loaded);
            BitmapFilter.EdgeDetectQuick(processed);

            pictureBox2.Image = processed;
        }

        private void embossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null)
            {
                MessageBox.Show("No image is loaded. Please load an image first.");
                return;
            }
            
            processed = new Bitmap(loaded);
            BitmapFilter.EmbossLossy(processed);

            pictureBox2.Image = processed;
        }


    }
}

