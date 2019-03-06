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
            const string expression = "a()";
            var parsed = Parser.Parse(expression);
            Assert.AreEqual("a()", parsed);
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "a(b)";

            var parsed = Parser.Parse(expression);
            Assert.AreEqual("a(b)", parsed);
        }

        [TestMethod]
        public void TestMethod3()
        {
            const string expression = "a(b, c)";
            var parsed = Parser.Parse(expression);
            Assert.AreEqual("a(b, c)", parsed);
        }

        [TestMethod]
        public void TestMethod4()
        {
            const string expression = "a(b)(c)";
            var parsed = Parser.Parse(expression);
            Assert.AreEqual(expression, parsed);
        }

        [TestMethod]
        public void TestMethod5()
        {
            const string expression = "a(b) + c(d)";
            var parsed = Parser.Parse(expression);
            Assert.AreEqual("(a(b) + c(d))", parsed);
        }

        [TestMethod]
        public void TestMethod6()
        {
            const string expression = "a(b ? c : d, e + f)";
            var parsed = Parser.Parse(expression);
            Assert.AreEqual("a((b ? c : d), (e + f))", parsed);
        }
    }
}
