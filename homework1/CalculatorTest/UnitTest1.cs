using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.FSharp.Core;
using FSLibrary;

namespace CalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSum()
        {
            Assert.AreEqual(FSharpResult<int, string>.NewOk(2), 
                              Calculator.Calculate
                              (FSharpResult<int, string>.NewOk(1),
                               FSharpResult<int, string>.NewOk(1), 
                                   Calculator.Operation.Plus));
        }
        [TestMethod]
        public void TestSumDouble()
        {
            Assert.AreEqual(FSharpResult<double, string>.NewOk(2), 
                Calculator.Calculate
                (FSharpResult<double, string>.NewOk(1),
                    FSharpResult<double, string>.NewOk(1), 
                    Calculator.Operation.Plus));
        }
        [TestMethod]
        public void TestSumDecimal()
        {
            Assert.AreEqual(FSharpResult<decimal, string>.NewOk(2), 
                Calculator.Calculate
                (FSharpResult<decimal, string>.NewOk(1),
                    FSharpResult<decimal, string>.NewOk(1), 
                    Calculator.Operation.Plus));
        }
        [TestMethod]
        public void TestSumFloat()
        {
            Assert.AreEqual(FSharpResult<float, string>.NewOk(2), 
                Calculator.Calculate
                (FSharpResult<float, string>.NewOk(1),
                    FSharpResult<float, string>.NewOk(1), 
                    Calculator.Operation.Plus));
        }
        
        [TestMethod]
        public void TestMinus()
        {
            Assert.AreEqual(FSharpResult<int, string>.NewOk(1), 
                              Calculator.Calculate
                              (FSharpResult<int, string>.NewOk(3),  
                               FSharpResult<int, string>.NewOk(2), 
                                   Calculator.Operation.Minus));
        }
        
        [TestMethod]
        public void TestMultiply()
        {
            Assert.AreEqual(FSharpResult<int, string>.NewOk(5), 
                              Calculator.Calculate
                              (FSharpResult<int,string>.NewOk(1), 
                               FSharpResult<int, string>.NewOk(5), 
                                   Calculator.Operation.Multiply));
        }

        [TestMethod]
        public void TestDivide()
        {
            Assert.AreEqual(FSharpResult<int, string>.NewOk(5), 
                              Calculator.Calculate
                              (FSharpResult<int, string>.NewOk(25), 
                               FSharpResult<int, string>.NewOk(5),  
                                   Calculator.Operation.Divide));
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            Assert.AreEqual(FSharpResult<int, string>.NewError(FSLibrary.Calculator.DivisionByZero), 
                              Calculator.Calculate
                              (FSharpResult<int, string>.NewOk(1),
                               FSharpResult<int, string>.NewOk(0),
                                   Calculator.Operation.Divide));
        }

        [TestMethod]
        public void TestParseNumber()
        {
            Assert.AreEqual(FSharpResult<int, string>.NewOk(1), Parser.ParseIntNumber("1"));
            Assert.AreEqual(FSharpResult<int,string>.NewError(FSLibrary.Parser.ParserOperationFail), 
                              Parser.ParseIntNumber("a"));
        }

        [TestMethod]
        public void TestParseOperation()
        {
            Assert.AreEqual(FSharpResult<Calculator.Operation, string>.NewOk(Calculator.Operation.Plus), Parser.ParseOperation("+"));
            Assert.AreEqual(FSharpResult<Calculator.Operation, string>.NewOk(Calculator.Operation.Minus), Parser.ParseOperation("-"));
            Assert.AreEqual(FSharpResult<Calculator.Operation, string>.NewOk(Calculator.Operation.Multiply), Parser.ParseOperation("*"));
            Assert.AreEqual(FSharpResult<Calculator.Operation, string>.NewOk(Calculator.Operation.Divide), Parser.ParseOperation("/"));
            Assert.AreEqual(FSharpResult<Calculator.Operation, string>.NewError(FSLibrary.Parser.ParserOperationFail),
                              Parser.ParseOperation("a"));
        }
    }
}