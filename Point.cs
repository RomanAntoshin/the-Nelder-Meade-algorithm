using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лаб_1__вт_попытка_
{
    class Point
    {
        public List<double> Vect = new List<double>();
        public double y;
        public Point(List<double> x)
        {
            Vect.Clear();
            foreach (double el in x)
                Vect.Add(el);
        }
        public Point()
        {
            Vect.Clear();
        }
        public Point(int n)
        {
            for (int i = 0; i < n; i++)
                Vect.Add(0);
        }
        public void Print(Point p, TextBox textBox, ListBox listBox)
        {
            textBox.Text = "(";
            foreach (double el in p.Vect)
                textBox.Text = textBox.Text + el.ToString() + ";" + " ";
            textBox.Text = textBox.Text + ")";
            listBox.Items.Add(textBox.Text);
            textBox.Clear();
        }
        public void Print(List<Point> points, TextBox textBox, ListBox listBox)
        {
            foreach (Point el in points)
                el.Print(el, textBox, listBox);
        }
        public static Point operator +(Point p1, Point p2)
        {
            Point res = new Point(p1.Vect.Count);
            for (int i = 0; i < p1.Vect.Count; i++)
                res.Vect[i] = p1.Vect[i] + p2.Vect[i];
            return res;
        }
        public static Point operator *(double a, Point p)
        {
            Point res = new Point(p.Vect);
            for (int i = 0; i < res.Vect.Count; i++)
                res.Vect[i] = res.Vect[i] * a;
            return res;
        }
        public static Point operator -(Point p1, Point p2)
        {
            Point res = new Point(p1.Vect.Count);
            for (int i = 0; i < p1.Vect.Count; i++)
                res.Vect[i] = p1.Vect[i] - p2.Vect[i];
            return res;
        }
        public static Point operator /(Point p, double a)
        {
            Point res = new Point(p.Vect);
            for (int i = 0; i < res.Vect.Count; i++)
                res.Vect[i] = res.Vect[i] / a;
            return res;
        }
    }
}
