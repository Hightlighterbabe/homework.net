using System;
using System.Linq;
using hw9.DataBases;

namespace hw9.Models
{
    public class CacheExpressions
    {
        private readonly ContextExpressionsComputed _context;

        public CacheExpressions(ContextExpressionsComputed context) =>
            _context = context;

        public ExpressionsComputed GetOrSet(
            ExpressionsComputed expWithoutRes,
            Func<decimal> resultBuilder)
        {
            try
            {
                lock (_context)
                {
                    return _context.Items.First(expression =>
                        expression.Val1 == expWithoutRes.Val1 &&
                        expression.Val2 == expWithoutRes.Val2 &&
                        expression.Op == expWithoutRes.Op);
                }
            }
            catch
            {
                expWithoutRes.Res = resultBuilder();
                lock (_context)
                {
                    _context.Items.Add(expWithoutRes);
                    _context.SaveChanges();
                }
                return expWithoutRes;
            }
        }
    }
}
