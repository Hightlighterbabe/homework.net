using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebCalculatorWithDI.CalcExpressionTreeBuilder
{
     public class CalculatorVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = Task.Run(() => Visit(node.Left));
            var right = Task.Run(() => Visit(node.Right));

            Thread.Sleep(1000);
            Task.WhenAll(left, right).Wait();

            var leftResult = (decimal)((ConstantExpression)left.Result)?.Value!;
            var rightResult = (decimal)((ConstantExpression)right.Result)?.Value!;

            var operation = node.NodeType switch
            {
                ExpressionType.Add => "+",
                ExpressionType.Subtract => "-",
                ExpressionType.Multiply => "*",
                ExpressionType.Divide => "/"
            };

            var result = (decimal)CalculatorF.Program.GetResult(new string[] {
                leftResult.ToString(),
                operation,
                rightResult.ToString()})
                .Value;

            return Expression.Constant(result);
        }
    }
}
