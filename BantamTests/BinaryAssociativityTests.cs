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
    public class BinaryAssociativityTests
    {
        [TestMethod]
        public void TestMethod1()
        {            
            const string expression = "a = b = c";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.ASSIGN, new AssignParselet()),
                new Infix(TokenType.LEFT_PAREN, new CallParselet()),
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "(a = (b = c))";
            Assert.AreEqual(expected, actual);          
        }
        
        [TestMethod]
        public void TestMethod2()
        {            
            const string expression = "a + b - c";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig)),
                new Infix(TokenType.MINUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig)),
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "((a + b) - c)";
            Assert.AreEqual(expected, actual);          
        }
        
        [TestMethod]
        public void TestMethod3()
        {
            // BinaryAssociativity.
            // _expectationses.AddExpectation("a ^ b ^ c", "(a ^ (b ^ c))");
            const string expression = "a * b / c";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,tokenConfig)),
                new Infix(TokenType.SLASH, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,tokenConfig)),
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "((a * b) / c)";
            Assert.AreEqual(expected, actual); //Fails           
        }

        [TestMethod]
        public void TestMethod4()
        {            
            const string expression = "a ^ b ^ c";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.CARET, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Right,tokenConfig)),                
            };

            var actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "(a ^ (b ^ c))";
            Assert.AreEqual(expected, actual); //Fails           
        }
    }
}
