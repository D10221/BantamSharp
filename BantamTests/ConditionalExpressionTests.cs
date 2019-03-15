
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
            var expresion = new ConditionalExpression(condition, then, @else);
            var _builder = new Builder();
            expresion.Visit(_builder);
            var actual = _builder.ToString();
            Assert.AreEqual("(x?y:z)", actual);
        }
    }
}