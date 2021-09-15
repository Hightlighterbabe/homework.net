using System;
namespace homework1
{ 
    public static class Parser
        {
            public static Exception ParserNumberFail = new Exception("Value is not integer");

            public static Exception ParserOperationFail = new Exception("Operation is not correct");

            public static int ToParseNumber(string value)
            {
                var success = int.TryParse(value, out var result);
                if (!success)
                {
                    throw ParserNumberFail;
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
                    _ => throw ParserOperationFail
                };

                return result;
            }
        }
}