using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace hw12с.Models
{
    public class SlowExecutor 
    {
        public Expression StartVisiting(Expression expression)
        {
            return Visit((dynamic) expression);
        }

        protected virtual Expression Visit(BinaryExpression node)
        {
            Task.Delay(1000).Wait();
            var leftResult = Task.Run(
                () => (ConstantExpression) (
                    node.Left is BinaryExpression leftBinary
                        ? Visit(leftBinary)
                        : node.Left));
            var rightResult = Task.Run(
                () => (ConstantExpression) (
                    node.Right is BinaryExpression rightBinary
                        ? Visit(rightBinary)
                        : node.Right));
            Task.WaitAll(leftResult, rightResult);
            Console.WriteLine($"{leftResult.Result} {node.Method} {rightResult.Result}");
            var res = node.Method?.Invoke(default,
                new[] {leftResult.Result.Value, rightResult.Result.Value});
            return Expression.Constant(res);
        }

        protected virtual Expression Visit(UnaryExpression node)
        {
            var nodeResult = (node.Operand is BinaryExpression binary
                    ? Visit(binary)
                    : node.Operand)
                as ConstantExpression;
            return Expression.Constant(node.Method?.Invoke(default, new[] {nodeResult?.Value}));
        }
    }
}