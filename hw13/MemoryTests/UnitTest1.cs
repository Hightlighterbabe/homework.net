using System.Net.Http;
using System.Text;
using JetBrains.dotMemoryUnit;
using Microsoft.AspNetCore.Mvc.Testing;
using WebCalculatorWithDI;
using Xunit;
using Xunit.Abstractions;

namespace Task3;

public class UnitTest1
{
    private readonly ITestOutputHelper _helper;
    private readonly HttpClient _client = new WebApplicationFactory<Startup>().CreateClient();

    public UnitTest1(ITestOutputHelper helper)
    {
        _helper = helper;
        DotMemoryUnitTestOutput.SetOutputMethod(_helper.WriteLine);
    }

    [DotMemoryUnit(CollectAllocations = true, FailIfRunWithoutSupport = false)]
    [Fact]
    public void Test1()
    {
        var point = dotMemory.Check();

        long allocated = 0;
        for (long i = 0; i < 1e5; ++i)
        {
            var expression = $"{i}%2b{i}";
            allocated += Encoding.UTF8.GetBytes(expression).Length;
            _client.GetAsync($"https://localhost:7145/Calcs?expressionString={expression}").GetAwaiter().GetResult();
        }

        dotMemory.Check(memory =>
        {
            _helper.WriteLine(memory.GetTrafficFrom(point).CollectedMemory.SizeInBytes.ToString());
            _helper.WriteLine(allocated.ToString());
            Assert.True(memory.GetTrafficFrom(point).CollectedMemory.SizeInBytes >= allocated);
        });
    }
}