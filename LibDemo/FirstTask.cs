using MindBox_1;
using System;

public class FirstTask
{
    static void Main(string[] args)
    {
        AreaCalculator calc = new AreaCalculator();
        calc.DynamicModules.GetAreaCustom = (Func<double, double>)((double radius) =>
        {
            return AreaCalculator.GetAreaCircle(radius) + radius*radius;
        });

        Console.WriteLine(AreaCalculator.GetAreaCircle(2));
        Console.WriteLine(calc.DynamicModules.GetAreaCustom(2));
        Console.WriteLine(args.Length);
    }
}
