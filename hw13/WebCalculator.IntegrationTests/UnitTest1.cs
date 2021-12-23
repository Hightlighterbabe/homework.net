using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using WebCalculatorWithDI;
using Xunit;

namespace Tests8
{
    public class HostBuilder : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
            => Host
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(a => a
                    .UseStartup<Startup>()
                    .UseTestServer());
    }

    public class IntegrationTests : IClassFixture<HostBuilder>
    {
        private readonly HttpClient _client;

        public IntegrationTests(HostBuilder fixture)
        {
            _client = fixture.CreateClient();
        }

        private static readonly Uri Uri = new("https://localhost:7145/Calcs");
        private const string Error = "Error";

        [Theory]
        [InlineData("10%2B6", "16")]
        [InlineData("10-50", "-40")]
        [InlineData("7*5", "35")]
        [InlineData("63/3", "21")]
        [InlineData("(100/50)%2B2", "4")]
        public async Task CalculatorController_ReturnCorrectResult(string expression, string expected)
            => await MakeTestAsync(expression, expected);

        [Theory]
        [InlineData("sometext")]
        [InlineData("()1-1(")]
        [InlineData("((1-1)")]
        [InlineData("(1-1)/")]
        [InlineData("((1-1)()")]
        public async Task CalculatorController_ReturnError(string expression)
            => await MakeTestAsync(expression, Error);

        private async Task MakeTestAsync(string expression, string expected)
        {
            var str = $"?expressionString={expression}";
            using var response = await _client.GetAsync(Uri+str);
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("(1001%2b999)/2000*20-1")]
        private async Task CalculatorController_ParallelTest(string expression)
        {
            var watch = new Stopwatch();
            var str = $"expression={expression}";
            watch.Start();
            using var response = await _client.GetAsync(Uri + str);
            watch.Stop();
            var result = watch.ElapsedMilliseconds;
            Assert.True(result - 1000 < 500);
        }
    }
}