using System;
namespace homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            var argument1 = Parser.ParseNumber(args[0]);
            var operation = Parser.ParseOperation(args[1]);
            var argument2 = Parser.ParseNumber(args[2]);

            var result = Calculator.Calculate(argument1, argument2, operation);
            

            Console.WriteLine($"Result is:{result}");
        }
    }
}