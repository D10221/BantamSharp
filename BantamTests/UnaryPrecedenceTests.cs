using Bantam;
using Bantam.Paselets;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Prefix = System.Tuple<SimpleParser.TokenType, SimpleParser.IPrefixParselet<SimpleParser.TokenType>>;
using Infix = System.Tuple<SimpleParser.TokenType, SimpleParser.InfixParselet<SimpleParser.TokenType>>;


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
