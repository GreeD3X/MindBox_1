using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MindBox_1.Tests
{
    [TestClass()]
    public class AreaCalculatorTests
    {
        private double eps = 0.00001;

        [TestMethod()]
        public void GetAreaArbitraryPolyTest()
        {
            Tuple<double, double>[] points = new Tuple<double, double>[3];
            {
                points[0] = new Tuple<double, double>(0, 0);
                points[1] = new Tuple<double, double>(3, 0);
                points[2] = new Tuple<double, double>(0, 4);
            }

            double res = AreaCalculator.GetAreaArbitraryPoly(points);

            Assert.AreEqual(res, 6, eps, "Failed to eval simple triangle with Gauss area formula");

            points = new Tuple<double, double>[10];
            {
                points[0] = new Tuple<double, double>(0, 0);
                points[1] = new Tuple<double, double>(5, 0);
                points[2] = new Tuple<double, double>(5, 2);
                points[3] = new Tuple<double, double>(7, 2);
                points[4] = new Tuple<double, double>(7, 4);
                points[5] = new Tuple<double, double>(5, 4);
                points[6] = new Tuple<double, double>(5, 6);
                points[7] = new Tuple<double, double>(3, 6);
                points[8] = new Tuple<double, double>(3, 8);
                points[9] = new Tuple<double, double>(0, 12);
            }

            res = AreaCalculator.GetAreaArbitraryPoly(points);

            Assert.AreEqual(res, 46, eps, "Failed to get big figure area");

        }

        [TestMethod()]
        public void GetAreaArbitraryPolyTest1()
        {
            double[] pointsx = new double[3];
            double[] pointsy = new double[3];
            {
                pointsx[0] = 0;
                pointsy[0] = 0;
                pointsx[1] = 3;
                pointsy[1] = 0;
                pointsx[2] = 0;
                pointsy[2] = 4;
            }

            double res = AreaCalculator.GetAreaArbitraryPoly(pointsx, pointsy);

            Assert.AreEqual(res, 6, eps, "Failed to eval simple triangle with Gauss area formula");

            pointsx = new double[10];
            pointsy = new double[10];
            {
                pointsx[0] = 0;
                pointsy[0] = 0;
                pointsx[1] = 5;
                pointsy[1] = 0;
                pointsx[2] = 5;
                pointsy[2] = 2;
                pointsx[3] = 7;
                pointsy[3] = 2;
                pointsx[4] = 7;
                pointsy[4] = 4;
                pointsx[5] = 5;
                pointsy[5] = 4;
                pointsx[6] = 5;
                pointsy[6] = 6;
                pointsx[7] = 3;
                pointsy[7] = 6;
                pointsx[8] = 3;
                pointsy[8] = 8;
                pointsx[9] = 0;
                pointsy[9] = 12;
            }

            res = AreaCalculator.GetAreaArbitraryPoly(pointsx, pointsy);

            Assert.AreEqual(res, 46, eps, "Failed to get big figure area");

        }

        [TestMethod()]
        public void GetAreaCircleTest()
        {
            double radius = 3 / Math.Sqrt(Math.PI);
            Assert.AreEqual(AreaCalculator.GetAreaCircle(radius), 9, eps);
        }

        [TestMethod()]
        public void GetAreaTriangleTest()
        {
            Assert.AreEqual(AreaCalculator.GetAreaTriang(3, 4, 5), 6, eps, "Failed to calculate right traingle area");
            Assert.AreEqual(AreaCalculator.GetAreaTriang(1, 1, 1), Math.Sqrt(3) / 4, eps, "Failed to calculate equilateral triangle area");
            Assert.AreEqual(AreaCalculator.GetAreaTriang(Math.Sqrt(10), Math.Sqrt(10), 2), 3, eps, "Failed to calculate isosceles triangle area");
        }

        [TestMethod()]
        public void GetAreadynamicTest()
        {
            AreaCalculator calc = new AreaCalculator();
            calc.DynamicModules.GetAreaCustom = (Func<double, double, double, double>)((double side1, double side2, double side3) =>
            {
                return AreaCalculator.GetAreaTriang(side1, side2, side3);
            });
            Assert.AreEqual(calc.DynamicModules.GetAreaCustom(3, 4, 5), 6, eps);

            Tuple<double, double>[] points = new Tuple<double, double>[10];
            {
                points[0] = new Tuple<double, double>(0, 0);
                points[1] = new Tuple<double, double>(5, 0);
                points[2] = new Tuple<double, double>(5, 2);
                points[3] = new Tuple<double, double>(7, 2);
                points[4] = new Tuple<double, double>(7, 4);
                points[5] = new Tuple<double, double>(5, 4);
                points[6] = new Tuple<double, double>(5, 6);
                points[7] = new Tuple<double, double>(3, 6);
                points[8] = new Tuple<double, double>(3, 8);
                points[9] = new Tuple<double, double>(0, 12);
            }

            calc.DynamicModules.GetAreaCustom = (Func<Tuple<double, double>[], double>)((Tuple<double, double>[] points) =>
            {
                return AreaCalculator.GetAreaArbitraryPoly(points);
            });
            Assert.AreEqual(calc.DynamicModules.GetAreaCustom(points), 46, eps);
        }
    }
}