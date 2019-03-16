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
            var e = ParserFactory.Create()("a = b = c");
            Assert.AreEqual(
                expected: "(a=(b=c))",
                actual: Printer.Create().Print(e));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("a + b - c"));
            Assert.AreEqual(expected: "((a+b)-c)", actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // BinaryAssociativity.
            // _expectationses.AddExpectation("a ^ b ^ c", "(a ^ (b ^ c))");            
            var e = Printer.Create().Print(
                ParserFactory.Create()("a * b / c"));
            Assert.AreEqual(expected: "((a*b)/c)", e);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var actual = Printer.Create().Print(
                ParserFactory.Create()("a ^ b ^ c"));
            Assert.AreEqual(expected: "(a^(b^c))", actual);
        }
        [TestMethod]
        public void TestMethod5()
        {
            var e = ParserFactory.Create()("a && b");
            Assert.AreEqual(expected: "(a&&b)", Printer.Create().Print(e));
        }
        [TestMethod]
        public void TestMethod6()
        {
            var e = ParserFactory.Create()("a  && b || c");
            Assert.AreEqual(expected: "(a&&(b||c))", Printer.Create().Print(e));
        }
        [TestMethod]
        public void TestMethod7()
        {
            var e = ParserFactory.Create()("a * b + c");
            Assert.AreEqual(expected: "((a*b)+c)", Printer.Create().Print(e));
        }
        [TestMethod]
        public void TestMethod8()
        {
            var e = ParserFactory.Create()("a / b - c");
            Assert.AreEqual(expected: "((a/b)-c)", Printer.Create().Print(e));
        }
        [TestMethod]
        public void TestMethod9()
        {
            var e = ParserFactory.Create()("a / b * c - d + e");
            Assert.AreEqual(expected: "((((a/b)*c)-d)+e)", Printer.Create().Print(e));
        }
        [TestMethod]
        public void TestMethod10()
        {
            var e = ParserFactory.Create()("a == b != c");
            Assert.AreEqual(expected: "(a==(b!=c))", Printer.Create().Print(e));
        }
    }
}
