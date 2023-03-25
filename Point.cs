using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаб_1__вт_попытка_
{
    class Point
    {
        /* double X;
        double Y;
        double Z;
        double T;*/
        public List<double> Vect = new List<double>();
        public double y;
        /* Point(double x1)
         {
             X = x1;
         }
         Point(double x1, double x2)
         {
             X = x1;
             Y = x2;
         }
         Point(double x1, double x2, double x3)
         {
             X = x1;
             Y = x2;
             Z = x3;
         }
         Point(double x1, double x2, double x3, double x4)
         {
             X = x1;
             Y = x2;
             Z = x3;
             T = x4;
         }*/
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
