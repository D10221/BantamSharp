
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
            var expresion = NameExpression.From("aa");
            var _builder = new Builder();
            expresion.Visit(_builder);
            var actual = _builder.ToString();
            Assert.AreEqual("aa", actual);
        }


        [TestMethod]
        public void NameExpressionTest1()
        {
            var expresion = NameExpression.From("a");
            var _builder = new Builder();
            expresion.Visit(_builder);
            var actual = _builder.ToString();
            Assert.AreEqual("a", actual);
        }

        [TestMethod]
        public void NameExpressionTest2()
        {
            var expresion = NameExpression.From("");
            var _builder = new Builder();
            expresion.Visit(_builder);
            var actual = _builder.ToString();
            Assert.AreEqual("", actual);
        }

        [TestMethod]
        public void NameExpressionTest3()
        {
            var expresion = NameExpression.From("whatever you want to put here\n");
            var _builder = new Builder();
            expresion.Visit(_builder);
            var actual = _builder.ToString();
            Assert.AreEqual("whatever you want to put here\n", actual);
        }
    }
}
