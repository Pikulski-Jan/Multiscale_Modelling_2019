using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CATest
{
    public partial class Form1 : Form
    {
        private static int height = 300;
        private static int width = 300;
        private int[,] canvas = new int[width, height];
        string bitmapString = null;

        Bitmap TestBitmap = new Bitmap(width, height);
        Bitmap BufferBitmap = new Bitmap(width, height);

        public Form1()
        {
            InitializeComponent();
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    TestBitmap.SetPixel(i, j, Color.White);
                }
            }

            pictureBox1.Image = TestBitmap;

            SetBeginningStage();
        }

        private void SetBeginningStage()
        {
            string grainNo = grainBox.Text;
            bool result;
            int a;
            result = int.TryParse(grainNo, out a);
            if (result)
            {
                Random rnd = new Random();
                //Console.WriteLine("Works");
                for (int i = 0; i < a; i++)
                {
                    int r = rnd.Next(50, 250);
                    int g = rnd.Next(50, 250);
                    int b = rnd.Next(50, 250);

                    do
                    {
                        int x = rnd.Next(width);
                        int y = rnd.Next(height);

                        if (TestBitmap.GetPixel(x, y).ToArgb().Equals(Color.White.ToArgb()))
                        {
                            TestBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                            result = false;
                        }
                    } while (result);
                    result = true;

                }

                pictureBox1.Image = TestBitmap;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        BufferBitmap.SetPixel(i, j, TestBitmap.GetPixel(i, j));
                    }
                }

            }
            else
            {
                //Console.WriteLine("Not Works");
            }
        }

        private void ExpandGrains()
        {
            bool wasChanged = true;

            while (wasChanged)
            {
                wasChanged = false;
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (TestBitmap.GetPixel(i, j).ToArgb().Equals(Color.White.ToArgb()))
                        {
                            wasChanged = true;
                            SetGrainState(i, j);
                        }
                    }
                }
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        TestBitmap.SetPixel(i, j, BufferBitmap.GetPixel(i, j));
                    }
                }
            }
        }

        private void SetGrainState(int x, int y)
        {
            var colorDictionary = new Dictionary<Color, int>();
            int currentCount = 0;
            if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
            {
                colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y - 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x - 1, y - 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y - 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x, y - 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y - 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x + 1, y - 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x - 1, y)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x + 1, y)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y + 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x - 1, y + 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y + 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x, y + 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y + 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x + 1, y + 1)] = currentCount + 1;
            }
            else if (x == 0)
            {
                if (y == 0)
                {
                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x + 1, y)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y + 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x, y + 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y + 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x + 1, y + 1)] = currentCount + 1;

                }
                else if (y == height - 1)
                {
                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x + 1, y)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y - 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x, y - 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y - 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x + 1, y - 1)] = currentCount + 1;
                }
                else
                {
                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y - 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x, y - 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y - 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x + 1, y - 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x + 1, y)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y + 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x + 1, y + 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x + 1, y)] = currentCount + 1;
                }
            }
            else if (x == width - 1)
            {
                if (y == 0)
                {
                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x - 1, y)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y + 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x - 1, y + 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y + 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x, y + 1)] = currentCount + 1;

                }
                else if (y == height - 1)
                {
                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x - 1, y)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y - 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x - 1, y - 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y - 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x, y - 1)] = currentCount + 1;

                }
                else
                {
                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y - 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x, y - 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y - 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x - 1, y - 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x - 1, y)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y + 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x - 1, y + 1)] = currentCount + 1;

                    colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y + 1), out currentCount);
                    colorDictionary[TestBitmap.GetPixel(x, y + 1)] = currentCount + 1;
                }
            }
            else if (y == 0)
            {
                colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x - 1, y)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y + 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x - 1, y + 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y + 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x, y + 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y + 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x + 1, y + 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x + 1, y)] = currentCount + 1;

            }
            else if (y == height - 1)
            {
                colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x - 1, y)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x - 1, y - 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x - 1, y - 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x, y - 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x, y - 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y - 1), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x + 1, y - 1)] = currentCount + 1;

                colorDictionary.TryGetValue(TestBitmap.GetPixel(x + 1, y), out currentCount);
                colorDictionary[TestBitmap.GetPixel(x + 1, y)] = currentCount + 1;
            }

            colorDictionary.Remove(Color.FromArgb(255, 255, 255));

            if (colorDictionary.Count > 0)
            {
                Color max = colorDictionary.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                BufferBitmap.SetPixel(x, y, max);
                Console.WriteLine(string.Format("Key = {0}", max));
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    TestBitmap.SetPixel(i, j, Color.White);
                }
            }

            pictureBox1.Image = TestBitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExpandGrains();
            pictureBox1.Image = TestBitmap;
        }

        private void saveBitMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Images|*.png;*.bmp;*.jpg";
            System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Bmp;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                pictureBox1.Image.Save(saveFileDialog1.FileName, format);
            }
        }

        private void importBitMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strfilename = openFileDialog1.FileName;
                TestBitmap = (Bitmap)Bitmap.FromFile(strfilename);
                pictureBox1.Image = TestBitmap;
            }
        }

        private void saveTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmapString = null;
                using(MemoryStream memoryStream = new MemoryStream())
                {
                    TestBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    byte[] bitmapBytes = memoryStream.GetBuffer();
                    bitmapString = Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks);
                    File.WriteAllText(saveFileDialog1.FileName, bitmapString);
                }
            }
        }

        private void importTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bitmapString = File.ReadAllText(openFileDialog1.FileName);
                byte[] bitmapBytes = Convert.FromBase64String(bitmapString);
                using (MemoryStream memoryStream = new MemoryStream(bitmapBytes))
                {
                    pictureBox1.Image = (Bitmap)Image.FromStream(memoryStream);
                }
            }

        }
    }
}
