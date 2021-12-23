using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebCalculatorWithDI.Cache;
using WebCalculatorWithDI.DataBase;

namespace WebCalculatorWithDI.CalcExpressionTreeBuilder
{
    public class CalculatorVisitorCache : CalculatorVisitor
    {
        private CalculatorVisitor _baseVisitor = new CalculatorVisitor();

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var expressionEntity = new ExpressionEntity()
            {
                Expression = node.ToString(),
            };

            return Expression.Constant(ExpressionDbCache.GetOrSet(expressionEntity, () => {
                var left = Visit(node.Left);
                var right = Visit(node.Right);
                return (decimal)((ConstantExpression)_baseVisitor.Visit(Expression.MakeBinary(node.NodeType, left, right)))?.Value!;
            }).Res);
        }
    }
}