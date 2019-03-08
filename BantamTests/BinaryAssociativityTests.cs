using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BantamTests
{
    [TestClass]
    public class BinaryAssociativityTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string expression = "a = b = c";
            string actual = Parser.Parse(expression);
            const string expected = "(a=(b=c))";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "a + b - c";
            string actual = Parser.Parse(expression);
            const string expected = "((a+b)-c)";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // BinaryAssociativity.
            // _expectationses.AddExpectation("a ^ b ^ c", "(a ^ (b ^ c))");
            const string expression = "a * b / c";
            string actual = Parser.Parse(expression);
            const string expected = "((a*b)/c)";
            Assert.AreEqual(expected, actual); //Fails           
        }

        [TestMethod]
        public void TestMethod4()
        {
            const string expression = "a ^ b ^ c";
            var actual = Parser.Parse(expression);
            const string expected = "(a^(b^c))";
            Assert.AreEqual(expected, actual); //Fails           
        }
    }
}
