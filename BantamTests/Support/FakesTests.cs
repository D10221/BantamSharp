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

namespace BantamTests.Support
{
    [TestClass]
    public class FakesTests
    {
        [TestMethod]
        public void FakeLexerTest()
        {
            var lexer = new FakeLexer("a");
            var t = lexer.Next();
            Assert.AreEqual("a", t.GetText());
        }

        [TestMethod]
        public void FakeLexerTest2()
        {
            var lexer = new FakeLexer("ab");
            Assert.AreEqual("a", lexer.Next().GetText());
            Assert.AreEqual("b", lexer.Next().GetText());
        }

        [TestMethod]
        public void FakeLexerTest3()
        {
            var lexer = new FakeLexer("");
            //WARNING ! , null not ok 
            Assert.AreEqual(null, lexer.Next().GetText());          
        }

        [TestMethod]
        public void FakeBuilderTest()
        {
            var fake = new FakeExpression("x");
            var builder = new FakeBuilder();
            fake.Print(builder);
            Assert.AreEqual("x", builder.Build());
        }
        
    }

    
}
