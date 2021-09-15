using System;
namespace homework1
{ 
    public static class Parser
        {
            public static Exception ParseNumberFail = new Exception("Value is not integer");

            public static Exception ParseOperationFail = new Exception("Operation is not correct");

            public static int ParseNumber(string value)
            {
                var success = int.TryParse(value, out var result);
                if (!success)
                {
                    throw ParseNumberFail;
                }

                return result;
            }

            public static Calculator.Operation ParseOperation(string value)
            {
                var result = value switch
                {
                    "+" => Calculator.Operation.Plus,
                    "-" => Calculator.Operation.Minus,
                    "*" => Calculator.Operation.Multiply,
                    "/" => Calculator.Operation.Divide,
                    _ => throw ParseOperationFail
                };

                return result;
            }
        }
}