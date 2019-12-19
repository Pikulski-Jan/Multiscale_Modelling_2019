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
        private CAPixel[,] canvas = new CAPixel[width, height];
        private CAPixel[,] buff_canvas = new CAPixel[width, height];
        string bitmapString = null;
        bool first = true;
        bool regrow = false;
        bool wasChanged = true;
        bool showEnergy = false;
        int borderCount = 0;
        int index = 0;
        Dictionary<Color, int> colorDictionary2 = new Dictionary<Color, int>();
        private readonly Color selectionColor = Color.FromArgb(255, 0, 0);
        List<Color> colorList = new List<Color>();
        List<Point> points = new List<Point>();

        Random rnd2 = new Random();

        Bitmap TestBitmap = new Bitmap(width, height);
        Bitmap EnergyBitmap = new Bitmap(width, height);


        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            Console.WriteLine(regrow);
            if (!regrow)
            {
                if (first)
                {
                    if (methodBox.Text != "Monte Carlo")
                    {
                        for (int i = 0; i < width; i++)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                canvas[i, j] = new CAPixel(i, j);
                                buff_canvas[i, j] = new CAPixel(i, j);
                                TestBitmap.SetPixel(i, j, canvas[i, j].GetColor());
                            }
                        }
                    }
                    else
                    {
                        colorList.Clear();
                        for (int i = 0; i < width; i++)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                canvas[i, j] = new CAPixel(i, j);
                                buff_canvas[i, j] = new CAPixel(i, j);
                                TestBitmap.SetPixel(i, j, canvas[i, j].GetColor());
                            }
                        }

                        int r, g, b;
                        for(int i = 0; i < int.Parse(grainBox.Text); i++)
                        {
                            r = rnd2.Next(50, 250);
                            g = rnd2.Next(50, 250);
                            b = rnd2.Next(50, 250);
                            colorList.Add(Color.FromArgb(r, g, b));
                        }
                    }

                    pictureBox1.Image = TestBitmap;
                    first = false;
                    Console.WriteLine(colorList.Count());

                }
                else
                {
                    int r, g, b;
                    for (int i = 0; i < int.Parse(grainBox.Text); i++)
                    {
                        r = rnd2.Next(50, 250);
                        g = rnd2.Next(50, 250);
                        b = rnd2.Next(50, 250);
                        colorList.Add(Color.FromArgb(r, g, b));
                    }
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (!canvas[i, j].isSelected)
                            {
                                canvas[i, j] = new CAPixel(i, j);
                                buff_canvas[i, j] = new CAPixel(i, j);
                                TestBitmap.SetPixel(i, j, canvas[i, j].GetColor());
                            }
                        }
                    }
                }
            }

            wasChanged = true;
            SetBeginningStage();
            setBitmapFromCanvas();

        }

        private void SetBeginningStage()
        {
            string grainNo = grainBox.Text;
            bool result;
            int a;
            result = int.TryParse(grainNo, out a);
            if (result)
            {
                if (methodBox.Text != "Monte Carlo")
                {
                    if (a > (height * width) - Convert.ToInt32(numericUpDownIncl.Value))
                    {
                        label4.Text = "Too many nucleons for selected grid size";
                        return;
                    }

                    Random rnd = new Random();

                    int r;
                    int g;
                    int b;

                    for (int i = 0; i < a; i++)
                    {
                        r = rnd.Next(50, 251);
                        g = rnd.Next(50, 251);
                        b = rnd.Next(50, 251);

                        do
                        {
                            int x = rnd.Next(width);
                            int y = rnd.Next(height);

                            if (canvas[x, y].GetColor().ToArgb().Equals(Color.White.ToArgb()))
                            {
                                canvas[x, y].SetColor(Color.FromArgb(r, g, b));
                                string buffer = r.ToString() + g.ToString() + b.ToString();
                                canvas[x, y].SetID(Int32.Parse(buffer));
                                result = false;
                            }
                        } while (result);
                        result = true;

                    }

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            buff_canvas[i, j].SetColor(canvas[i, j].GetColor());
                            buff_canvas[i, j].SetID(canvas[i, j].GetID());
                        }
                    }

                    if ((int)numericUpDownIncl.Value > 0)
                    {
                        if (!inclusionCheckbox.Checked)
                        {
                            if (!(inclustionType.Text == "square" || inclustionType.Text == "circular"))
                            {
                                label4.Text = "No type of inclusion selected";
                            }
                            else
                            {
                                if (numericUpDownIncl.Value == 0 || numericUpDownSize.Value == 0 || numericUpDownIncl.Value > (height * width) - a)
                                {
                                    label4.Text = "Too many inclusions for selected size of microstructure";
                                }
                                else
                                {
                                    Console.WriteLine("Set Inclusions");
                                    DrawInclusions(inclustionType.Text, false, (int)numericUpDownSize.Value, (int)numericUpDownIncl.Value);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Color color;
                    string buffer;
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (!canvas[i, j].isSelected)
                            {
                                color = colorList[rnd2.Next(colorList.Count)];
                                buff_canvas[i, j].SetColor(color);
                                buffer = color.R.ToString() + color.G.ToString() + color.B.ToString();
                                buff_canvas[i, j].SetID(Int32.Parse(buffer));
                                canvas[i, j].SetColor(color);
                                canvas[i, j].SetID(Int32.Parse(buffer));
                            }
                        }
                    }
                    setBitmapFromCanvas();
                }
            }
            else
            {
                Console.WriteLine("Not Works");
                if (false)
                {
                    Moore3(0, 0);
                }
            }
        }

        private void ExpandGrains()
        {
            Console.WriteLine("Start Expanding");

            if (methodBox.Text != "Monte Carlo")
            {
                while (wasChanged)
                {
                    wasChanged = false;
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (canvas[i, j].GetColor().ToArgb().Equals(Color.White.ToArgb()) && canvas[i, j].GetIsBorder() == false)
                            {
                                SetGrainState(i, j);
                            }
                        }
                    }
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            canvas[i, j].CopyData(buff_canvas[i, j].GetID(), buff_canvas[i, j].GetColor(), buff_canvas[i, j].GetIsBorder());
                        }
                    }
                }
            }
            else
            {
                int prevEnergy = 0;
                int nextEnergy = 0;
                bool isBorder = false;
                Color changeColor;
                List<Color> keyList;
                for (int a = 0; a < width; a++)
                {
                    for (int b = 0; b < height; b++)
                    {
                        if (canvas[a, b].isSelected == false)
                            points.Add(new Point(a, b));
                    }
                }
                for (int k = 0; k < 80; k++)
                {
                    
                    points = points.OrderBy(a => Guid.NewGuid()).ToList();
                    foreach (Point p in points)
                    {
                        prevEnergy = MonteCarloMoore2(p.X, p.Y, buff_canvas[p.X, p.Y].GetColor());

                        colorDictionary2.Remove(buff_canvas[p.X, p.Y].GetColor());
                        colorDictionary2.Remove(Color.FromArgb(255, 0, 0));

                        if (colorDictionary2.Count > 0)
                        {
                            keyList = new List<Color>(colorDictionary2.Keys);
                            changeColor = keyList[rnd2.Next(0, keyList.Count)];
                            nextEnergy = MonteCarloMoore2(p.X, p.Y, changeColor);
                            if (nextEnergy < prevEnergy)
                            {
                                if (nextEnergy > 0)
                                    isBorder = true;
                                buff_canvas[p.X, p.Y].setMCGrain(changeColor, isBorder);
                            }
                        }

                    }
                    //points.Clear();
                }
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        canvas[i, j].CopyData(buff_canvas[i, j].GetID(), buff_canvas[i, j].GetColor(), buff_canvas[i, j].GetIsBorder());
                    }
                }
                    setBitmapFromCanvas();
            }
            //DistributeEnergy();
        }

        private void SetGrainState(int x, int y)
        {
            Moore1(x, y);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                        TestBitmap.SetPixel(i, j, Color.White);
                        canvas[i, j].ResetPixel();
                        buff_canvas[i, j].ResetPixel();
                }
            }

            first = true;
            pictureBox1.Image = TestBitmap;
            label4.Text = "";
            borderCount = 0;
            borderLabel.Text = "";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExpandGrains();
            setBitmapFromCanvas();
            label4.Text = "";
            if ((int)numericUpDownIncl.Value > 0)
            {
                int a = 0;
                int.TryParse(grainBox.Text, out a);
                if (inclusionCheckbox.Checked)
                {
                    if (!(inclustionType.Text == "square" || inclustionType.Text == "circular"))
                    {
                        label4.Text = "No type of inclusion selected";
                    }
                    else
                    {
                        if (numericUpDownIncl.Value == 0 || numericUpDownSize.Value == 0 || numericUpDownIncl.Value > (height * width) - a)
                        {
                            label4.Text = "Too many inclusions for selected size of microstructure";
                        }
                        else
                        {
                            Console.WriteLine("Set Inclusions");
                            DrawInclusions(inclustionType.Text, true, (int)numericUpDownSize.Value, (int)numericUpDownIncl.Value);
                        }
                    }
                }
                setBitmapFromCanvas();
            }
            
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

        private void button3_Click(object sender, EventArgs e)
        {
            width = Convert.ToInt32(numericUpDownX.Value);
            height = Convert.ToInt32(numericUpDownY.Value);
            Console.WriteLine(width);
            Console.WriteLine(height);
            Size size = new Size(width, height);
            pictureBox1.Size = size;
            TestBitmap = new Bitmap(width, height);
            label4.Text = "";
            canvas = new CAPixel[width, height];
            buff_canvas = new CAPixel[width, height];
        }

        private void DrawInclusions(string type, bool isChecked, int size, int n)
        {
            Random rnd = new Random();

            bool result = true;
            int x = 0;
            int y = 0;

            while (n != 0)
            {
                
                if (!isChecked)
                {
                    if (type == "circular")
                    {
                        do
                        {
                            x = rnd.Next(width);
                            y = rnd.Next(height);
                            if (canvas[x, y].GetColor().ToArgb().Equals(Color.White.ToArgb()))
                            {
                                Console.WriteLine("set circle");
                                setCircleInclusion(x, y, size);
                                n--;
                                result = false;
                            }
                        } while (result);
                        result = true;
                    }
                    else
                    {
                        do
                        {
                            x = rnd.Next(width);
                            y = rnd.Next(height);
                            if (canvas[x, y].GetColor().ToArgb().Equals(Color.White.ToArgb()))
                            {
                                Console.WriteLine("set square");
                                setSquareInclusion(x, y, size);
                                n--;
                                result = false;
                            }
                        } while (result);
                        result = true;
                    }
                }
                else
                {
                    if (type == "circular")
                    {
                        do
                        {
                            x = rnd.Next(width);
                            y = rnd.Next(height);
                            if (canvas[x, y].GetIsBorder())
                            {
                                setCircleInclusion(x, y, size);
                                n--;
                                result = false;
                            }
                        } while (result);
                        result = true;
                    }
                    else
                    {
                        do
                        {
                            x = rnd.Next(width);
                            y = rnd.Next(height);
                            if (canvas[x, y].GetIsBorder())
                            {
                                setSquareInclusion(x, y, size);
                                n--;
                                result = false;
                            }
                        } while (result);
                        result = true;
                    }
                }
            }

        }
        private void setSquareInclusion(int x, int y, int size)
        {
            int halfSize = size / 2;
            for(int i = x - halfSize; i < x + halfSize; i++)
            {
                for (int j = y - halfSize; j < y + halfSize; j++)
                {
                    if(i >= 0 && i <= width - 1 && j >= 0 && j <= height - 1)
                    {
                        //TestBitmap.SetPixel(i, j, Color.Black);
                        canvas[i, j].SetColor(Color.Black);
                        //buff_canvas[i, j].SetColor(Color.Black);
                    }
                }
            }
        }

        private void setCircleInclusion(int x, int y, int size)
        {
            for (int i = x - size; i < x + size; i++)
            {
                for (int j = y - size; j < y + size; j++)
                {
                    if (i >= 0 && i <= width - 1 && j >= 0 && j <= height - 1)
                    {
                        if (((x - i) * (x - i) + (y - j) * (y - j)) < size * size)
                        {
                            //TestBitmap.SetPixel(i, j, Color.Black);
                            canvas[i, j].SetColor(Color.Black);
                            //buff_canvas[i, j].SetColor(Color.Black);
                        }
                    }
                }
            }
        }

        private void setBitmapFromCanvas()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    buff_canvas[i, j].SetColor(canvas[i, j].GetColor());
                    TestBitmap.SetPixel(i, j, canvas[i, j].GetColor());
                }
            }
            pictureBox1.Image = TestBitmap;
        }

        private void setEnergyBitmapFromCanvas()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    buff_canvas[i, j].SetColor(canvas[i, j].GetColor());
                    EnergyBitmap.SetPixel(i, j, canvas[i, j].GetColor());
                }
            }
            pictureBox1.Image = EnergyBitmap;
        }

        private void Moore4(int x, int y)
        {
            //
            
            int probability = rnd2.Next(0, 100);

            if (probability < (int)numericUpDownProb.Value)
            {

                var colorDictionary = new Dictionary<Color, int>();
                int currentCount = 0;

                if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
                {
                    colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                }
                else if (x == 0)
                {
                    if (y == 0)
                    {

                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;

                    }
                    else if (y == height - 1)
                    {

                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                    }
                    else
                    {

                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                    }
                }
                else if (x == width - 1)
                {
                    if (y == 0)
                    {
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                    }
                    else if (y == height - 1)
                    {
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                    }
                    else
                    {
                        //
                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                    }
                }
                else if (y == 0)
                {
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                }
                else if (y == height - 1)
                {
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;
                }
                colorDictionary.Remove(Color.FromArgb(255, 255, 255));
                colorDictionary.Remove(Color.FromArgb(255, 0, 0));
                colorDictionary.Remove(Color.White);
                bool border = colorDictionary.Count > 1 ? true : false;
                //buff_canvas[x, y].energy = colorDictionary.Count > 1 ? 5.0f : 2.0f;
                colorDictionary.Remove(Color.Black);
                colorDictionary.Remove(Color.FromArgb(0, 0, 0));

                if (colorDictionary.Count > 0)
                {
                    Color max = colorDictionary.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                    int count = colorDictionary[max];

                    buff_canvas[x, y].setGrain(max, border);
                    //regrowContinue = false;
                    wasChanged = true;
                }
            }
        }

        private void Moore3(int x, int y)
        {
            var colorDictionary = new Dictionary<Color, int>();
            int currentCount = 0;

            if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
            {
                colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
            }
            else if (x == 0)
            {
                Moore4(x, y);
                return;
            }
            else if (x == width - 1)
            {
                Moore4(x, y);
                return;
            }
            else if (y == 0)
            {
                Moore4(x, y);
                return;

            }
            else if (y == height - 1)
            {
                Moore4(x, y);
                return;
            }
            colorDictionary.Remove(Color.FromArgb(255, 255, 255));
            colorDictionary.Remove(Color.FromArgb(255, 0, 0));
            bool border = colorDictionary.Count > 1 ? true : false;
            //buff_canvas[x, y].energy = colorDictionary.Count > 1 ? 5.0f : 2.0f;
            colorDictionary.Remove(Color.Black);

            if (colorDictionary.Count > 0)
            {
                Color max = colorDictionary.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                int count = colorDictionary[max];

                if (count == 3 || count == 4)
                    buff_canvas[x, y].setGrain(max, border);
                else
                    Moore4(x, y);
            }
        }

        private void Moore2(int x, int y)
        {
            var colorDictionary = new Dictionary<Color, int>();
            int currentCount = 0;

            if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
            {
                colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
            }
            else if (x == 0)
            {
                if (y == 0)
                {

                    Moore4(x, y);
                    return;

                }
                else if (y == height - 1)
                {

                    Moore4(x, y);
                    return;
                }
                else
                {

                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;
                }
            }
            else if (x == width - 1)
            {
                if (y == 0)
                {
                    Moore4(x, y);
                    return;

                }
                else if (y == height - 1)
                {
                    Moore4(x, y);
                    return;
                }
                else
                {
                    //
                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                }
            }
            else if (y == 0)
            {
                //
                colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

            }
            else if (y == height - 1)
            {
                //
                colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;
            }
            colorDictionary.Remove(Color.FromArgb(255, 255, 255));
            colorDictionary.Remove(Color.FromArgb(255, 0, 0));
            colorDictionary.Remove(Color.White);
            bool border = colorDictionary.Count > 1 ? true : false;
            //buff_canvas[x, y].energy = colorDictionary.Count > 1 ? 5.0f : 2.0f;
            colorDictionary.Remove(Color.Black);
            colorDictionary.Remove(Color.FromArgb(0, 0, 0));

            if (colorDictionary.Count > 0)
            {
                Color max = colorDictionary.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                int count = colorDictionary[max];

                if (count == 3 || count == 4)
                {
                    buff_canvas[x, y].setGrain(max, border);
                    //regrowContinue = false;
                    wasChanged = true;
                }
                else
                    Moore4(x, y);
            }
        }

        private void Moore1(int x, int y)
        {
            var colorDictionary = new Dictionary<Color, int>();
            int currentCount = 0;

            if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
            {
                colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
            }
            else if (x == 0)
            {
                if (y == 0)
                {

                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;

                }
                else if (y == height - 1)
                {

                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                }
                else
                {

                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                    colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                }
            }
            else if (x == width - 1)
            {
                if (y == 0)
                {
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                }
                else if (y == height - 1)
                {
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                }
                else
                {
                    //
                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                    //
                    colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                }
            }
            else if (y == 0)
            {
                //
                colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

            }
            else if (y == height - 1)
            {
                //
                colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                //
                colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;
            }
            colorDictionary.Remove(Color.FromArgb(255, 255, 255));
            colorDictionary.Remove(Color.FromArgb(255, 0, 0));
            colorDictionary.Remove(Color.White);
            bool border = colorDictionary.Count > 1 ? true : false;
            ///buff_canvas[x, y].energy = colorDictionary.Count > 1 ? 5.0f : 2.0f;
            colorDictionary.Remove(Color.Black);
            colorDictionary.Remove(Color.FromArgb(0, 0, 0));

            if (colorDictionary.Count > 0)
            {
                Color max = colorDictionary.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                int count = colorDictionary[max];

                if ((int)numericUpDownProb.Value != 0)
                {
                    if (count >= 5)
                    {
                        buff_canvas[x, y].setGrain(max, border);
                        wasChanged = true;
                    }

                    else
                        Moore2(x, y);
                }
                else
                {
                    buff_canvas[x, y].setGrain(max, border);
                    wasChanged = true;
                }
            }
        }

        private int MonteCarloMoore(int x, int y, Color c)
        {
            colorDictionary2 = new Dictionary<Color, int>();
            int energy = 0;
            index = 0;

            if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
            {
                if(buff_canvas[x - 1, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y - 1].GetColor()))
                    {
                        colorDictionary2.Add(buff_canvas[x - 1, y - 1].GetColor(), index);
                        index++;
                    }
                    energy++;
                }
                if (buff_canvas[x, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x, y - 1].GetColor()))
                    {
                        colorDictionary2.Add(buff_canvas[x, y - 1].GetColor(), index);
                        index++;
                    }
                    energy++;
                }
                if (buff_canvas[x + 1, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y - 1].GetColor()))
                    {
                        colorDictionary2.Add(buff_canvas[x + 1, y - 1].GetColor(), index);
                        index++;
                    }
                    energy++;
                }
                if (buff_canvas[x - 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y].GetColor()))
                    {
                        colorDictionary2.Add(buff_canvas[x - 1, y].GetColor(), index);
                        index++;
                    }
                    energy++;
                }
                if (buff_canvas[x + 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y].GetColor()))
                    {
                        colorDictionary2.Add(buff_canvas[x + 1, y].GetColor(), index);
                        index++;
                    }
                    energy++;
                }
                if (buff_canvas[x - 1, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y + 1].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x - 1, y + 1].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x, y + 1].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x, y + 1].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x + 1, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y + 1].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x + 1, y + 1].GetColor(), index); index++; }
                    energy++;
                }
            }
            else if (x == 0)
            {
                if (y == 0)
                {
                    if (buff_canvas[x + 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (buff_canvas[x + 1, y].GetColor().ToArgb() != c.ToArgb())
                        {
                            if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y].GetColor()))
                            { colorDictionary2.Add(buff_canvas[x + 1, y].GetColor(), index); index++; }
                            energy++;
                        }
                    }
                    if (buff_canvas[x, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x, y + 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x, y + 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x + 1, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y + 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x + 1, y + 1].GetColor(), index); index++; }
                        energy++;
                    }

                }
                else if (y == height - 1)
                {
                    if (buff_canvas[x + 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x + 1, y].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x, y - 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x, y - 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x + 1, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y - 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x + 1, y - 1].GetColor(), index); index++; }
                        energy++;
                    }
                }
                else
                {
                    if (buff_canvas[x, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x, y - 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x, y - 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x + 1, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y - 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x + 1, y - 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x + 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x + 1, y].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x, y + 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x, y + 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x + 1, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y + 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x + 1, y + 1].GetColor(), index); index++; }
                        energy++;
                    }
                }
            }
            else if (x == width - 1)
            {
                if (y == 0)
                {
                    if (buff_canvas[x - 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x - 1, y].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x - 1, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y + 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x - 1, y + 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x, y + 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x, y + 1].GetColor(), index); index++; }
                        energy++;
                    }
                }
                else if (y == height - 1)
                {
                    if (buff_canvas[x - 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x - 1, y].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x - 1, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y - 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x - 1, y - 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x, y - 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x, y - 1].GetColor(), index); index++; }
                        energy++;
                    }
                }
                else
                {
                    if (buff_canvas[x, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x, y - 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x, y - 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x - 1, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y - 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x - 1, y - 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x - 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x - 1, y].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x - 1, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y + 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x - 1, y + 1].GetColor(), index); index++; }
                        energy++;
                    }
                    if (buff_canvas[x, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                    {
                        if (!colorDictionary2.ContainsKey(buff_canvas[x, y + 1].GetColor()))
                        { colorDictionary2.Add(buff_canvas[x, y + 1].GetColor(), index); index++; }
                        energy++;
                    }
                }
            }
            else if (y == 0)
            {
                if (buff_canvas[x - 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x - 1, y].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x - 1, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y + 1].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x - 1, y + 1].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x, y + 1].GetColor()))
                    {  colorDictionary2.Add(buff_canvas[x, y + 1].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x + 1, y + 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y + 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y + 1].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x + 1, y + 1].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x + 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x + 1, y].GetColor(), index); index++; }
                    energy++;
                }
            }
            else if (y == height - 1)
            {
                if (buff_canvas[x - 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x - 1, y].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x - 1, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x - 1, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x - 1, y - 1].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x - 1, y - 1].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x, y - 1].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x, y - 1].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x + 1, y - 1].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y - 1].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y - 1].GetColor()))
                    {  colorDictionary2.Add(buff_canvas[x + 1, y - 1].GetColor(), index); index++; }
                    energy++;
                }
                if (buff_canvas[x + 1, y].GetColor().ToArgb() != c.ToArgb() && buff_canvas[x + 1, y].GetColor().ToArgb() != Color.Black.ToArgb())
                {
                    if (!colorDictionary2.ContainsKey(buff_canvas[x + 1, y].GetColor()))
                    { colorDictionary2.Add(buff_canvas[x + 1, y].GetColor(), index); index++; }
                    energy++;
                }
            }
            colorDictionary2.Remove(Color.FromArgb(255, 0, 0));
            bool border = energy > 1 ? true : false;
            colorDictionary2.Remove(Color.Black);
            colorDictionary2.Remove(Color.FromArgb(0, 0, 0));

            return energy;

        }

        private int MonteCarloMoore2(int x, int y, Color c)
        {
            colorDictionary2 = new Dictionary<Color, int>();
            int energy = 0;
            int currentCount;

            for (int i = x - 1; i < x + 2; i++)
            {
                for(int j = y - 1; j < y + 2; j++)
                {
                    if(i >= 0 && i < width && j >= 0 && j < height)
                    {
                        if (buff_canvas[i, j].GetColor().ToArgb() != c.ToArgb())
                        {
                            if (buff_canvas[i, j].GetColor().ToArgb() != Color.Black.ToArgb())
                            {
                                colorDictionary2.TryGetValue(buff_canvas[i, j].GetColor(), out currentCount);
                                colorDictionary2[canvas[i, j].GetColor()] = currentCount + 1;
                                energy++;
                            }
                        }
                        else
                        {
                            colorDictionary2.TryGetValue(buff_canvas[i, j].GetColor(), out currentCount);
                            colorDictionary2[canvas[i, j].GetColor()] = currentCount + 1;
                        }
                    }
                }
            }
            colorDictionary2.Remove(Color.FromArgb(255, 0, 0));
            bool border = energy > 1 ? true : false;
            //buff_canvas[x, y].energy = energy > 1 ? 5.0f : 2.0f;
            colorDictionary2.Remove(Color.Black);
            colorDictionary2.Remove(Color.FromArgb(0, 0, 0));

            return energy;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            int xCoordinate = me.X;
            int yCoordinate = me.Y;
            int ID = canvas[xCoordinate, yCoordinate].GetID();
            switch (me.Button) {
                case MouseButtons.Left:
                    if (canvas[xCoordinate, yCoordinate].GetColor().ToArgb().Equals(Color.FromArgb(255, 0, 0).ToArgb()))
                    {
                        for (int i = 0; i < width; i++)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                if (canvas[i, j].GetID() == ID || (j == yCoordinate && i == xCoordinate))
                                {
                                    canvas[i, j].isSelected = false;
                                    canvas[i, j].SetColor(canvas[i, j].color_temp);
                                }
                            }
                        }
                        setBitmapFromCanvas();
                    }
                    else if (!canvas[xCoordinate, yCoordinate].GetColor().ToArgb().Equals(Color.White))
                    {
                        Console.WriteLine(canvas[xCoordinate, yCoordinate].GetID());
                        for (int i = 0; i < width; i++)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                if (canvas[i, j].GetID() == ID || (j == yCoordinate && i == xCoordinate))
                                {
                                    canvas[i, j].isSelected = true;
                                    canvas[i, j].color_temp = canvas[i, j].GetColor();
                                    canvas[i, j].SetColor(selectionColor);
                                }
                            }
                        }
                        setBitmapFromCanvas();

                    }
                    break;
                case MouseButtons.Right:
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            if (canvas[x, y].GetID() == ID)
                            {
                                var colorDictionary = new Dictionary<Color, int>();
                                int currentCount = 0;

                                if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
                                {
                                    colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;

                                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                                    colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;

                                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                                    colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;

                                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                                    colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                                }
                                else if (x == 0)
                                {
                                    if (y == 0)
                                    {

                                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                                        colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;

                                    }
                                    else if (y == height - 1)
                                    {

                                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                                        colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                                    }
                                    else
                                    {

                                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                                        colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                                        colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                                    }
                                }
                                else if (x == width - 1)
                                {
                                    if (y == 0)
                                    {
                                        //
                                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                                        //
                                        colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                                    }
                                    else if (y == height - 1)
                                    {
                                        //
                                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                                        //
                                        colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                                    }
                                    else
                                    {
                                        //
                                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                                        //
                                        colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                                        //
                                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                                        //
                                        colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                                    }
                                }
                                else if (y == 0)
                                {
                                    //
                                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                                    //
                                    colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                                    //
                                    colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                                    //
                                    colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                                }
                                else if (y == height - 1)
                                {
                                    //
                                    colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                                    colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                                    //
                                    colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                                    //
                                    colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                                    //
                                    colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                                    colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                                    colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                                    colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;
                                }
                                bool border = colorDictionary.Count > 1 ? true : false;

                                if (colorDictionary.Count > 1)
                                {
                                    buff_canvas[x, y].setGrain(Color.Black, border);
                                    borderCount++;
                                }
                            }
                        }
                    }
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            canvas[x, y].CopyData(buff_canvas[x, y].GetID(), buff_canvas[x, y].GetColor(), buff_canvas[x, y].GetIsBorder());
                        }
                    }
                    setBitmapFromCanvas();
                    float result = (float)borderCount * 100 / (float)(width * height);
                    borderLabel.Text = String.Format("{0:0.00}", result) + "%";
                    break;
            }
        }

        private void SetSelectedGrain(int x, int y)
        {
            canvas[x, y].isSelected = true;
            canvas[x, y].SetColor(selectionColor);

            if (!canvas[x, y].GetIsBorder()) {
                if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
                {
                    SetSelectedGrain(x - 1, y - 1);
                    SetSelectedGrain(x - 1, y);
                    SetSelectedGrain(x - 1, y + 1);
                    SetSelectedGrain(x, y - 1);
                    SetSelectedGrain(x, y + 1);
                    SetSelectedGrain(x + 1, y - 1);
                    SetSelectedGrain(x + 1, y);
                    SetSelectedGrain(x + 1, y + 1);

                }
                else if (x == 0)
                {
                    if (y == 0)
                    {

                        SetSelectedGrain(x + 1, y);

                        SetSelectedGrain(x, y + 1);

                        SetSelectedGrain(x + 1, y + 1);

                    }
                    else if (y == height - 1)
                    {

                        SetSelectedGrain(x + 1, y);

                        SetSelectedGrain(x, y - 1);

                        SetSelectedGrain(x + 1, y - 1);
                    }
                    else
                    {

                        SetSelectedGrain(x, y - 1);

                        SetSelectedGrain(x + 1, y - 1);

                        SetSelectedGrain(x + 1, y);

                        SetSelectedGrain(x, y + 1);

                        SetSelectedGrain(x + 1, y + 1); ;
                    }
                }
                else if (x == width - 1)
                {
                    if (y == 0)
                    {
                        SetSelectedGrain(x - 1, y);
                        SetSelectedGrain(x - 1, y + 1);
                        SetSelectedGrain(x, y + 1);

                    }
                    else if (y == height - 1)
                    {
                        SetSelectedGrain(x - 1, y);
                        SetSelectedGrain(x - 1, y - 1);
                        SetSelectedGrain(x, y - 1);
                    }
                    else
                    {
                        SetSelectedGrain(x, y - 1);
                        SetSelectedGrain(x - 1, y - 1);
                        SetSelectedGrain(x - 1, y);
                        SetSelectedGrain(x - 1, y + 1);
                        SetSelectedGrain(x, y + 1);
                    }
                }
                else if (y == 0)
                {
                    SetSelectedGrain(x - 1, y);
                    SetSelectedGrain(x - 1, y + 1);
                    SetSelectedGrain(x, y + 1);
                    SetSelectedGrain(x + 1, y + 1);
                    SetSelectedGrain(x + 1, y);

                }
                else if (y == height - 1)
                {
                    SetSelectedGrain(x - 1, y);
                    SetSelectedGrain(x - 1, y - 1);
                    SetSelectedGrain(x, y - 1);
                    SetSelectedGrain(x + 1, y - 1);
                    SetSelectedGrain(x + 1, y);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorList.Clear();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (!canvas[i, j].isSelected)
                    {
                        TestBitmap.SetPixel(i, j, Color.White);
                        canvas[i, j].ResetPixel();
                        buff_canvas[i, j].ResetPixel();
                    }
                }
            }

            pictureBox1.Image = TestBitmap;
            label4.Text = "";


        }

        private void button5_Click(object sender, EventArgs e)
        {
            borderCount = 0;
            borderLabel.Text = "";
            for (int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    var colorDictionary = new Dictionary<Color, int>();
                    int currentCount = 0;

                    if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
                    {
                        colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                    }
                    else if (x == 0)
                    {
                        if (y == 0)
                        {

                            colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;

                        }
                        else if (y == height - 1)
                        {

                            colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                        }
                        else
                        {

                            colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                        }
                    }
                    else if (x == width - 1)
                    {
                        if (y == 0)
                        {
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                            colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                        }
                        else if (y == height - 1)
                        {
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                            colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                        }
                        else
                        {
                            //
                            colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                            colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                        }
                    }
                    else if (y == 0)
                    {
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                    }
                    else if (y == height - 1)
                    {
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;
                    }
                    //colorDictionary.Remove(Color.FromArgb(255, 255, 255));
                    //colorDictionary.Remove(Color.FromArgb(255, 0, 0));
                    bool border = colorDictionary.Count > 1 ? true : false;
                    //colorDictionary.Remove(Color.Black);

                    if (colorDictionary.Count > 1)
                    {
                        //Console.WriteLine("Setting Border Pixel");
                        buff_canvas[x, y].setGrain(Color.Black, border);
                        borderCount++;
                    }
                }
                
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    canvas[x, y].CopyData(buff_canvas[x, y].GetID(), buff_canvas[x, y].GetColor(), buff_canvas[x, y].GetIsBorder());
                }
            }
            setBitmapFromCanvas();

            float result = (float)borderCount * 100 / (float)(width * height);
            borderLabel.Text = String.Format("{0:0.00}", result) + "%";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            regrow = true;
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    if(!((canvas[x,y].GetColor().ToArgb().Equals(Color.Black.ToArgb())) && (canvas[x,y].GetIsBorder() == true)))
                    {
                        canvas[x, y].ResetPixel();
                        buff_canvas[x, y].ResetPixel();
                    }
                }
            }
            setBitmapFromCanvas();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void DistributeEnergy()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var colorDictionary = new Dictionary<Color, int>();
                    int currentCount = 0;

                    if (x != 0 && x != width - 1 && y != 0 && y != height - 1)
                    {
                        colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                        colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                    }
                    else if (x == 0)
                    {
                        if (y == 0)
                        {

                            colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;

                        }
                        else if (y == height - 1)
                        {

                            colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                        }
                        else
                        {

                            colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                            colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                        }
                    }
                    else if (x == width - 1)
                    {
                        if (y == 0)
                        {
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                            colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;

                        }
                        else if (y == height - 1)
                        {
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                            colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                        }
                        else
                        {
                            //
                            colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                            //
                            colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                            colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                            colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                        }
                    }
                    else if (y == 0)
                    {
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y + 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y + 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x + 1, y + 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y + 1].GetColor()] = currentCount + 1;
                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;

                    }
                    else if (y == height - 1)
                    {
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x - 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x - 1, y - 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x, y - 1].GetColor()] = currentCount + 1;
                        //
                        colorDictionary.TryGetValue(canvas[x + 1, y - 1].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y - 1].GetColor()] = currentCount + 1;
                        colorDictionary.TryGetValue(canvas[x + 1, y].GetColor(), out currentCount);
                        colorDictionary[canvas[x + 1, y].GetColor()] = currentCount + 1;
                    }
                    bool border = colorDictionary.Count > 1 ? true : false;

                    if (colorDictionary.Count > 1)
                    {
                        if (grainEnergy.Value != boundaryEnergy.Value)
                        {
                            EnergyBitmap.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                            buff_canvas[x, y].energy = 5.0f;
                        }
                        else
                            EnergyBitmap.SetPixel(x, y, Color.FromArgb(0, 0, 255 - ((255 - buff_canvas[x, y].GetColor().B) / 10)));

                    }
                    else
                    {
                        EnergyBitmap.SetPixel(x, y, Color.FromArgb(0, 0, 255 - ((255 - buff_canvas[x,y].GetColor().B)/10)));
                    }
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!showEnergy)
            {
                DistributeEnergy();
                pictureBox1.Image = EnergyBitmap;
                showEnergy = true;
            }
            else
            {
                pictureBox1.Image = TestBitmap;
                showEnergy = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int r;
            int g;
            int b;
            bool result = true;

            for (int i = 0; i < grainBegin.Value; i++)
            {
                r = rnd2.Next(50, 251);
                g = rnd2.Next(50, 251);
                b = rnd2.Next(50, 251);

                do
                {
                    int x = rnd2.Next(width);
                    int y = rnd2.Next(height);

                    if (canvas[x, y].GetColor().ToArgb().Equals(Color.White.ToArgb()))
                    {
                        canvas[x, y].SetColor(Color.FromArgb(r, g, b));
                        string buffer = r.ToString() + g.ToString() + b.ToString();
                        canvas[x, y].SetID(Int32.Parse(buffer));
                        result = false;
                    }
                } while (result);
                result = true;

            }
        }
    }
}
