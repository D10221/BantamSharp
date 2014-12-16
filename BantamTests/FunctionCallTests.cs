using Bantam;
using Bantam.Paselets;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
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

namespace BantamTests
{
    [TestClass]
    public class FunctionCallTests
    {
        // Function call.
        [TestMethod]
        public void TestMethod1()
        {            
            const string expression = "a()";
            var testParser = TestParser.Factory.CreateNew(new[]
            {
                new Prefix(TokenType.NAME, new NameParselet()),                
            }, new[] { new Infix(TokenType.LEFT_PAREN, new CallParselet()) });

            var parsed = testParser.Parse(expression);
            Assert.AreEqual("a()",parsed);
        }
        
        [TestMethod]
        public void TestMethod2()
        {           
            const string expression = "a(b)";
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes=
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet())
            };

            var parsed = TestParser.Factory.CreateNew(prefixes,infixes).Parse(expression);
            Assert.AreEqual("a(b)",parsed);
        }

        [TestMethod]
        public void TestMethod3()
        {            
            const string expression = "a(b, c)";
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet())
            };

            var parsed = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            Assert.AreEqual("a(b, c)", parsed);
        }
        
        [TestMethod]
        public void TestMethod4()
        {           
            const string expression = "a(b)(c)";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet()),
                new Infix(TokenType.PLUS,new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig))
            };

            var parsed = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            Assert.AreEqual(expression, parsed);
        }
        
        [TestMethod]
        public void TestMethod5()
        {
           const string expression = "a(b) + c(d)";
           var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet()),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig))
            };

            var parsed = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            Assert.AreEqual("(a(b) + c(d))", parsed);
        }
        
        [TestMethod]
        public void TestMethod6()
        {
            const string expression = "a(b ? c : d, e + f)";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet()),
                new Infix(TokenType.QUESTION, new ConditionalParselet()),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig))
            };

            var parsed = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            Assert.AreEqual("a((b ? c : d), (e + f))", parsed);
        }
    }
}
