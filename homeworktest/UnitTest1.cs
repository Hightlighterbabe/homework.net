using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using homework5;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace homeworktest5._1
{
    [TestClass]
    public class UnitTest1
    {
        private WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        [TestInitialize]
        public void SetFactory()
        {
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }
        
        [TestMethod]
        public async Task TestMethodPlus()
        {
            var response = await _client.GetAsync($"http://localhost:5000/calc?Operation=plus&Num1=1&Num2=2");
            var res = await response.Content.ReadAsStringAsync();
            var num = decimal.Parse(res, CultureInfo.InvariantCulture);
            Assert.AreEqual(3m, num);
        }
        [TestMethod]
        public async Task TestMethodMinus()
        {
            var response = await _client.GetAsync($"http://localhost:5000/calc?Operation=minus&Num1=4&Num2=3");
            var res = await response.Content.ReadAsStringAsync();
            var num = decimal.Parse(res, CultureInfo.InvariantCulture);
            Assert.AreEqual(1m, num);
        }
        [TestMethod]
        public async Task TestMethodDivide()
        {
            var response = await _client.GetAsync($"http://localhost:5000/calc?Operation=divide&Num1=4&Num2=2");
            var res = await response.Content.ReadAsStringAsync();
            var num = decimal.Parse(res, CultureInfo.InvariantCulture);
            Assert.AreEqual(2m, num);
        }
        [TestMethod]
        public async Task TestMethodMultiply()
        {
            var response = await _client.GetAsync($"http://localhost:5000/calc?Operation=multiply&Num1=1&Num2=2");
            var res = await response.Content.ReadAsStringAsync();
            var num = decimal.Parse(res, CultureInfo.InvariantCulture);
            Assert.AreEqual(2m, num);
        }
        [TestMethod]
        public async Task TestMethodDivideByZero()
        {
            var response = await _client.GetAsync($"http://localhost:5000/calc?Operation=divide&Num1=1&Num2=0");
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual($"\"{FSLibrary.Calculator.DivisionByZero}\"", res);
        }

        [TestMethod]
        public async Task TestMethodWrongOperation()
        {
            var response = await _client.GetAsync($"http://localhost:5000/calc?Operation=plus&Num1=a&Num2=0");
            var res = await response.Content.ReadAsStringAsync();
            Assert.AreEqual($"\"{FSLibrary.Parser.ParserOperationFail}\"", res);
        }
        
    }
}