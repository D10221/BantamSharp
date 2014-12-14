using Bantam.Expressions;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests.Expressions
{
    [TestClass]
    public class PrefixExpressionTests
    {
        [TestMethod]
        public void PrefixExpressionTest()
        {
            const TokenType @operator = TokenType.MINUS;
            var right = new FakeExpression("A");
            var exression = new PrefixExpression(@operator,right);
            var builder= new FakeBuilder();
            exression.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(-A)",actual);
        }
        
        [TestMethod]
        public void PrefixExpressionTest2()
        {
            const TokenType @operator = TokenType.BANG;
            var right = new FakeExpression("A");
            var exression = new PrefixExpression(@operator,right);
            var builder= new FakeBuilder();
            exression.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(!A)",actual);
        }
        
        [TestMethod]
        public void PrefixExpressionTest3()
        {
            const TokenType @operator = TokenType.PLUS;
            var right = new FakeExpression("A");
            var exression = new PrefixExpression(@operator,right);
            var builder= new FakeBuilder();
            exression.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(+A)",actual);
        }

        [TestMethod]
        public void PrefixExpressionTest4()
        {
            const TokenType @operator = TokenType.TILDE;
            var right = new FakeExpression("A");
            var exression = new PrefixExpression(@operator,right);
            var builder= new FakeBuilder();
            exression.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(~A)",actual);
        }
        
        

        
    }
}
