using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace hw9.Models
{
    public class SlowExecutor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Task.Delay(1000).Wait();
            var leftResult = Task.Run(
                () => (ConstantExpression) (
                    node.Left is BinaryExpression leftBinary
                        ? VisitBinary(leftBinary)
                        : node.Left));
            var rightResult = Task.Run(
                () => (ConstantExpression) (
                    node.Right is BinaryExpression rightBinary
                        ? VisitBinary(rightBinary)
                        : node.Right));
            Task.WaitAll(leftResult, rightResult);
            Console.WriteLine($"{leftResult.Result} {node.Method} {rightResult.Result}");
            var res = node.Method?.Invoke(default,
                new[] {leftResult.Result.Value, rightResult.Result.Value});
            return Expression.Constant(res);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var nodeResult = (node.Operand is BinaryExpression binary
                    ? VisitBinary(binary)
                    : node.Operand)
                as ConstantExpression;
            return Expression.Constant(node.Method?.Invoke(default, new[] {nodeResult?.Value}));
        }
    }
}