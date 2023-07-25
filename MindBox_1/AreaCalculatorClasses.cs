using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;

namespace MindBox_1
{
    //base calculator
    public interface IFigureAreaCalculator
    {
        double GetArea();
    }

    //you can't change the figures in the instance of object :c
    public class TriangleAreaCalculator : IFigureAreaCalculator
    {
        private double side1 { get; }
        private double side2 { get; }
        private double side3 { get; }

        private double areaStored = -1;

        public TriangleAreaCalculator(double side1, double side2, double side3)
        {
            //can be moved to some side checking fun
            if (side1 <= 0)
                throw new ArgumentException($"Negative or zero side: {nameof(side1)}");

            if (side2 <= 0)
                throw new ArgumentException($"Negative or zero side: {nameof(side2)}");

            if (side3 <= 0)
                throw new ArgumentException($"Negative or zero side: {nameof(side3)}");

            if (side2 == Math.Max(side1, Math.Max(side2, side3)))
                (side2, side3) = (side3, side2);

            if (side1 == Math.Max(side1, Math.Max(side1, side3)))
                (side1, side3) = (side3, side1);

            if (side1 + side2 <= side3)
                throw new ArgumentException("Sides don't form a triangle");

            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
        }

        public TriangleAreaCalculator(List<Tuple<double, double>> points) 
            : this(Helpers.GetLength(points[0], points[1]), Helpers.GetLength(points[1], points[2]), Helpers.GetLength(points[2], points[0]))
            { }

        public double GetArea()
        {
            if (areaStored != -1)
                return areaStored;

            if (IsRightTriangle())
            {
                areaStored = side1 * side2 / 2;
                return areaStored;
            }

            double halfSum = (side1 + side2 + side3) / 2;

            areaStored = Math.Sqrt(halfSum * (halfSum - side1) * (halfSum - side2) * (halfSum - side3));

            return areaStored;
        }

        private bool IsRightTriangle()
        {
            double squareSum = side1 * side1 + side2 * side2;

            if (squareSum == side3 * side3)
            {
                return true;
            }
            return false;
        }

    }

    public class CircleAreaCalculator : IFigureAreaCalculator
    {
        private double areaStored = -1;

        private double radius;

        public CircleAreaCalculator(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Negative or zero radius");

            this.radius = radius;
        }

        public double GetArea()
        {
            if (areaStored != -1)
                return areaStored;
            areaStored = radius * radius * Math.PI;
            return areaStored;
        }
    }


    public class PolygonAreaCalculator: IFigureAreaCalculator
    {
        private double areaStored = -1;
        //in case we are not sure if points array won't be changed
        private List<Tuple<double, double>> points;

        //When we can be sure, that our point enumerable won't change --- we wont need a copy
        private IEnumerator<Tuple<double, double>> enumerator;

        public PolygonAreaCalculator(IEnumerable<Tuple<double, double>> points)
        {
            //count may iterate( could've used TryGetNonEnumeratedCount, though no garanties
            if (points is null || points.Count() < 3)
                throw new ArgumentException("Empty Polygon");
            //copy
            this.points = new List<Tuple<double, double>>();
            this.points.AddRange(points);

            //or create enumerator
            enumerator = points.GetEnumerator();
        }

        public PolygonAreaCalculator(List<double> pointx, List<double> pointy)
        {
            if (pointx.Count != pointy.Count)
                throw new ArgumentException("Arrays differ in legnth");

            points = new List<Tuple<double, double>>(pointx.Count);

            for (int i = 0; i < pointx.Count; i++)
            {
                points.Add(new Tuple<double, double>(pointx[i], pointy[i]));
            }
        }

        //when used with a copy
        public double GetArea()
        {
            if (areaStored != -1)
                return areaStored;
            double totalArea = 0;

            totalArea += Helpers.GetOrderedArea(points[0], points[1], points[points.Count - 1]);
            for (int i = 1; i < points.Count - 1; i++)
            {
                totalArea += Helpers.GetOrderedArea(points[i], points[i + 1], points[i - 1]);
            }
            totalArea += Helpers.GetOrderedArea(points[points.Count - 1], points[0], points[points.Count - 2]);
            areaStored = totalArea / 2;
            return areaStored;
        }

        //when used with enumerator
        public double GetArea1()
        {
            if (areaStored != -1)
                return areaStored;
            double totalArea = 0;
            
            enumerator.MoveNext();
            Tuple<double, double> beforeLast = enumerator.Current;
            enumerator.MoveNext();
            Tuple<double, double> last = enumerator.Current;
            while (enumerator.MoveNext())
            {
                totalArea += Helpers.GetOrderedArea(last, enumerator.Current, beforeLast);
                beforeLast = last;
                last = enumerator.Current;
            }
            //should check if it was possible in the first place, or we could've stored first value
            enumerator.Reset();

            enumerator.MoveNext();
            totalArea += Helpers.GetOrderedArea(last, enumerator.Current, beforeLast);

            //actual last element of the sequence
            beforeLast = last;
            //first element
            last = enumerator.Current;
            enumerator.MoveNext();
            totalArea += Helpers.GetOrderedArea(last, enumerator.Current, beforeLast);

            areaStored = totalArea / 2;
            return areaStored;
        }
    }
}
