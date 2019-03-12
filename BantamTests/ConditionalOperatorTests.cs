using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class ConditionalOperatorTests
    {
        [TestMethod]
        public void TestMethod0()
        {
            string actual = Printer.Default.Print(
                ParserFactory.Create()("a ? b : c"));
            Assert.AreEqual(expected: "(a?b:c)", actual);
        }

        [TestMethod]
        public void TestMethod1()
        {
            string actual = Printer.Default.Print(
                ParserFactory.Create()("a ? b : c ? d : e")
            );
            Assert.AreEqual(expected: "(a?b:(c?d:e))", actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string actual = Printer.Default.Print(
                ParserFactory.Create()("a ? b ? c : d : e"));
            Assert.AreEqual(expected: "(a?(b?c:d):e)", actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string actual = Printer.Default.Print(
                ParserFactory.Create()("a + b ? c * d : e / f"));
            Assert.AreEqual(expected: "((a+b)?(c*d):(e/f))", actual);
        }
    }
}
