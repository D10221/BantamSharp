using Bantam.Expressions;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests.Expressions
{
    [TestClass]
    public class PostfixExpressionTests
    {
        [TestMethod]
        public void PostfixExpressionTest()
        {
            var op = TokenType.QUESTION;
            ISimpleExpression left = new FakeExpression("A");
            var expression = new PostfixExpression(left,op);
            var builder =  new FakeBuilder();
            expression.Print(builder);
            var actual = builder.Build();
            var expected = "(A?)";
            Assert.AreEqual(expected,actual);
        }
    }
}
