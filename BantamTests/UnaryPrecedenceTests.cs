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
    [TestClass] //?
    public class UnaryPrecedenceTests
    {
        [TestMethod]
        public void UnaryPrecedenceTest2()
        {
            const string expression = "a!!!";
            var tokenConfig = new TokenConfig();
            var parser = TestParser.Factory.CreateNew(new[]
            {
                new Prefix(TokenType.NAME, new NameParselet()),               
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet()),
                
            }, new[]
            {
                new Infix(TokenType.BANG, new PostfixOperatorParselet(Precedence.POSTFIX,tokenConfig))
              
            });

            var actual = parser.Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("(((a!)!)!)", actual);
        }

        [TestMethod]
        public void UnaryPrecedenceTest1()
        {
            const string expression = "~!-+a";
            var tokenConfig = new TokenConfig();
            var parser = TestParser.Factory.CreateNew(new[]
            {
                new Prefix(TokenType.BANG, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.LEFT_PAREN,new GroupParselet()),
                new Prefix(TokenType.TILDE, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.PLUS, new PrefixOperatorParselet(Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.NAME, new NameParselet())
            });

            var actual = parser.Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("(~(!(-(+a))))", actual);
        }
    }
}
