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
            var expression = new PostfixExpression(Token.From(TokenType.NONE, "?"), NameExpression.From("A"));
            var builder = new Builder();
            expression.Print(builder);
            var actual = builder.ToString();
            var expected = "(A?)";
            Assert.AreEqual(expected, actual);
        }
    }
}
