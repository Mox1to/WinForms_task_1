using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int xc = 100;
            int yc = this.Height / 2;
            g.TranslateTransform(xc, yc);
            g.DrawLine(new Pen(Color.Black, 2.0f), 0, 0, 1000, 0);
            g.DrawLine(new Pen(Color.Black, 2.0f), 0, 250, 0, -250);
            g.DrawString("100", new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, -50, -260);
            g.DrawString("-100", new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, -55, 240);
        }

        void CleanAndDraw()
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            int xc = 100;
            int yc = this.Height / 2;
            g.TranslateTransform(xc, yc);
            g.DrawLine(new Pen(Color.Black, 2.0f), 0, 0, 1000, 0);
            g.DrawLine(new Pen(Color.Black, 2.0f), 0, 250, 0, -250);
            g.DrawString("100", new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, -50, -260);
            g.DrawString("-100", new Font(FontFamily.GenericSansSerif, 12), Brushes.Black, -55, 240);
        }

        List<Column> columns;

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            CleanAndDraw();
            int xc = 100;
            int yc = this.Height / 2;
            g.TranslateTransform(xc, yc);
            Random rnd = new Random();
            columns = new List<Column>();
            int n = rnd.Next(10, 30);
            float k = 500 / n;
            for (int i = 0; i < n; i++)
            {
                Column temp = new Column();
                int height = 0;
                while (height == 0)
                {
                    height = rnd.Next(-100, 100);
                    temp.height = height;
                }
                temp.red = rnd.Next(0, 255);
                temp.green = rnd.Next(0, 255);
                temp.blue = rnd.Next(0, 255);
                columns.Add(temp);
                SolidBrush tempbrush = new SolidBrush(Color.FromArgb(temp.red, temp.green, temp.blue));
                Font tempfont = new Font(FontFamily.GenericSansSerif, 9);
                if (temp.height > 0) 
                {
                    if (i == 0)
                    {
                        g.FillRectangle(tempbrush, 0, -temp.height * 2.5f, 500 / n, temp.height * 2.5f);
                        g.DrawRectangle(new Pen(Color.Black), 0, -temp.height * 2.5f, k, temp.height * 2.5f);
                        g.DrawString(temp.height.ToString(), tempfont, Brushes.Black, k / 2 - tempfont.Size, -temp.height * 2.5f - 25);
                    }
                    else
                    {
                        g.FillRectangle(tempbrush, 2 * i * k, -temp.height * 2.5f, k, temp.height * 2.5f);
                        g.DrawRectangle(new Pen(Color.Black), 2 * i * k, -temp.height * 2.5f, k, temp.height * 2.5f);
                        g.DrawString(temp.height.ToString(), tempfont, Brushes.Black, 2 * i * k + (k / 2 - tempfont.Size), -temp.height * 2.5f - 25);
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        g.FillRectangle(tempbrush, 0, 0, k, -temp.height * 2.5f);
                        g.DrawRectangle(new Pen(Color.Black), 0, 0, k, -temp.height * 2.5f);
                        g.DrawString(temp.height.ToString(), tempfont, Brushes.Black, k / 2.5f - tempfont.Size, -temp.height * 2.5f + 10);
                    }
                    else
                    {
                        g.FillRectangle(tempbrush, 2 * i * k, 0, k, -temp.height * 2.5f);
                        g.DrawRectangle(new Pen(Color.Black), 2 * i * k, 0, k, -temp.height * 2.5f);
                        g.DrawString(temp.height.ToString(), tempfont, Brushes.Black, 2 * i * k + (k / 2.5f - tempfont.Size), -temp.height * 2.5f + 10);
                    }
                }
            }
            g.DrawLine(new Pen(Color.Black, 2.0f), 0, 0, 1000, 0);
            label1.Text = "Построена случайная диаграмма";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("2.txt")) //файл располагается в папке Debug
            {
                foreach (Column column in columns)
                {
                    sw.WriteLine($"{column.height}:{column.red}:{column.green}:{column.blue}");
                }
            }
            label1.Text = "Текущая диаграмма сохранена в 2.txt";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            CleanAndDraw();
            int xc = 100;
            int yc = this.Height / 2;
            g.TranslateTransform(xc, yc);
            columns = new List<Column>();
            int n = 0;
            using (StreamReader sr = new StreamReader("1.txt")) //файл располагается в папке Debug
            {
                while (sr.Peek() != -1)
                {
                    sr.ReadLine();
                    n++;
                }
            }
            float k = 500 / n;
            using (StreamReader sr = new StreamReader("1.txt"))
            {
                int i = 0;
                while (sr.Peek() != -1)
                {
                    Column temp = new Column();
                    string[] line = sr.ReadLine().Split(':');
                    temp.height = int.Parse(line[0]);
                    temp.red = int.Parse(line[1]);
                    temp.green = int.Parse(line[2]);
                    temp.blue = int.Parse(line[3]);
                    columns.Add(temp);
                    SolidBrush tempbrush = new SolidBrush(Color.FromArgb(temp.red, temp.green, temp.blue));
                    Font tempfont = new Font(FontFamily.GenericSansSerif, 9);
                    if (temp.height > 0)
                    {
                        if (i == 0)
                        {
                            g.FillRectangle(tempbrush, 0, -temp.height * 2.5f, 500 / n, temp.height * 2.5f);
                            g.DrawRectangle(new Pen(Color.Black), 0, -temp.height * 2.5f, k, temp.height * 2.5f);
                            g.DrawString(temp.height.ToString(), tempfont, Brushes.Black, k / 2 - tempfont.Size, -temp.height * 2.5f - 25);
                        }
                        else
                        {
                            g.FillRectangle(tempbrush, 2 * i * k, -temp.height * 2.5f, k, temp.height * 2.5f);
                            g.DrawRectangle(new Pen(Color.Black), 2 * i * k, -temp.height * 2.5f, k, temp.height * 2.5f);
                            g.DrawString(temp.height.ToString(), tempfont, Brushes.Black, 2 * i * k + (k / 2 - tempfont.Size), -temp.height * 2.5f - 25);
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            g.FillRectangle(tempbrush, 0, 0, k, -temp.height * 2.5f);
                            g.DrawRectangle(new Pen(Color.Black), 0, 0, k, -temp.height * 2.5f);
                            g.DrawString(temp.height.ToString(), tempfont, Brushes.Black, k / 2.5f - tempfont.Size, -temp.height * 2.5f + 10);
                        }
                        else
                        {
                            g.FillRectangle(tempbrush, 2 * i * k, 0, k, -temp.height * 2.5f);
                            g.DrawRectangle(new Pen(Color.Black), 2 * i * k, 0, k, -temp.height * 2.5f);
                            g.DrawString(temp.height.ToString(), tempfont, Brushes.Black, 2 * i * k + (k / 2.5f - tempfont.Size), -temp.height * 2.5f + 10);
                        }
                    }
                    i++;
                }
            }
            g.DrawLine(new Pen(Color.Black, 2.0f), 0, 0, 1000, 0);
            label1.Text = "Построена пользовательская диаграмма из 1.txt";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
