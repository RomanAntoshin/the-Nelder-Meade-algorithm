using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            double A = 1;
            double B = 0.5;
            double G = 2;
            int k = 0;
            bool flag = false;
            List<Point> points = new List<Point>();
            Random random = new Random();
            //Начальный массив точек
            int n = int.Parse(textBox_size.Text);
            //listBox1.Items.Add("//////");
            for (int i = 0; i < n + 1; i++)
            {
                points.Add(GetPoint(random, n));
                points[i].y = Criteri(points[i]);
                listBox1.Items.Add(points[i].y + " s");
            }
            /*List<double> s = new List<double>();
            s.Add(21);
            Point p = new Point(s);
            points.Add(p);
            List<double> s2 = new List<double>();
            s2.Add(51);
            Point p2 = new Point(s2);
            points.Add(p2);*/
            while (k < 10)
            {
                if (k == 0)
                    listBox1.Items.Add("Xc=" + GetXc(points).Vect[0]);
                Algorithm(points, A, B, G);
                //listBox1.Items.Add(Algorithm(ref points, A, B, G));
                k++;
                listBox1.Items.Add(points[0].Vect[0] + " &");
               // if (k == 9)
                //    listBox1.Items.Add("Amswer: " + Algorithm(ref points, A, B, G));
            }
        }
        Point GetPoint(Random random, int n)
        {
            int r;
            List<double> Vect = new List<double>();
            for (int i = 0; i < n; i++)
            {
                r = random.Next(-100, 101);
                Vect.Add(r);
                //listBox1.Items.Add(Vect[i]);
            }
            Point p = new Point(Vect);
            return p;
        }
        double Criteri(Point p)
        {
            //double y =Math.Sin(p.Vect[0]);
            double y = p.Vect[0] * p.Vect[0];
            p.y = y;
            return y;
        }
        double Algorithm(List<Point> points, double A, double B, double G)
        {
            Sort(points);
            Point Xc = new Point();
            Xc = GetXc(points);
            Point Xr = new Point(points[0].Vect.Count);
            Xr = (1 + A) * Xc + A * points[points.Count-1];
            Xc.y = Criteri(Xc);
            Xr.y = Criteri(Xr);
            Point Xs = new Point(points.Count - 1);
            if(Reflection(points, Xc, Xr, G, B, ref Xs)==true)
            {
                points[0].y = Criteri(points[0]);
                listBox1.Items.Add("S5T9");
                return points[0].y;
            }
            //Step 7
            if (Xs.y < points[points.Count - 1].y)
            {
                points[points.Count - 1] = Xs;
                Dispersion(points[0], points[points.Count - 2], points[points.Count - 1]);
                points[0].y = Criteri(points[0]);
                listBox1.Items.Add("S7T9");
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
        }
        Point GetXc(List<Point> points)
        {
            Point Xc = new Point(points.Count - 1);
            for (int i = 0; i < Xc.Vect.Count; i++)
            {
                for (int j = 0; j < points.Count - 1; j++)
                    Xc.Vect[i] = (Xc.Vect[i] + points[j].Vect[i]);
            }
            for (int i = 0; i < Xc.Vect.Count; i++)
                Xc.Vect[i] = Xc.Vect[i] / (points.Count - 1);
            return Xc;
        }
        bool Reflection(List<Point> points, Point Xc, Point Xr, double G, double B, ref Point Xs)
        {
            bool flag = false;
            if (Xr.y < points[0].y)
            {
                Point Xe = new Point(Xr.Vect.Count);
                Xe = (1 - G) * Xc + G * Xr;
                Xe.y = Criteri(Xe);
                if (Xe.y < Xr.y)
                {
                    points[points.Count-1] = Xe;
                    //Dispersion(Xbest, Xgood, Xworst);
                    flag = true;
                    //Закончить итерацию алгоритма
                }
                if (Xr.y < Xe.y)
                {
                    points[points.Count - 1] = Xr;
                    //Dispersion(Xbest, Xgood, Xworst);
                    flag = true;
                    //Закончить итерацию алгоритма
                }
            }
            if (points[0].y < Xr.y && Xr.y < points[points.Count-2].y)
            {
                points[points.Count-1] = Xr;
                //Dispersion(Xbest, Xgood, Xworst);
                flag = true;
                //Закончить итерацию алгоритма
            }
            //Point Xs = new Point(Xbest.Vect.Count);
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
            return flag;
        }
        Point Compression(Point Xc, Point Xw, double B)
        {
            Point res = new Point();
            res = B * Xw + (1 - B) * Xc;
            res.y = Criteri(res);
            return res;
        }
        double Dispersion(Point p1, Point p2, Point p3)
        {
            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            for (int i = 0; i < p1.Vect.Count; i++)
            {
                sum1 = sum1 + (p1.Vect[i] - p2.Vect[i]) * (p1.Vect[i] - p2.Vect[i]);
                sum2 = sum2 + (p1.Vect[i] - p3.Vect[i]) * (p1.Vect[i] - p3.Vect[i]);
                sum3 = sum3 + (p2.Vect[i] - p3.Vect[i]) * (p2.Vect[i] - p3.Vect[i]);
            }
            return Max(Math.Sqrt(sum1), Math.Sqrt(sum2), Math.Sqrt(sum3));
        }
        double Max(double a, double b, double c)
        {
            List<double> vs = new List<double>();
            vs.Add(a);
            vs.Add(b);
            vs.Add(c);
            return vs.Max();
        }
    }
}
