
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class ConditionalExpressionTests
    {
        [TestMethod]
        public void ConditionalExpressionTest()
        {
            ISimpleExpression<TokenType> condition = new NameExpression(Token.From(TokenType.NONE, "x"));
            ISimpleExpression<TokenType> then = new NameExpression(Token.From(TokenType.NONE, "y"));
            ISimpleExpression<TokenType> @else = new NameExpression(Token.From(TokenType.NONE, "z"));
            var e = new ConditionalExpression(condition, then, @else);
            var b = new Printer();
            b.Visit(e);
            var actual = b.ToString();
            Assert.AreEqual("(x?y:z)", actual);
        }
    }
}