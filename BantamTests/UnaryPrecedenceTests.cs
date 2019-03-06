using Bantam;


using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.IParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.InfixParselet<Bantam.TokenType, char>>;


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
            var parser = TestParser.Factory.CreateNew(prefixes: new[]
            {
                new Prefix(TokenType.NAME, new NameParselet()),               
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet()),
                
            }, infixes: new[]
            {
                new Infix(TokenType.BANG, new PostfixOperatorParselet((int) Precedence.POSTFIX,tokenConfig))
              
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
            var parser = TestParser.Factory.CreateNew(prefixes: new[]
            {
                new Prefix(TokenType.BANG, new PrefixOperatorParselet((int) Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.LEFT_PAREN,new GroupParselet()),
                new Prefix(TokenType.TILDE, new PrefixOperatorParselet((int) Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet((int) Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.PLUS, new PrefixOperatorParselet((int) Precedence.PREFIX,tokenConfig)),
                new Prefix(TokenType.NAME, new NameParselet())
            });

            var actual = parser.Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("(~(!(-(+a))))", actual);
        }
    }
}
