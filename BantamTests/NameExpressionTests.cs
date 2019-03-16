
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class NameExpressionTests
    {
        [TestMethod]
        public void NameExpressionTest()
        {
            var e = NameExpression.From("aa");
            var b = new Printer();
            b.Visit(e);
            var actual = b.ToString();
            Assert.AreEqual("aa", actual);
        }


        [TestMethod]
        public void NameExpressionTest1()
        {
            var e = NameExpression.From("a");
            var b = new Printer();
            b.Visit(e);
            var actual = b.ToString();
            Assert.AreEqual("a", actual);
        }

        [TestMethod]
        public void NameExpressionTest2()
        {
            var e = NameExpression.From("");
            var b = new Printer();
            b.Visit(e);
            var actual = b.ToString();
            Assert.AreEqual("", actual);
        }

        [TestMethod]
        public void NameExpressionTest3()
        {
            var e = NameExpression.From("whatever you want to put here\n");
            var b = new Printer();
            b.Visit(e);
            var actual = b.ToString();
            Assert.AreEqual("whatever you want to put here\n", actual);
        }
    }
}
