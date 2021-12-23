using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebCalculatorWithDI.CalcExpressionTreeBuilder;

namespace WebCalculatorWithDI.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet, Route("Calcs")]
        public IActionResult Calcs([FromServices] CalculatorVisitorCache visitor, [FromServices] IExpressionCalculator calculator, string expressionString)
        {
            if (!calculator.TryParseStringIntoExpression(expressionString, out var exp))
            {
                return Ok("Error");
            }

            var result = visitor.Visit(exp);
            return Ok(result.ToString());
        }
    }
}
