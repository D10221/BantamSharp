using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

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
        public void TestMethod1()
        {
            var parselet = new AssignParselet();
            var token = Token.From(TokenType.ASSIGN, "=");
            var parser = new FakeParser(new NameExpression("a"));
            var left = new NameExpression("A");
            var p = parselet.Parse(parser, token, left);
            var builder = new Builder();
            p.Print(builder);
            var x = builder.ToString();
            Assert.AreEqual("(A=a)", x);
        }
    }
}
