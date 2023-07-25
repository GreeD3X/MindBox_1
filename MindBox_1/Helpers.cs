using System;
using System.Collections.Generic;
using System.Text;

namespace MindBox_1
{
    internal static class Helpers
    {
        public static double GetLength(Tuple<double, double> p1, Tuple<double, double> p2)
        {
            return Math.Sqrt(Math.Pow(p1.Item1 - p2.Item1, 2) + Math.Pow(p1.Item2 - p2.Item2, 2));
        }

        public static double GetOrderedArea(Tuple<double, double> p1, Tuple<double, double> p2, Tuple<double, double> p3)
        {
            return p1.Item1 * (p2.Item2 - p3.Item2);
        }
    }
}
