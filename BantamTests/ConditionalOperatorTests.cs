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
            const string expression = "a ? b : c";
            string actual = Parser.Parse(expression);
            const string expected = "(a?b:c)";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod1()
        {
            const string expression = "a ? b : c ? d : e";
            string actual = Parser.Parse(expression);
            const string expected = "(a?b:(c?d:e))";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "a ? b ? c : d : e";
            string actual = Parser.Parse(expression);
            const string expected = "(a?(b?c:d):e)";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            const string expression = "a + b ? c * d : e / f";
            string actual = Parser.Parse(expression);
            const string expected = "((a+b)?(c*d):(e/f))";
            Assert.AreEqual(expected, actual);
        }
    }
}
