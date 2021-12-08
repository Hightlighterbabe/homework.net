using System;
using homework8.Controllers;
using homework8.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace homework8
{
    public class NewCalculator: ICalculator
    {
        public decimal Calculate(CalcArguments args)
        {
            var val1 = decimal.Parse(args.Val1);
            var val2 = decimal.Parse(args.Val2);

            return args.Operation switch
            {
                "+" => val1 + val2,
                "-" => val1 - val2,
                "/" => val1 / val2,
                "*" => val1 * val2,
                _ => throw new Exception("Wrong operation")
            };
        }
        
    }
}