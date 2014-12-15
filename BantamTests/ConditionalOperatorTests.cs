using Bantam.Paselets;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Prefix = System.Tuple<SimpleParser.TokenType, SimpleParser.IPrefixParselet>;
using Infix = System.Tuple<SimpleParser.TokenType, SimpleParser.InfixParselet>;

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

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.ASSIGN, new AssignParselet()),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM,InfixType.Left)),
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT,InfixType.Left)),
                new Infix(TokenType.SLASH, new BinaryOperatorParselet(Precedence.PRODUCT,InfixType.Left)),
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "((a + b) ? (c * d) : (e / f))";
            Assert.AreEqual(expected, actual);      
        }
        
        
    }
}
