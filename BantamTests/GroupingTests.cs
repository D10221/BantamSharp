using System;
using System.Collections.Generic;
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class GroupingTests
    {
        class NotParser : IParser<TokenType>
        {
            private ISimpleExpression<TokenType> _expression;

            public NotParser(ISimpleExpression<TokenType> expression)
            {
                _expression = expression;
            }

            public IEnumerable<IToken<TokenType>> Tokens => throw new System.NotImplementedException();

            public IToken<TokenType> Consume(TokenType expected)
            {
                return _expression.Token;
            }

            public IToken<TokenType> Consume()
            {
                return _expression.Token;
            }

            public IToken<TokenType> LookAhead()
            {
                return _expression.Token;
            }

            public ISimpleExpression<TokenType> Parse(int precedence)
            {
                return _expression;
            }
        }
       
        [TestMethod]
        public void GroupParseletTest2()
        {
            Exception ex = null;
            try
            {
                var builder = new Printer();
                new GroupParselet(TokenType.PAREN_LEFT, TokenType.PARENT_RIGHT)
                    .Parse(
                        new NotParser(NameExpression.From("a")),
                        Token.From(TokenType.NAME, "x"),
                        null
                    );

            }
            catch (System.Exception e)
            {
                ex = e;
            }
            Assert.IsInstanceOfType(ex, typeof(ParseException));
            TestLogger.Log($"{nameof(GroupParseletTest2)}: Expected message? '{ex.Message}'");
        }

        [TestMethod]
        public void GroupingTest1()
        {
            Assert.AreEqual(
                expected: "((a+(b+c))+d)",
                actual: Printer.Create().Print(ParserFactory.Create()("a + (b + c) + d")));
        }

        [TestMethod]
        public void GroupingTest2()
        {
            Assert.AreEqual(
                expected: "(a^(b+c))",
                actual: Printer.Create().Print(ParserFactory.Create()("a ^ (b + c)")));
        }

        [TestMethod]
        public void GroupingTest3()
        {
            const string expected = "((!a)!)";
            Assert.AreEqual(
                expected,
                actual: Printer.Create().Print(ParserFactory.Create()("(!a)!")));
        }
        [TestMethod]
        public void GroupingTest4()
        {
            const string expected = "a";
            Assert.AreEqual(
                expected,
                actual: Printer.Create().Print(ParserFactory.Create()("a")));
        }
    }
}
