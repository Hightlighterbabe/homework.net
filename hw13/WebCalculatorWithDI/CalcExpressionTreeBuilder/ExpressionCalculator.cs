using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using WebCalculatorWithDI.Controllers;

namespace WebCalculatorWithDI.CalcExpressionTreeBuilder
{
    public class ExpressionCalculator : IExpressionCalculator
    {
        public bool TryParseStringIntoExpression(string str, out Expression expression)
        {
            expression = Expression.Constant(0);
            if (!str.IsValidPlacementBrackets()) return false;

            var elements = new List<string>();
            var isValid = str.TrySplitMathExpressionByOperationsAndNumbers(elements);
            if (!isValid) return false;

            var output = new Stack<Expression>();
            var operations = new Stack<Operation>();

            foreach (var e in elements)
            {
                if (decimal.TryParse(e, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
                {
                    output.Push(Expression.Constant(number, typeof(decimal)));
                }
                else if (OperationExtensions.TryParse(e, out var operation))
                {
                    if (operation == Operation.RightBracket &&
                        !TryHighlightExpressionInBracketsInExpression(operations, output))
                        return false;

                    while (operations.Count != 0
                           && Priorities[operations.Peek()] >= Priorities[operation]
                           && operation != Operation.LeftBracket)
                    {
                        if (!TryJoinExpressionsByBinaryExpression(output, operations.Pop())) return false;
                    }

                    if (operation != Operation.RightBracket) operations.Push(operation);
                }
                else return false;
            }

            var result = operations
                .Select(operation => TryJoinExpressionsByBinaryExpression(output, operation))
                .All(valid => valid);

            if (!result) return false;

            expression = output.Pop();

            return true;
        }

        private static bool TryHighlightExpressionInBracketsInExpression(Stack<Operation> operations, Stack<Expression> output)
        {
            var currentOperation = operations.Pop();
            while (currentOperation != Operation.LeftBracket)
            {
                var isValid = TryJoinExpressionsByBinaryExpression(output, currentOperation);
                if (!isValid) return false;
                currentOperation = operations.Pop();
            }

            return true;
        }

        private static bool TryJoinExpressionsByBinaryExpression(Stack<Expression> output, Operation operation)
        {
            var isValid1 = output.TryPop(out var right);
            var isValid2 = output.TryPop(out var left);
            if (!isValid1 || !isValid2) return false;
            var isExp = operation.TryConvertToBinaryExpression(left, right, out var exp);
            output.Push(exp);
            return isExp;
        }

        private static readonly Dictionary<Operation, int> Priorities = new()
        {
            { Operation.LeftBracket, 0 },
            { Operation.RightBracket, 0 },
            { Operation.Plus, 1 },
            { Operation.Minus, 1 },
            { Operation.Division, 2 },
            { Operation.Multiplication, 2 }
        };
    }
}