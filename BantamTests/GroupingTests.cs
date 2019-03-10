using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class GroupingTests
    {
        class Parser : IParser<TokenType>
        {
            private ISimpleExpression _expression;

            public Parser(ISimpleExpression expression)
            {
                _expression = expression;
            }

            public IEnumerable<IToken<TokenType>> Tokens => throw new System.NotImplementedException();

            public IToken<TokenType> Consume(TokenType expected)
            {
                return (IToken<TokenType>)_expression.Token;
            }

            public IToken<TokenType> Consume()
            {
                return (IToken<TokenType>)_expression.Token;
            }

            public bool IsMatch(TokenType expected)
            {
                return expected == ((IToken<TokenType>)_expression.Token).TokenType;
            }

            public ISimpleExpression ParseExpression(int precedence = 0)
            {
                return _expression;
            }
        }
        [TestMethod]
        public void TestMethod1()
        {
            var builder = new Builder();
            new GroupParselet(TokenType.PAREN_LEFT, TokenType.PARENT_RIGHT)
                .Parse(
                    new Parser(NameExpression.From("a")),
                    Token.From(TokenType.PAREN_LEFT, "("),
                    null
                ).Print(builder);
            Assert.AreEqual(
                "a",
                builder.ToString()
            );
        }
        [TestMethod]

        public void TestMethod2()
        {
            Exception ex = null;
            try
            {
                var builder = new Builder();
                new GroupParselet(TokenType.PAREN_LEFT, TokenType.PARENT_RIGHT)
                    .Parse(
                        new Parser(NameExpression.From("a")),
                        Token.From(TokenType.NAME, "x"),
                        null
                    );

            }
            catch (System.Exception e)
            {
                ex = e;
            }            
            Assert.IsInstanceOfType(ex, typeof(ParseException));
            //Assert.AreEqual(ex.Message, "?");
        }
    }
}
