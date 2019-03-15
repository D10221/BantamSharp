using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using System.Collections.Generic;

namespace BantamTests
{
    [TestClass]
    public class AssignParseletTests
    {
        private class FakeParser : IParser<TokenType>
        {
            private readonly NameExpression _expression;

            public FakeParser(NameExpression expression)
            {
                _expression = expression;
            }
            public IEnumerable<IToken<TokenType>> Tokens { get; } = new List<IToken<TokenType>>();

            public ISimpleExpression<TokenType> ParseExpression(int precedence = 0, object caller = null)
            {
                return _expression;
            }

            public bool IsMatch(TokenType expected)
            {
                throw new System.NotImplementedException();
            }

            public IToken<TokenType> Consume(TokenType expected)
            {
                throw new System.NotImplementedException();
            }

            public IToken<TokenType> Consume()
            {
                throw new System.NotImplementedException();
            }
        }

        [TestMethod]
        public void Test1()
        {
            var parselet = new AssignParselet(TokenType.ASSIGN, (int)Precedence.ASSIGNMENT);
            var token = Token.From(TokenType.ASSIGN, "=");
            var parser = new FakeParser(NameExpression.From("a"));
            var left = NameExpression.From("A");
            var expression = parselet.Parse(parser, token, left);
            var builder = new Builder();
            builder.Visit(expression);
            var x = builder.ToString();
            Assert.AreEqual("(A=a)", x);
        }
        [TestMethod]
        public void Test2()
        {
            Assert.ThrowsException<ParseletException>(() =>
            {
                var parselet = new AssignParselet(TokenType.ASSIGN, (int)Precedence.ASSIGNMENT);
                var token = Token.From(TokenType.ASSIGN, "=");
                parselet.Parse(
                    new FakeParser(NameExpression.From("a")),
                    token,
                    new WrongExpression());
            });
        }
        class WrongExpression : ISimpleExpression<TokenType>
        {
            public IToken<TokenType> Token { get; } = SimpleParser.Token.Empty(TokenType.NONE);

            public void Visit(IExpressionVisitor<TokenType> builder)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
