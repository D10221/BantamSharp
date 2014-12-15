using Bantam.Expressions;
using Bantam.Paselets;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Token = SimpleParser.Token<SimpleParser.TokenType>;

namespace BantamTests.Parselets
{
    [TestClass]
    public class AssignParseletTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var parselet = new AssignParselet();
            var token = Token.New(TokenType.ASSIGN, "=");
            var parser = new FakeParser(new NameExpression("a"),null);
            var left = new NameExpression("A");
            var p = parselet.Parse(parser, left, token);
            var builder = new Builder();
            p.Print(builder);
            var x = builder.Build();
            Assert.AreEqual("(A = a)", x);
        }

       
    }
}
