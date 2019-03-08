
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
            var exp = new FunctionCallExpression(nameExpression, null);
            var _builder = new Builder();
            exp.Print(_builder);
            string actual = _builder.ToString();
            Assert.AreEqual("a()", actual);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var nameExpression = new NameExpression(Token.From(TokenType.NONE, ""));
            var exp = new FunctionCallExpression(nameExpression, null);
            var _builder = new Builder();
            exp.Print(_builder);
            string actual = _builder.ToString();
            Assert.AreEqual("()", actual);
        }
    }
}