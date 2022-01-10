using BenchmarkDotNet.Attributes;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenchmarkForCalculator
{
    [MinColumn]
    [MaxColumn]
    [StdDevColumn]
    [StdErrorColumn]
    [MedianColumn]
    public class Benchmark
    {

        private HttpClient _clientCSharp;
        private HttpClient _clientFSharp;

        private const string FSharpUrl = "https://localhost:5001/?";
        private const string CSharpUrl = "https://localhost:5001/Calculate?";

        [GlobalSetup]
        public void GlobalSetUp()
        {
            _clientCSharp = new CSharpHostBuilder().CreateClient();
            _clientFSharp = new FSharpHostBuilder().CreateClient();
        }

        public string GetExpressionString(string val1, string val2, string op) =>
            $"val1={val1}&val2={val2}op={op}&";

        [Benchmark(Description = "F# Plus")]
        public async Task PlusFSharp()
            => await _clientFSharp.GetAsync(FSharpUrl + GetExpressionString("150", "%2b", "1849"));

        [Benchmark(Description = "C# Plus")]
        public async Task PlusCSharp()
            => await _clientFSharp.GetAsync(CSharpUrl + GetExpressionString("150", "%2b", "1849"));


        [Benchmark(Description = "F# Minus")]
        public async Task MinusFSharp()
            => await _clientFSharp.GetAsync(FSharpUrl + GetExpressionString("1999", "-", "991"));

        [Benchmark(Description = "C# Minus")]
        public async Task MinusCSharp()
            => await _clientCSharp.GetAsync(CSharpUrl + GetExpressionString("1999", "-", "991"));


        [Benchmark(Description = "F# Multiply")]
        public async Task MultFSharp()
            => await _clientFSharp.GetAsync(FSharpUrl + GetExpressionString("111", "*", "17"));

        [Benchmark(Description = "C# Multiply")]
        public async Task MultCSharp()
            => await _clientCSharp.GetAsync(CSharpUrl + GetExpressionString("111", "*", "17"));


        [Benchmark(Description = "F# Divide")]
        public async Task DivFSharp()
            => await _clientFSharp.GetAsync(FSharpUrl + GetExpressionString("2323", "/", "101"));

        [Benchmark(Description = "C# Divide")]
        public async Task DivCSharp()
            => await _clientCSharp.GetAsync(CSharpUrl + GetExpressionString("2323", "/", "101"));
    }
}