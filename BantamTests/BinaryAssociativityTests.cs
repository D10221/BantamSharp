using Bantam.Paselets;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Prefix = System.Tuple<SimpleParser.TokenType, SimpleParser.IPrefixParselet>;
using Infix = System.Tuple<SimpleParser.TokenType, SimpleParser.InfixParselet>;

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

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM,InfixType.Left)),
                new Infix(TokenType.MINUS, new BinaryOperatorParselet(Precedence.SUM,InfixType.Left)),
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

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT,InfixType.Left)),
                new Infix(TokenType.SLASH, new BinaryOperatorParselet(Precedence.PRODUCT,InfixType.Left)),
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "((a * b) / c)";
            Assert.AreEqual(expected, actual); //Fails           
        }

        [TestMethod]
        public void TestMethod4()
        {            
            const string expression = "a ^ b ^ c";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.CARET, new BinaryOperatorParselet(Precedence.PRODUCT,InfixType.Right)),                
            };

            var actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "(a ^ (b ^ c))";
            Assert.AreEqual(expected, actual); //Fails           
        }
    }
}
