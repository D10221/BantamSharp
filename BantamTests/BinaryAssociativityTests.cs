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
            string actual = Printer.Default.Print(
                Parser.Parse(
                    "a = b = c"
            ));
            Assert.AreEqual(
                expected: "(a=(b=c))",
                actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string actual = Printer.Default.Print(
                Parser.Parse(
                    "a + b - c"
            ));
            Assert.AreEqual(expected: "((a+b)-c)", actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // BinaryAssociativity.
            // _expectationses.AddExpectation("a ^ b ^ c", "(a ^ (b ^ c))");            
            var e = Printer.Default.Print(
                Parser.Parse(
                    "a * b / c"
            ));
            Assert.AreEqual(expected: "((a*b)/c)", e); //Fails           
        }

        [TestMethod]
        public void TestMethod4()
        {
            var actual = Printer.Default.Print(
                Parser.Parse(
                    "a ^ b ^ c"
            ));
            Assert.AreEqual(expected: "(a^(b^c))", actual); //Fails           
        }
        [TestMethod]
        public void TestMethod5()
        {
            var e = Parser.Parse(
                    "a == b && b == c "
            );
            Assert.AreEqual(expected: "((a==b)&&(b==c))", e); //Fails   
        }
    }
}
