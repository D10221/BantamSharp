using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using System;
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

            public ISimpleExpression ParseExpression(int precedence)
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
            expression.Print(builder);
            var x = builder.ToString();
            Assert.AreEqual("(A=a)", x);
        }
        [TestMethod]
        public void Test2()
        {
            Exception ex = null;
            try
            {
                var parselet = new AssignParselet(TokenType.ASSIGN, (int)Precedence.ASSIGNMENT);
                var token = Token.From(TokenType.ASSIGN, "=");
                var expression = parselet.Parse(
                    new FakeParser(NameExpression.From("a")),
                    token,
                    new WrongExpression());
            }
            catch (System.Exception e)
            {
                ex = e;
            }
            Assert.IsInstanceOfType(ex, typeof(ParseException));
        }
        class WrongExpression : ISimpleExpression
        {
            public object Token => null;

            public void Print(IBuilder builder)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
