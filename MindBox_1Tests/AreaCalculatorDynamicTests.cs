using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MindBox_1.Tests
{
    [TestClass()]
    public class AreaCalculatorDynamicTests
    {
        private double eps = 0.00001;

        [TestMethod()]
        public void GetAreaArbitraryPolyTest()
        {
            List<Tuple<double, double>> points = new List<Tuple<double, double>>(3);
            {
                points.Add(new Tuple<double, double>(0, 0));
                points.Add(new Tuple<double, double>(3, 0));
                points.Add(new Tuple<double, double>(0, 4));
            }

            double res = AreaCalculatorDynamic.GetAreaArbitraryPoly(points);

            Assert.AreEqual(res, 6, eps, "Failed to eval simple triangle with Gauss area formula");

            points = new List<Tuple<double, double>>(10);
            {
                points.Add(new Tuple<double, double>(0, 0));
                points.Add(new Tuple<double, double>(5, 0));
                points.Add(new Tuple<double, double>(5, 2));
                points.Add(new Tuple<double, double>(7, 2));
                points.Add(new Tuple<double, double>(7, 4));
                points.Add(new Tuple<double, double>(5, 4));
                points.Add(new Tuple<double, double>(5, 6));
                points.Add(new Tuple<double, double>(3, 6));
                points.Add(new Tuple<double, double>(3, 8));
                points.Add(new Tuple<double, double>(0, 12));
            }

            res = AreaCalculatorDynamic.GetAreaArbitraryPoly(points);

            Assert.AreEqual(res, 46, eps, "Failed to get big figure area");

        }

        [TestMethod()]
        public void GetAreaArbitraryPolyTest1()
        {
            List<double> pointsx = new List<double>(3);
            List<double> pointsy = new List<double>(3);
            {
                pointsx.Add(0);
                pointsy.Add(0);
                pointsx.Add(3);
                pointsy.Add(0);
                pointsx.Add(0);
                pointsy.Add(4);
            }

            double res = AreaCalculatorDynamic.GetAreaArbitraryPoly(pointsx, pointsy);

            Assert.AreEqual(res, 6, eps, "Failed to eval simple triangle with Gauss area formula");

            pointsx = new List<double>(10);
            pointsy = new List<double>(10);
            {
                pointsx.Add(0);
                pointsy.Add(0);
                pointsx.Add(5);
                pointsy.Add(0);
                pointsx.Add(5);
                pointsy.Add(2);
                pointsx.Add(7);
                pointsy.Add(2);
                pointsx.Add(7);
                pointsy.Add(4);
                pointsx.Add(5);
                pointsy.Add(4);
                pointsx.Add(5);
                pointsy.Add(6);
                pointsx.Add(3);
                pointsy.Add(6);
                pointsx.Add(3);
                pointsy.Add(8);
                pointsx.Add(0);
                pointsy.Add(12);
            }

            res = AreaCalculatorDynamic.GetAreaArbitraryPoly(pointsx, pointsy);

            Assert.AreEqual(res, 46, eps, "Failed to get big figure area");
        }

        [TestMethod()]
        public void GetAreaCircleTest()
        {
            double radius = 3 / Math.Sqrt(Math.PI);
            Assert.AreEqual(AreaCalculatorDynamic.GetAreaCircle(radius), 9, eps);
        }

        [TestMethod()]
        public void GetAreaTriangleTest()
        {
            Assert.AreEqual(AreaCalculatorDynamic.GetAreaTriang(3, 4, 5), 6, eps, "Failed to calculate right traingle area");
            Assert.AreEqual(AreaCalculatorDynamic.GetAreaTriang(1, 1, 1), Math.Sqrt(3) / 4, eps, "Failed to calculate equilateral triangle area");
            Assert.AreEqual(AreaCalculatorDynamic.GetAreaTriang(Math.Sqrt(10), Math.Sqrt(10), 2), 3, eps, "Failed to calculate isosceles triangle area");
        }

        [TestMethod()]
        public void GetAreadynamicTest()
        {
            AreaCalculatorDynamic calc = new AreaCalculatorDynamic();
            calc.DynamicModules.GetAreaCustom = (Func<double, double, double, double>)((double side1, double side2, double side3) =>
            {
                return AreaCalculatorDynamic.GetAreaTriang(side1, side2, side3);
            });
            Assert.AreEqual(calc.DynamicModules.GetAreaCustom(3, 4, 5), 6, eps);

            List<Tuple<double, double>> points = new List<Tuple<double, double>>(10);
            {
                points.Add(new Tuple<double, double>(0, 0));
                points.Add(new Tuple<double, double>(5, 0));
                points.Add(new Tuple<double, double>(5, 2));
                points.Add(new Tuple<double, double>(7, 2));
                points.Add(new Tuple<double, double>(7, 4));
                points.Add(new Tuple<double, double>(5, 4));
                points.Add(new Tuple<double, double>(5, 6));
                points.Add(new Tuple<double, double>(3, 6));
                points.Add(new Tuple<double, double>(3, 8));
                points.Add(new Tuple<double, double>(0, 12));
            }

            calc.DynamicModules.GetAreaCustom = (Func<List<Tuple<double, double>>, double>)((List<Tuple<double, double>> points) =>
            {
                return AreaCalculatorDynamic.GetAreaArbitraryPoly(points);
            });
            Assert.AreEqual(calc.DynamicModules.GetAreaCustom(points), 46, eps);
        }
    }
}