using Bantam;
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
            var tokenConfig = new TokenConfig();
            const TokenType @operator = TokenType.MINUS;
            var right = new FakeExpression("A");
            var exression = new PrefixExpression(tokenConfig,@operator,right);
            var builder= new FakeBuilder();
            exression.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(-A)",actual);
        }
        
        [TestMethod]
        public void PrefixExpressionTest2()
        {
            var tokenConfig = new TokenConfig();
            const TokenType @operator = TokenType.BANG;
            var right = new FakeExpression("A");
            var exression = new PrefixExpression(tokenConfig,@operator,right);
            var builder= new FakeBuilder();
            exression.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(!A)",actual);
        }
        
        [TestMethod]
        public void PrefixExpressionTest3()
        {
            var tokenConfig = new TokenConfig();
            const TokenType @operator = TokenType.PLUS;
            var right = new FakeExpression("A");
            var exression = new PrefixExpression(tokenConfig,@operator,right);
            var builder= new FakeBuilder();
            exression.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(+A)",actual);
        }

        [TestMethod]
        public void PrefixExpressionTest4()
        {
            var tokenConfig = new TokenConfig();
            const TokenType @operator = TokenType.TILDE;
            var right = new FakeExpression("A");
            var exression = new PrefixExpression(tokenConfig,@operator,right);
            var builder= new FakeBuilder();
            exression.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(~A)",actual);
        }
        
        

        
    }
}
