using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using hw9.DataBases;

namespace hw9.Models
{
    public class CachedExecutor : SlowExecutor
    {
        private readonly CacheExpressions _cache;

        public CachedExecutor(CacheExpressions cache) =>
            _cache = cache;
        
        protected override Expression Visit(BinaryExpression node)
        {
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

            var expression = new ExpressionsComputed
            {
                Val1 = (decimal) leftResult.Result.Value!,
                Val2 = (decimal) rightResult.Result.Value!,
                Op = node.NodeType switch
                {
                    ExpressionType.Add => Operation.Plus,
                    ExpressionType.Subtract => Operation.Minus,
                    ExpressionType.Multiply => Operation.Mult,
                    ExpressionType.Divide => Operation.Div,
                    _ => throw new Exception("иди те в баню")
                }
            };
            expression = _cache.GetOrSet(expression, () =>
            {
                Task.Delay(1000).Wait();
                return expression.Op switch
                {
                    Operation.Plus => expression.Val1 + expression.Val2,
                    Operation.Minus => expression.Val1 - expression.Val2,
                    Operation.Mult => expression.Val1 * expression.Val2,
                    Operation.Div => expression.Val1 / expression.Val2,
                    _ => throw new Exception("иди нахер пж")
                };
            });

            Console.WriteLine($"{leftResult.Result} {node.Method} {rightResult.Result}");
            return Expression.Constant(expression.Res);
        }
    }
}