using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MyUfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Invalidate();
            Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        int factorial(int x) {
            if (x == 0)
                return 1;
            else
                return (x * factorial(x - 1));
        }

        double msin(int s, double x) {
            double sin = 0.0;
            for (int n = 1; n <= s; n++)
                sin = sin + (Math.Pow(-1, n - 1) * Math.Pow(x, 2 * n - 1))/ (factorial(2 * n - 1));
            return sin;
        }

        double mcos(int s, double x)
        {
            double cos = 0.0;
            for (int n = 1; n <= s; n++)
                cos = cos + (Math.Pow(-1, n - 1) * Math.Pow(x, 2 * n - 2)) / (factorial(2 * n - 2));
            return cos;
        }
        double marctg(int s, double x)
        {
            double arctg = 0;
            if (-1 <= x && x <= 1)
            {
                for (int n = 1; n < s + 1; n++)
                    arctg = arctg + (Math.Pow(-1, n - 1) * Math.Pow(x, 2 * n - 1)) / (2 * n - 1);
            }
            else {
                if (x > 1) {
                    arctg = arctg + Math.PI / 2;
                    for (int n = 0; n < s; n++)
                        arctg = arctg - Math.Pow(-1, n) / ((2 * n + 1) * Math.Pow(x, 2 * n + 1));
                }
                else
                {
                    arctg = arctg - Math.PI / 2;
                    for (int n = 0; n < s; n++)
                        arctg = arctg - Math.Pow(-1, n) / ((2 * n + 1) * Math.Pow(x, 2 * n + 1));
                }
            }
            return arctg;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsState gs;
            g.ScaleTransform(0.5f, 0.5f);

            Pen pline = new Pen(Color.MidnightBlue, 4);
            Pen ppoint = new Pen(Color.Red, 10);

            double x1 = 20;
            double y1 = 30;
            double x2 = 850;
            double y2 = 750;

            g.DrawEllipse(ppoint, (int)x1, (int)y1, 2, 2);
            g.DrawEllipse(ppoint, (int)x2, (int)y2, 2, 2);

            double a = Math.Abs(x1 - x2);
            double b = Math.Abs(y1 - y2);
            double value = a + b;
            double distance = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            double step = 1;
            double angle = marctg(10, (y2 - y1) / (x1 - x2));

            while (distance <= value)
            {
                x1 = x1 + step * mcos(10, angle);
                y1 = y1 - step * msin(10, angle);
                g.DrawEllipse(pline, (int)x1, (int)y1, 1, 1);
                a = Math.Abs(x1 - x2);
                b = Math.Abs(y1 - y2);
                distance = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
                if (distance < value)
                    value = distance;
            }

            g.DrawString("Погрешность: " + value.ToString("0.000"), new Font("Microsoft Sans Serif", 30), Brushes.Black, new PointF(10, 600));
            gs = g.Save();
            g.Restore(gs);

            List<double> listpoint = new List<double>();
            for (int i = 2; i <= 10; i++)
            {
                x1 = 20;
                y1 = 30;
                a = Math.Abs(x1 - x2);
                b = Math.Abs(y1 - y2);
                value = a + b;
                distance = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
                angle = marctg(i, (y2 - y1) / (x1 - x2));

                while (distance <= value)
                {
                    x1 = x1 + step * mcos(i, angle);
                    y1 = y1 - step * msin(i, angle);
                    a = Math.Abs(x1 - x2);
                    b = Math.Abs(y1 - y2);
                    distance = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
                    if (distance < value)
                        value = distance;
                }
                listpoint.Add(value);
            }
            Form2 f2 = new Form2(listpoint);
            f2.Show();
        }
    }
}
