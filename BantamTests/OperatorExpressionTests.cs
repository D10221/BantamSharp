using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using System;

namespace BantamTests
{
    [TestClass]
    public class OperatorExpressionTests
    {
        [TestMethod]
        public void OperatorExpressionTest()
        {
            var left = new NameExpression("x");
            var right = new NameExpression("y");            
            var expression = new BinaryOperatorExpression(Token.From(TokenType.NONE, "+"), left, right);
            IBuilder builder = new Builder();
            expression.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(x+y)", actual);
        }

        [TestMethod]
        public void OperatorExpressionTest2()
        {
            Exception ex = null;
            try
            {
                var left = new NameExpression("x");
                var right = new NameExpression("y");                
                var expression = new BinaryOperatorExpression(null, left, right);
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
