using Bantam;
using Bantam.Common;
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
    [TestClass] //Unary and binary predecence
    public class UnaryAndBinaryPrecedenceTests
    {

        [TestMethod]
        public void TestMethod()
        {
            const string expression = "a+(b+c)";

            var tokenConfig = new TokenConfig();

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.PLUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "(a+(b+c))";

            Assert.AreEqual(expected, actual.Reglex("\\s","")); //Fails           
        }

        // Unary and binary predecence.
        [TestMethod]
        public void TestMethod1()
        {
            const string expression = "-a * b";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig))
            };
            Infix[] infixes =
            {
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,tokenConfig))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "((-a) * b)";
            Assert.AreEqual(expected, actual); //Fails           
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "!a + b";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.BANG, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig))
            };
            Infix[] infixes =
            {
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "((!a) + b)";
            Assert.AreEqual(expected, actual); //Fails     
        }

        [TestMethod]
        public void TestMethod3()
        {
            const string expression = "~a ^ b";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.TILDE, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig))
            };
            Infix[] infixes =
            {
                //TODO: 
                new Infix(TokenType.CARET, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,tokenConfig))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "((~a) ^ b)";
            Assert.AreEqual(expected, actual); //Fails     
        }


        [TestMethod]
        public void TestMethod4()
        {
            const string expression = "-a!";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig))
            };

            Infix[] infixes =
            {
                new Infix(TokenType.BANG, new PostfixOperatorParselet(Precedence.POSTFIX,tokenConfig))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "(-(a!))";
            Assert.AreEqual(expected, actual); //Fails   
        }

        [TestMethod]
        public void TestMethod5()
        {
            const string expression = "!a!";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.BANG, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig))
            };

            Infix[] infixes =
            {
                new Infix(TokenType.BANG, new PostfixOperatorParselet(Precedence.POSTFIX,tokenConfig))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "(!(a!))";
            Assert.AreEqual(expected, actual); //Fails   
        }

        // BinaryPrecedence.
        //test("a = b + c * d ^ e - f / g", "(a = ((b + (c * (d ^ e))) - (f / g)))");
        [TestMethod]
        public void BinaryPrecedenceTest()
        {
            const string expression = "(a = ((b + (c * (d ^ e))) - (f / g)))";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet()),
                new Prefix(TokenType.PLUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig))                
            };

            Infix[] infixes =
            {
                new Infix(TokenType.ASSIGN, new AssignParselet()),
                new Infix(TokenType.LEFT_PAREN, new CallParselet()),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig)),
                new Infix(TokenType.MINUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left,tokenConfig)),
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,tokenConfig)),
                new Infix(TokenType.SLASH, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left,tokenConfig)),
                new Infix(TokenType.CARET, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Right,tokenConfig))
            };

            var actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "(a = ((b + (c * (d ^ e))) - (f / g)))";
            Assert.AreEqual(expected, actual); //Fails               
        }
    }
}
