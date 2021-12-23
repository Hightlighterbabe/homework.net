using System.Linq.Expressions;

namespace hw12с.Models
{
    public interface IExpressionCalculator
    {
        public Expression FromString(string str);
        public decimal? ExecuteSlowly(SlowExecutor slowExecutor, Expression expression);
    }
}