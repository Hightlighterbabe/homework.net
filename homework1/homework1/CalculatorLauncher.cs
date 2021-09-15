using System;

namespace homework1
{
    public static class CalculatorLauncher
    {
        private const int ArgumentLength = 1;
        private const int ArgFormat = 2;

        public static int Launch(string[] args)
        {
            if (CheckArgLength(args))
            {
                return ArgumentLength;
            }

            var operation = args[1];
            if (TryParseOrQuit(args[0], out var val1)
                || TryParseOrQuit(args[2], out var val2))
            {
                return ArgFormat;
            }

            try
            {
                var result = Calculator.Calculate(operation, val1, val2);
                Console.WriteLine($"Result is: {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Can't divide to zero! Invalid arguments.");
            }

            return 0;
        }

        private static bool TryParseOrQuit(string arg, out int val1)
        {
            var isVal1 = int.TryParse(arg, out val1);
            if (isVal1)
            {
                return false;
            }

            Console.WriteLine($"Value is not int: {arg}");
            {
                return true;
            }

        }

        private static bool CheckArgLength(string[] args)
        {
            if (args.Length >= 3)
            {
                return false;
            }

            Console.WriteLine(
                $"The program requires 3 "
                + $"CLI arguments but {args.Length} provided");
            {
                return true;
            }
        }
    }
}