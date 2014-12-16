using Bantam.Expressions;
using BantamTests.Parselets;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseException = SimpleParser.ParseException<Bantam.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<char>;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>;
using InfixParselet = SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>;

namespace BantamTests.Expressions
{
    [TestClass]
    public class AssignExpressionTests
    {
        [TestMethod]
        public void AssignExpressionTest()
        {
            const string name = "a";
            ISimpleExpression right = new NameExpression("b");
            var expresison = new AssignExpression(name, right);
            var builder = new Builder();
            expresison.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(a = b)", actual);
        }
        
        [TestMethod]
        public void AssignExpressionTest2()
        {
            const string name = "a";
            ISimpleExpression right = new CallExpression(new NameExpression("b"), null);
            var expresison = new AssignExpression(name, right);
            var builder = new Builder();
            expresison.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(a = b())", actual);
        }

        [TestMethod]
        public void AssignExpressionTest3()
        {
            const string name = "a";
            ISimpleExpression right = new FakeExpression("x");
            var expresison = new AssignExpression(name, right);
            var builder = new FakeBuilder();
            expresison.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(a = x)", actual);
        }       
       
    }
}
