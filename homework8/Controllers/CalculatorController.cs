using System;
using Microsoft.AspNetCore.Mvc;
using homework8.Models;

namespace homework8.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet, Route("calculate")]
        public IActionResult Calcul([FromServices] ICalculator calculator, [FromQuery] CalcArguments args)
        {
            if (args.Operation ==null)
            {
                args.Operation = "+";
            }

            try
            {
                return Ok(calculator.Calculate(args));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ObjectResult(e.Message)
                {
                    StatusCode = 450
                };
            }

        }
    }
    public class CalcArguments
    {
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public string Operation { get; set; }
    }
}