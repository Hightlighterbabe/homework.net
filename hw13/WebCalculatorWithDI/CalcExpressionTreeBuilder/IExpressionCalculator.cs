using System.Linq.Expressions;

namespace WebCalculatorWithDI.Controllers
{
    public interface IExpressionCalculator
    {
        public bool TryParseStringIntoExpression(string str, out Expression expression);
    }
}