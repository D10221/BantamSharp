using System;
using Bantam;


using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseException = SimpleParser.ParseException<Bantam.TokenType>;
using IBuilder = SimpleParser.IBuilder<char>;

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
            var tokenConfig = new TokenConfig();
            var expression = new OperatorExpression(tokenConfig, left, @operator, right);
            IBuilder builder = new FakeBuilder();
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
                var tokenConfig = new TokenConfig();
                var left = new FakeExpression("x");
                var right = new FakeExpression("y");
                const TokenType @operator = TokenType.NONE;
                var expression = new OperatorExpression(tokenConfig, left, @operator, right);
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
