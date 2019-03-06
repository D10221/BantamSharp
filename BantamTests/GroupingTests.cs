using Bantam;


using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.IParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.InfixParselet<Bantam.TokenType, char>>;

namespace BantamTests
{
    [TestClass]
    public class GroupingTests
    {
        [TestMethod]
        public void TestMethod1()
        {            
            const string expression = "a + (b + c) + d";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.PLUS,new BinaryOperatorParselet((int) Precedence.SUM, InfixType.Left,tokenConfig))                
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "((a + (b + c)) + d)";
            Assert.AreEqual(expected, actual);    
        }
        
        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "a ^ (b + c)";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.CARET,new BinaryOperatorParselet((int) Precedence.POSTFIX, InfixType.Right,tokenConfig)),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet((int) Precedence.SUM, InfixType.Right,tokenConfig))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "(a ^ (b + c))";
            Assert.AreEqual(expected, actual);    
        }
        
        [TestMethod]
        public void TestMethod3()
        {                            
            const string expression = "(!a)!";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet()),
                new Prefix(TokenType.BANG, new PrefixOperatorParselet((int) Precedence.PREFIX,tokenConfig))
            };

            Infix[] infixes =
            {
                new Infix(TokenType.BANG,new PostfixOperatorParselet((int) Precedence.POSTFIX,tokenConfig))                                
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "((!a)!)";
            Assert.AreEqual(expected, actual);    
        }
    }
}
