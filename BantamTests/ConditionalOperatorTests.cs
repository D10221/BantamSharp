using Bantam;


using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.IParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.InfixParselet<Bantam.TokenType, char>>;

namespace BantamTests
{
    [TestClass]
    public class ConditionalOperatorTests
    {
        [TestMethod]
        public void TestMethod1()
        {           
            const string expression = "a ? b : c ? d : e";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.QUESTION,new ConditionalParselet()),                
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "(a ? b : (c ? d : e))";
            Assert.AreEqual(expected, actual);      
        }
        
        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "a ? b ? c : d : e";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.QUESTION,new ConditionalParselet()),                
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "(a ? (b ? c : d) : e)";
            Assert.AreEqual(expected, actual);      
        }
        
        [TestMethod]
        public void TestMethod3()
        {                        
            const string expression = "a + b ? c * d : e / f";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.PLUS, new BinaryOperatorParselet((int) Precedence.SUM, InfixType.Left,tokenConfig)),
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet((int) Precedence.PRODUCT, InfixType.Left,tokenConfig)),
                new Infix(TokenType.SLASH, new BinaryOperatorParselet((int) Precedence.PRODUCT, InfixType.Left,tokenConfig)),
                new Infix(TokenType.QUESTION, new ConditionalParselet()),
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "((a + b) ? (c * d) : (e / f))";
            Assert.AreEqual(expected, actual);      
        }
        
        
    }
}
