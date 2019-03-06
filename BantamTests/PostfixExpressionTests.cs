using Bantam;


using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISimpleExpression = SimpleParser.ISimpleExpression<char>;

namespace BantamTests
{
    [TestClass]
    public class PostfixExpressionTests
    {
        [TestMethod]
        public void PostfixExpressionTest()
        {
            var tokenCOnfig = new TokenConfig();
            var op = TokenType.QUESTION;
            ISimpleExpression left = new FakeExpression("A");
            var expression = new PostfixExpression(tokenCOnfig, left, op);
            var builder = new FakeBuilder();
            expression.Print(builder);
            var actual = builder.Build();
            var expected = "(A?)";
            Assert.AreEqual(expected, actual);
        }
    }
}
