using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IBuilder = SimpleParser.IBuilder<char>;
using ParseException = SimpleParser.ParseException<Bantam.TokenType>;

namespace BantamTests
{
    [TestClass]
    public class OperatorExpressionTests
    {
        [TestMethod]
        public void OperatorExpressionTest()
        {
            var left = new FakeExpression("x");
            var right = new FakeExpression("y");
            const TokenType @operator = TokenType.PLUS;
            var expression = new OperatorExpression(Parser.TokenConfig, @operator, left, right);
            IBuilder builder = new Builder();
            expression.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(x + y)", actual);
        }

        [TestMethod]
        public void OperatorExpressionTest2()
        {
            Exception ex = null;
            try
            {
                var left = new FakeExpression("x");
                var right = new FakeExpression("y");
                const TokenType @operator = TokenType.NONE;
                var expression = new OperatorExpression(Parser.TokenConfig, @operator, left, right);
                //Just to stop the compiler warning becasue is not used 
                Assert.IsNotNull(expression);
            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.IsNotNull(ex as ParseException);
        }
    }
}
