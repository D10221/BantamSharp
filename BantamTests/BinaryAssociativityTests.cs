using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class BinaryAssociativityTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var e = Parser.Parse("a = b = c");
            Assert.AreEqual(
                expected: "(a=(b=c))",
                actual: Printer.Default.Print(e));
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
            Assert.AreEqual(expected: "((a*b)/c)", e);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var actual = Printer.Default.Print(
                Parser.Parse(
                    "a ^ b ^ c"
            ));
            Assert.AreEqual(expected: "(a^(b^c))", actual);
        }
        [TestMethod]
        public void TestMethod5()
        {
            var e = Parser.Parse(
                    "a && b"
            );
            Assert.AreEqual(expected: "(a&&b)", Printer.Default.Print(e));
        }
        [TestMethod]
        public void TestMethod6()
        {
            var e = Parser.Parse(
                    "a  && b || c"
            );
            Assert.AreEqual(expected: "(a&&(b||c))", Printer.Default.Print(e));
        }
        [TestMethod]
        public void TestMethod7()
        {
            var e = Parser.Parse(
                    "a * b + c"
            );
            Assert.AreEqual(expected: "((a*b)+c)", Printer.Default.Print(e));
        }
        [TestMethod]
        public void TestMethod8()
        {
            var e = Parser.Parse(
                    "a / b - c"
            );
            Assert.AreEqual(expected: "((a/b)-c)", Printer.Default.Print(e));
        }
        [TestMethod]
        public void TestMethod9()
        {
            var e = Parser.Parse(
                    "a / b * c - d + e"
            );
            Assert.AreEqual(expected: "((((a/b)*c)-d)+e)", Printer.Default.Print(e));
        }
        [TestMethod]
        public void TestMethod10()
        {
            var e = Parser.Parse(
                    "a == b != c"
            );
            Assert.AreEqual(expected: "(a==(b!=c))", Printer.Default.Print(e));
        }
    }
}
