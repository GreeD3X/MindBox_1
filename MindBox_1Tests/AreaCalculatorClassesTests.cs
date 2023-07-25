using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MindBox_1.Tests
{
    [TestClass()]
    public class AreaCalculatorClassesTests
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

            var calculator = new PolygonAreaCalculator(points);

            double res = calculator.GetArea();
            double res1 = calculator.GetArea1();

            Assert.AreEqual(res, 6, eps, "Failed to eval simple triangle with Gauss area formula");
            Assert.AreEqual(res1, 6, eps, "Failed to eval simple triangle with Gauss area formula");

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
            calculator = new PolygonAreaCalculator(points);
            res = calculator.GetArea();
            res1 = calculator.GetArea1();

            Assert.AreEqual(res, 46, eps, "Failed to get big figure area");
            Assert.AreEqual(res1, 46, eps, "Failed to get big figure area");

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

            var calculator = new PolygonAreaCalculator(pointsx, pointsy);

            double res = calculator.GetArea();
            double res1 = calculator.GetArea1();

            Assert.AreEqual(res, 6, eps, "Failed to eval simple triangle with Gauss area formula");
            Assert.AreEqual(res1, 6, eps, "Failed to eval simple triangle with Gauss area formula");

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

            calculator = new PolygonAreaCalculator(pointsx, pointsy);
            res = calculator.GetArea();
            res1 = calculator.GetArea1();

            Assert.AreEqual(res, 46, eps, "Failed to get big figure area");
            Assert.AreEqual(res1, 46, eps, "Failed to get big figure area");
        }

        [TestMethod()]
        public void GetAreaCircleTest()
        {
            double radius = 3 / Math.Sqrt(Math.PI);
            var calculator = new CircleAreaCalculator(radius);
            Assert.AreEqual(calculator.GetArea(), 9, eps);
        }

        [TestMethod()]
        public void GetAreaTriangleTest()
        {
            var calculator = new TriangleAreaCalculator(3, 4, 5);
            Assert.AreEqual(calculator.GetArea(), 6, eps, "Failed to calculate right traingle area");
            calculator = new TriangleAreaCalculator(1, 1, 1);
            Assert.AreEqual(calculator.GetArea(), Math.Sqrt(3) / 4, eps, "Failed to calculate equilateral triangle area");
            calculator = new TriangleAreaCalculator(Math.Sqrt(10), Math.Sqrt(10), 2);
            Assert.AreEqual(calculator.GetArea(), 3, eps, "Failed to calculate isosceles triangle area");
        }
    }
}