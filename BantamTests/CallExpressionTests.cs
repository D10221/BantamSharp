
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class CallExpressionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var nameExpression = new NameExpression(Token.From(TokenType.NONE, "a"));
            var e = new FunctionCallExpression(nameExpression, null);
            var b = new Printer();
            b.Visit(e);
            string actual = b.ToString();
            Assert.AreEqual("a()", actual);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var nameExpression = new NameExpression(Token.From(TokenType.NONE, ""));
            var e = new FunctionCallExpression(nameExpression, null);
            var b = new Printer();
            b.Visit(e);
            string actual = b.ToString();
            Assert.AreEqual("()", actual);
        }
    }
}