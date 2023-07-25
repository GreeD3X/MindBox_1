using System;
using System.Collections.Generic;
using System.Dynamic;

namespace MindBox_1
{
    public class AreaCalculatorDynamic
    {
        public AreaCalculatorDynamic()
        {
            //or we can move all the functs into a Dictionary<string, Delegate>()
            //but all this file is runtime and wierd :D
            DynamicModules = new ExpandoObject();
        }
        public static double GetAreaCircle(double radius)
        {
            if (radius < 0)
                throw new ArgumentException("Negative radius");
            return radius * radius * Math.PI;
        }

        public static double GetAreaTriang(List<Tuple<double, double>> points)
        {
            if (points.Count != 3)
                throw new ArgumentException("Wrong ammount of points for a triangle");

            double side1 = Helpers.GetLength(points[0], points[1]);
            double side2 = Helpers.GetLength(points[1], points[2]);
            double side3 = Helpers.GetLength(points[2], points[0]);

            return GetAreaTriang(side1, side2, side3);
        }

            public static double GetAreaTriang(double side1, double side2, double side3)
        {
            if (side1 <= 0 || side2 <= 0 || side3 <= 0)
                throw new ArgumentException("Sides cannot be negative");

            if (side1 + side2 <= side3 || side2 + side3 <= side1 || side3 + side1 <= side2)
                throw new ArgumentException("Sides don't form a triangle");

            if (side2 == Math.Max(side1, Math.Max(side2, side3)))
                (side2, side3) = (side3, side2);

            if (side1 == Math.Max(side1, Math.Max(side1, side3)))
                (side1, side3) = (side3, side1);

            if(RightTriangle(side1, side2, side3))
                return side1 * side2 / 2;

            double halfSum = (side1 + side2 + side3) / 2;

            return Math.Sqrt(halfSum * (halfSum - side1) * (halfSum - side2) * (halfSum - side3));
        }

        //Проверка на прямоугольность
        private static bool RightTriangle(double side1, double side2, double side3)
        {
            double squareSum = side1 * side1 + side2 * side2;

            if (squareSum == side3 * side3)
            {
                return true;
            }
            return false;
        }

        public static double GetAreaArbitraryPoly(List<Tuple<double, double>> points)
        {
            double totalArea = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (i == 0)
                {
                    totalArea += points[i].Item1 * (points[i + 1].Item2 - points[points.Count - 1].Item2);
                    continue;
                }

                if (i == points.Count - 1)
                {
                    totalArea += points[i].Item1 * (points[0].Item2 - points[i - 1].Item2);
                    continue;
                }
                totalArea += points[i].Item1 * (points[i + 1].Item2 - points[i - 1].Item2);
            }

            return totalArea/2;
        }
        public static double GetAreaArbitraryPoly(List<double> pointx, List<double> pointy)
        {
            if (pointx.Count != pointy.Count)
                throw new ArgumentException("Arrays differ in legnth");

            List<Tuple<double, double>> points = new List<Tuple<double, double>>(pointx.Count);

            for (int i = 0; i < pointx.Count; i++)
            {
                points.Add(new Tuple<double, double>(pointx[i], pointy[i]));
            }

            return GetAreaArbitraryPoly(points);
        }

        public dynamic DynamicModules;
    }
}
