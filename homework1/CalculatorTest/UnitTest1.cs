using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using homework1;

namespace CalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSum()
        {
            Assert.AreEqual(2, Calculator.Calculate(1, 1, Calculator.Operation.Plus));
        }
        
        [TestMethod]
        public void TestMinus()
        {
            Assert.AreEqual(1, Calculator.Calculate(3, 2, Calculator.Operation.Minus));
        }

        [TestMethod]
        public void TestMultiply()
        {
            Assert.AreEqual(5, Calculator.Calculate(1, 5, Calculator.Operation.Multiply));
        }

        [TestMethod]
        public void TestDivide()
        {
            Assert.AreEqual(5, Calculator.Calculate(25, 5, Calculator.Operation.Divide));
        }

        [TestMethod]
        public void TestUnknownOperation()
        {
            try
            {
                Calculator.Calculate(1, 5, Calculator.Operation.Unknown);
            } catch (Exception exception) {
                Assert.AreEqual("Operation is unknown", exception.Message);
            }
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            try
            {
                Calculator.Calculate(1, 0, Calculator.Operation.Divide);
            } catch (Exception exception) {
                Assert.AreEqual("Num2 is zero", exception.Message);
            }
        }

        [TestMethod]
        public void TestParseNumber()
        {
            var resultOfParse = Parser.ToParseNumber("1");
            Assert.AreEqual(1, resultOfParse);

            try
            {
                var resultOfParse1 = Parser.ToParseNumber("a");
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Value is not integer", exception.Message);
            }
        }

        [TestMethod]
        public void TestParseOperation()
        {
            var resultOfParse = Parser.ParseOperation("+");
            Assert.AreEqual(Calculator.Operation.Plus, resultOfParse);
            
            var resultOfParse2 = Parser.ParseOperation("-");
            Assert.AreEqual(Calculator.Operation.Minus, resultOfParse2);
            
            var resultOfParse3 = Parser.ParseOperation("*");
            Assert.AreEqual(Calculator.Operation.Multiply, resultOfParse3);

            var resultOfParse4 = Parser.ParseOperation("/");
            Assert.AreEqual(Calculator.Operation.Divide, resultOfParse4);

            try
            {
                var resultOfParse1 = Parser.ParseOperation("a");
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Operation is not correct", exception.Message);
            }
        }
    }
}