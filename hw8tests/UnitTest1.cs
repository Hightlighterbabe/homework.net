using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using homework8;
using homework8.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace hw8tests
{
    public class UnitTest1
    {
        private NewCalculator calculatorrr = new NewCalculator();

        [Theory]
        [InlineData(1, 2, "+", 3)]
        [InlineData(1, 2, "-", -1)]
        [InlineData(1, 2, "*", 2)]
        [InlineData(1, 2, "/", 0.5)]
        public void Calculat(decimal val1, decimal val2, string op, decimal res)
        {
            var args = calculatorrr.Calculate(new CalcArguments
            {
                Val1 = val1.ToString(),
                Val2 = val2.ToString(),
                Operation = op
            });

            Assert.Equal(args, res);
        }

        public class IntegrationTest
        {
            private WebApplicationFactory<Startup> factory;
            private HttpClient client;

            public IntegrationTest()
            {
                factory = new WebApplicationFactory<Startup>();
                client = factory.CreateClient();
            }

            private async Task<decimal> Action(decimal val1, decimal val2, string operation)
            {
                var response = await client.GetAsync($"http://localhost:5000/calculate?val1={val1}&val2={val2}&Operation={operation}");

                var strNumber = await response.Content.ReadAsStringAsync();
                decimal parsed;
                try
                {
                    parsed = decimal.Parse(strNumber, CultureInfo.InvariantCulture);
                }
                catch
                {
                    throw new Exception($"cant parse, the number is {strNumber}");
                }

                return parsed;
            }

            private async Task<decimal> Sum(decimal val1, decimal val2) => await Action(val1, val2, "+");
            private async Task<decimal> Minus(decimal val1, decimal val2) => await Action(val1, val2, "-");
            private async Task<decimal> Multiply(decimal val1, decimal val2) => await Action(val1, val2, "*");
            private async Task<decimal> Divided(decimal val1, decimal val2) => await Action(val1, val2, "/");

            private static void CheckEquality(decimal val1, decimal val2) => Assert.True(Math.Round(val1 - val2) < 0.0001m);

            [Theory]
            [InlineData(1, 2)]
            [InlineData(3, 2)]
            [InlineData(12.4, 2.6)]
            public async Task Calcul(decimal val1, decimal val2)
            {
                CheckEquality(await Sum(val1, val2), val1 + val2);
                CheckEquality(await Minus(val1, val2), val1 - val2);
                CheckEquality(await Multiply(val1, val2), val1 * val2);
                CheckEquality(await Divided(val1, val2), val1 / val2);
            }
        }
    }
}