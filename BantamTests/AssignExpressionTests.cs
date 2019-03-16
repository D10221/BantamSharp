
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class AssignExpressionTests
    {
        [TestMethod]
        public void AssignExpressionTest()
        {
            var e = new AssignExpression(NameExpression.From("a"), NameExpression.From("b"));
            var b = new Builder();
            b.Visit(e);
            var actual = b.ToString();
            Assert.AreEqual("(a=b)", actual);
        }

        [TestMethod]
        public void AssignExpressionTest2()
        {
            var token = Token.From(TokenType.NONE, "b");
            ISimpleExpression<TokenType> right = new FunctionCallExpression(new NameExpression(token), null);
            var e = new AssignExpression(NameExpression.From("a"), right);
            var b = new Builder();
            b.Visit(e);
            var actual = b.ToString();
            Assert.AreEqual("(a=b())", actual);
        }
    }
}
