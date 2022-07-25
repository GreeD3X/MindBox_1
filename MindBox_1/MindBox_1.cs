using System;
using System.Dynamic;

namespace MindBox_1
{
    public class AreaCalculator
    {
        public AreaCalculator()
        {
            DynamicModules = new ExpandoObject();
        }
        public static double GetAreaCircle(double radius)
        {
            return radius >= 0 ? radius * radius * Math.PI : -1;
        }

        public static double GetAreaTriang(double side1, double side2, double side3)
        {
            double squareSum = side1 * side1 + side2 * side2 + side3 * side3;

            if (side1 <= 0 || side2 <= 0 || side3 <= 0)
                return -1;

            double rightArea = side2 * side3 / 2 * RightTriangle(squareSum, side1) + side1 * side3 / 2 * RightTriangle(squareSum, side2) + side1 * side2 / 2 * RightTriangle(squareSum, side3);

            if (rightArea > 0)
                return rightArea;

            double halfSum = (side1 + side2 + side3) / 2;

            return Math.Sqrt(halfSum * (halfSum - side1) * (halfSum - side2) * (halfSum - side3));
        }

        private static double RightTriangle(double squareSum, double sideCheck)
        {
            if (squareSum == 2 * sideCheck * sideCheck)
            {
                return 1;
            }
            return 0;
        }

        public static double GetAreaArbitraryPoly(Tuple<double, double>[] points)
        {
            double totalArea = 0;
            for (int i = 0; i < points.Length; i++)
            {
                if (i == 0)
                {
                    totalArea += points[i].Item1 * (points[i + 1].Item2 - points[points.Length - 1].Item2);
                    continue;
                }

                if (i == points.Length - 1)
                {
                    totalArea += points[i].Item1 * (points[0].Item2 - points[i - 1].Item2);
                    continue;
                }
                totalArea += points[i].Item1 * (points[i + 1].Item2 - points[i - 1].Item2);
            }

            return totalArea/2;
        }
        public static double GetAreaArbitraryPoly(double[] pointx, double[] pointy)
        {
            if (pointx.Length != pointy.Length)
                return -1;

            Tuple<double, double>[] points = new Tuple<double, double>[pointx.Length];

            for (int i = 0; i < pointx.Length; i++)
            {
                points[i] = new Tuple<double, double>(pointx[i], pointy[i]);
            }

            return GetAreaArbitraryPoly(points);
        }

        public dynamic DynamicModules;
    }
}
