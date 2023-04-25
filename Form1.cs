using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace лаб_1__вт_попытка_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Начальный симплекс: ");
            double Alfa = 1;
            double Betta = 0.5;
            double Gamma = 2;
            int stepcount = 0;
            List<Point> points = new List<Point>();
            Random random = new Random();
            //Начальный массив точек
            int n = int.Parse(textBox_size.Text);
            for (int i = 0; i < n + 1; i++)
            {
                points.Add(GetPoint(random, n));
                points[i].y = Criteri(points[i]);
            }
            Point p = new Point();
            p.Print(points, textBox1, listBox1);
            Sort(points);
            listBox1.Items.Add("--------------------");
            while (stepcount < 10)
            {
                Algorithm(points, Alfa, Betta, Gamma);
                stepcount++;
                points[0].Print(points[0], textBox1, listBox1);
                listBox1.Items.Add(points[0].y + " - Критерий");
            }
        }
        Point GetPoint(Random random, int n)
        {
            int r;
            List<double> Vect = new List<double>();
            for (int i = 0; i < n; i++)
            {
                r = random.Next(-10, 11);
                Vect.Add(r);
            }
            Point p = new Point(Vect);
            return p;
        }
        double Criteri(Point p)
        {
            double y = p.Vect[0] * p.Vect[0] + p.Vect[1] * p.Vect[1];
            p.y = y;
            return y;
        }
        double Algorithm(List<Point> points, double A, double B, double G)
        {
            Sort(points);
            Point Xc = new Point();
            Xc = GetXc(points);
            Point Xr = new Point(points[0].Vect.Count);
            Xr = (1 + A) * Xc - A * points[points.Count-1]; //Считается верно
            Xc.y = Criteri(Xc);
            Xr.y = Criteri(Xr);
            Point Xs = new Point(points.Count - 1);
            if(Reflection(points, Xc, Xr, G, B, ref Xs)==true)
            {
                points[0].y = Criteri(points[0]);
                listBox1.Items.Add("S5T9");
                Sort(points);
                return points[0].y;
            }
            //Step 7
            if (Xs.y < points[points.Count - 1].y)
            {
                points[points.Count - 1] = Xs;
                points[0].y = Criteri(points[0]);
                listBox1.Items.Add("S7T9");
                Sort(points);
                return points[0].y;
            }
            //Step 8
            if (Xs.y > points[points.Count - 1].y)
            {
                for (int i = 1; i < points.Count; i++)
                {
                    points[i] = points[0] + (Xc - points[0]) / 2;
                    points[i].y = Criteri(points[i]);
                }
                listBox1.Items.Add("S8T9");
            }
            return points[0].y;
        }
        void Sort(List<Point> points)
        {
            Point buf = new Point();
            for (int i = 0; i < points.Count; i++)
                for (int j = 0; j < points.Count; j++)
                    if (points[i].y < points[j].y)
                    {
                        buf = points[i];
                        points[i] = points[j];
                        points[j] = buf;
                    }
        } //Работает корректно
        Point GetXc(List<Point> points)
        {
            Point Xc = new Point(points.Count - 1);
            for (int i = 0; i < points.Count - 1; i++)
                Xc = Xc + points[i];
            Xc = Xc / (points.Count - 1);
            return Xc;
        } //Работает корректно
        bool Reflection(List<Point> points, Point Xc, Point Xr, double G, double B, ref Point Xs)
        {
            bool stopiteration = false;
            if (Xr.y < points[0].y)
            {
                Point Xe = new Point(Xr.Vect.Count);
                Xe = (1 - G) * Xc + G * Xr;
                Xe.y = Criteri(Xe);
                /*listBox1.Items.Add("Xe=");
                Xe.Print(Xe, textBox1, listBox1);*/
                if (Xe.y < Xr.y)
                {
                    points[points.Count-1] = Xe;
                    stopiteration = true;
                    //Закончить итерацию алгоритма
                }
                if (Xr.y < Xe.y)
                {
                    points[points.Count - 1] = Xr;
                    //Dispersion(Xbest, Xgood, Xworst);
                    stopiteration = true;
                    //Закончить итерацию алгоритма
                }
            }
            if (points[0].y < Xr.y && Xr.y < points[points.Count-2].y)
            {
                points[points.Count-1] = Xr;
                stopiteration = true;
                //Закончить итерацию алгоритма
            }
            if (points[points.Count - 2].y < Xr.y && Xr.y < points[points.Count - 1].y)
            {
                Point buffer = new Point();
                buffer = Xr;
                Xr = points[points.Count - 1];
                points[points.Count - 1] = buffer;
                Xs = Compression(Xc, points[points.Count - 1], B);
                Xs.y = Criteri(Xs);
            }
            if (points[points.Count - 1].y < Xr.y)
            {
                Xs = Compression(Xc, points[points.Count - 1], B);
                Xs.y = Criteri(Xs);
            }
            return stopiteration;
        }
        Point Compression(Point Xc, Point Xw, double B)
        {
            Point res = new Point();
            res = B * Xw + (1 - B) * Xc;
            res.y = Criteri(res);
            return res;
        }

    }
}
