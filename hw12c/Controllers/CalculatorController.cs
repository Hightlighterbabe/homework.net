using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using hw12с.Models;
using Microsoft.Extensions.Logging;

namespace hw12с.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet, Route("calculate")]
        public IActionResult Calculate(
            [FromServices] SlowExecutor slowExecutor,
            [FromServices] IExpressionCalculator expressionCalculator,
            [FromServices] ExceptionHandler exceptionHandler,
            string expressionString)
        {
            string AddPluses(string str) =>
                str.Aggregate(new StringBuilder(), (builder, c) => builder.Append(c switch
                {
                    ' ' => "+",
                    '-' => builder.Length is not 0 && !"()*/+-".Contains(builder[^1]) ? "+-" : "-",
                    _ => c.ToString()
                })).ToString();

            expressionString = AddPluses(expressionString);
            Console.WriteLine();
            Console.WriteLine($"получено выражение:\n\t{expressionString}");

            try
            {
                var expression = expressionCalculator.FromString(expressionString);
                var res1 = expressionCalculator.ExecuteSlowly(slowExecutor, expression);
                Console.WriteLine(
                    $"вывод результата через ExpressionCalculator:\n\t{res1?.ToString(CultureInfo.InvariantCulture) ?? "ошибка"}");
                return Ok(res1?.ToString(CultureInfo.InvariantCulture) ?? "ошибка");
            }
            catch(Exception e)
            {
                exceptionHandler.DoHandle(LogLevel.Error, e);
                return BadRequest();
            }
        }
    }
}