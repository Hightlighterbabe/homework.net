using System;
using System.Linq;
using WebCalculatorWithDI.DataBase;

namespace WebCalculatorWithDI.Cache
{
    public static class ExpressionDbCache
    {
        private static readonly List<ExpressionEntity> _cache = new List<ExpressionEntity>();

        public static ExpressionEntity GetOrSet(
            ExpressionEntity expWithoutRes,
            Func<decimal> resultBuilder)
        {
            try
            {
                lock (_cache)
                {
                    return _cache.First(expression =>
                       expression.Expression == expWithoutRes.Expression);
                }
            }
            catch
            {
                expWithoutRes.Res = resultBuilder();
                lock (_cache)
                {
                    _cache.Add(expWithoutRes);
                }
                return expWithoutRes;
            }
        }
    }
}