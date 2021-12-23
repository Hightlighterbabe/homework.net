using System;
using System.Linq.Expressions;

namespace WebCalculatorWithDI.CalcExpressionTreeBuilder
{

    public enum Operation
    {
        Plus,
        Minus,
        Division,
        Multiplication,
        LeftBracket,
        RightBracket
    }

    public static class OperationExtensions
    {
        public static bool TryParse(string str, out Operation operation)
        {
            operation = str switch
            {
                "+" => Operation.Plus,
                "-" => Operation.Minus,
                "*" => Operation.Multiplication,
                "/" => Operation.Division,
                "(" => Operation.LeftBracket,
                ")" => Operation.RightBracket,
                _ => Operation.Plus
            };

            return operation != Operation.Plus || str == "+";
        }

        public static bool TryConvertToBinaryExpression(this Operation operation,
            Expression left,
            Expression right,
            out BinaryExpression expression)
        {
            expression = Expression.Add(left, right);
            if (operation is Operation.LeftBracket or Operation.RightBracket) return false;
            expression = operation switch
            {
                Operation.Plus => Expression.Add(left, right),
                Operation.Minus => Expression.Subtract(left, right),
                Operation.Division => Expression.Divide(left, right),
                Operation.Multiplication => Expression.Multiply(left, right),
                _ => throw new NotImplementedException()
            };

            return true;
        }
    }
}