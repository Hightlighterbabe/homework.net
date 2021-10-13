using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using homework1;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CalculatorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSum()
        {
            AreEqual(2, FSLibrary.Calculator.Calculate(1, 1, FSLibrary.Calculator.Operation.Plus));
        }
        
        [TestMethod]
        public void TestMinus()
        {
            AreEqual(1, FSLibrary.Calculator.Calculate(3, 2, FSLibrary.Calculator.Operation.Minus));
        }

        [TestMethod]
        public void TestMultiply()
        {
            AreEqual(5, FSLibrary.Calculator.Calculate(1, 5, FSLibrary.Calculator.Operation.Multiply));
        }

        [TestMethod]
        public void TestDivide()
        {
            AreEqual(5, FSLibrary.Calculator.Calculate(25, 5, FSLibrary.Calculator.Operation.Divide));
        }

        [TestMethod]
        public void TestUnknownOperation()
        {
            try
            {
                FSLibrary.Calculator.Calculate(1, 5, FSLibrary.Calculator.Operation.Unknown);
            } catch (Exception exception) {
                AreEqual("Operation is unknown", exception.Message);
            }
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            try
            {
                FSLibrary.Calculator.Calculate(1, 0, FSLibrary.Calculator.Operation.Divide);
            } catch (Exception exception) {
                AreEqual("Num2 is zero", exception.Message);
            }
        }

        [TestMethod]
        public void TestParseNumber()
        {
            var resultOfParse = Parser.ToParseNumber("1");
            AreEqual(1, resultOfParse);

            try
            {
                var resultOfParse1 = Parser.ToParseNumber("a");
            }
            catch (Exception exception)
            {
                AreEqual("Value is not integer", exception.Message);
            }
        }

        [TestMethod]
        public void TestParseOperation()
        {
            var resultOfParse = Parser.ParseOperation("+");
            AreEqual(Calculator.Operation.Plus, resultOfParse);
            
            var resultOfParse2 = Parser.ParseOperation("-");
            AreEqual(Calculator.Operation.Minus, resultOfParse2);
            
            var resultOfParse3 = Parser.ParseOperation("*");
            AreEqual(Calculator.Operation.Multiply, resultOfParse3);

            var resultOfParse4 = Parser.ParseOperation("/");
            AreEqual(Calculator.Operation.Divide, resultOfParse4);

            try
            {
                var resultOfParse1 = Parser.ParseOperation("a");
            }
            catch (Exception exception)
            {
                AreEqual("Operation is not correct", exception.Message);
            }
        }
    }
}