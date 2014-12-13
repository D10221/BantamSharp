using Bantam.Expressions;
using Bantam.Paselets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class AssignParseletTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var parselet = new AssignParselet();
            var token = Token.New(TokenType.ASSIGN, "=");
            var parser = new FakeParser();

            var left = new NameExpression("a");
            var p = parselet.Parse(parser, left, token);
            var builder = new Builder();
            p.Print(builder);
            var x = builder.ToString();
            Assert.AreEqual("(a = a)", x);

        }

       
    }

    public class FakeParser : IParser
    {


        public void Register(TokenType token, IPrefixParselet parselet)
        {
            throw new System.NotImplementedException();
        }

        public void Register(TokenType token, InfixParselet parselet)
        {
            throw new System.NotImplementedException();
        }

        public ISimpleExpression ParseExpression()
        {
            return new NameExpression("a");
        }

        public bool IsMatch(TokenType expected)
        {
            throw new System.NotImplementedException();
        }

        public IToken Consume(TokenType expected)
        {
            throw new System.NotImplementedException();
        }

        public IToken Consume()
        {
            throw new System.NotImplementedException();
        }
    }
}
