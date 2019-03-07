using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class PostfixExpressionTests
    {
        [TestMethod]
        public void PostfixExpressionTest()
        {
            var op = TokenType.QUESTION;
            ISimpleExpression left = new FakeExpression("A");
            var expression = new PostfixExpression(Parser.TokenConfig, left, op);
            var builder = new Builder();
            expression.Print(builder);
            var actual = builder.Build();
            var expected = "(A?)";
            Assert.AreEqual(expected, actual);
        }
    }
}
