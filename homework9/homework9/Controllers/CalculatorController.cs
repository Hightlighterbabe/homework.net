using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using hw9.Models;

namespace hw9.Controllers
{
    public class CalculatorController : Controller
    {
        /// <summary>
        /// пробел считается за '+', поэтому пробелы запрещены
        /// </summary>
        /// <param name="expressionString"></param>
        /// <returns></returns>
        [HttpGet, Route("calculate")]
        public IActionResult Calculate(string expressionString)
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

            var expression = ExpressionCalculator.FromString(expressionString);
            var res1 = ExpressionCalculator.ExecuteSlowly(expression);
            Console.WriteLine($"вывод результата через ExpressionCalculator:\n\t{res1?.ToString(CultureInfo.InvariantCulture) ?? "ошибка"}");
            return Ok(res1?.ToString(CultureInfo.InvariantCulture) ?? "ошибка");
        }
    }
}
