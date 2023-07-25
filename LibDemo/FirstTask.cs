using MindBox_1;
using System;

public class FirstTask
{
    static void Main(string[] args)
    {
        AreaCalculatorDynamic calc = new AreaCalculatorDynamic();

        calc.DynamicModules.GetAreaCustom = (Func<double, double>)((double radius) =>
        {
            return AreaCalculatorDynamic.GetAreaCircle(radius) + radius*radius;
        });

        Console.WriteLine(AreaCalculatorDynamic.GetAreaCircle(2));
        Console.WriteLine(calc.DynamicModules.GetAreaCustom(2));
        Console.WriteLine(args.Length);
    }
}
