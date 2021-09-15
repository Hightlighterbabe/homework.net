using System;
namespace homework1
{
    public static class Calculator
    {
        public enum Operation
        {
            Plus, 
            Minus,
            Multiply,
            Divide,
            Unknown
        }

        public static Exception WrongOperation = new Exception("Operation is unknown");
        
        public static DivideByZeroException DivisionByZero = new DivideByZeroException("Num2 is zero");
        
        
        public static int Calculate(int num1, int num2, Operation operation)
        {
            int result = 0;
            switch (operation)
            {
                case Operation.Plus:
                    result = num1 + num2;
                    break;
                case Operation.Minus:
                    result = num1 - num2;
                    break;
                case Operation.Multiply:
                    result = num1 * num2;
                    break;
                case Operation.Divide:
                    if (num2 == 0)
                    {
                        throw DivisionByZero;
                    }
                    result = num1 / num2;
                    break;
                default:
                    throw WrongOperation;
            }

            return result;
        }
    }
}