using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class FunctionCallTests
    {
        // Function call.
        [TestMethod]
        public void TestMethod1()
        {
            var parser = ParserFactory.Create();
            var e = parser("a()");
            var parsed = Printer.Default.Print(e);
            Assert.AreEqual("a()", parsed);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var parse = ParserFactory.Create();
            var e = parse("a(b)");
            var parsed = Printer.Default.Print(e);
            Assert.AreEqual("a(b)", parsed);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var parsed = Printer.Default.Print(ParserFactory.Create()("a(b, c)"));
            Assert.AreEqual("a(b,c)", parsed);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var parsed = Printer.Default.Print(ParserFactory.Create()("a(b)(c)"));
            Assert.AreEqual("a(b)(c)", parsed);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var parsed = Printer.Default.Print(ParserFactory.Create()("a(b) + c(d)"));
            Assert.AreEqual("(a(b)+c(d))", parsed);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var parsed = Printer.Default.Print(ParserFactory.Create()("a(b ? c : d, e + f)"));
            Assert.AreEqual("a((b?c:d),(e+f))", parsed);
        }
    }
}
