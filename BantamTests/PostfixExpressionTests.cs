using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class PostfixExpressionTests
    {
        [TestMethod]
        public void Test1()
        {
            var e = new PostfixExpression(Token.From(TokenType.NONE, "?"), NameExpression.From("A"));
            var b = new Printer();
            b.Visit(e);
            var actual = b.ToString();
            var expected = "(A?)";
            Assert.AreEqual(expected, actual);
        }
    }
}
