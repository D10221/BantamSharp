using Bantam.Common;
using Bantam.Paselets;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Prefix = System.Tuple<SimpleParser.TokenType, SimpleParser.IPrefixParselet>;
using Infix = System.Tuple<SimpleParser.TokenType, SimpleParser.InfixParselet>;

namespace BantamTests
{
    [TestClass] //Unary and binary predecence
    public class UnaryAndBinaryPrecedenceTests
    {

        [TestMethod]
        public void TestMethod()
        {
            const string expression = "a+(b+c)";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.PLUS, new PrefixOperatorParselet(Precedence.PREFIX)),
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left))
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

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX))
            };
            Infix[] infixes =
            {
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "((-a) * b)";
            Assert.AreEqual(expected, actual); //Fails           
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "!a + b";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.BANG, new PrefixOperatorParselet(Precedence.PREFIX))
            };
            Infix[] infixes =
            {
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "((!a) + b)";
            Assert.AreEqual(expected, actual); //Fails     
        }

        [TestMethod]
        public void TestMethod3()
        {
            const string expression = "~a ^ b";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.TILDE, new PrefixOperatorParselet(Precedence.PREFIX))
            };
            Infix[] infixes =
            {
                //TODO: 
                new Infix(TokenType.CARET, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "((~a) ^ b)";
            Assert.AreEqual(expected, actual); //Fails     
        }


        [TestMethod]
        public void TestMethod4()
        {
            const string expression = "-a!";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX))
            };

            Infix[] infixes =
            {
                new Infix(TokenType.BANG, new PostfixOperatorParselet(Precedence.POSTFIX))
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "(-(a!))";
            Assert.AreEqual(expected, actual); //Fails   
        }

        [TestMethod]
        public void TestMethod5()
        {
            const string expression = "!a!";

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.BANG, new PrefixOperatorParselet(Precedence.PREFIX))
            };

            Infix[] infixes =
            {
                new Infix(TokenType.BANG, new PostfixOperatorParselet(Precedence.POSTFIX))
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

            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
                new Prefix(TokenType.MINUS, new PrefixOperatorParselet(Precedence.PREFIX)),
                new Prefix(TokenType.LEFT_PAREN, new GroupParselet()),
                new Prefix(TokenType.PLUS, new PrefixOperatorParselet(Precedence.PREFIX)),                
            };

            Infix[] infixes =
            {
                new Infix(TokenType.ASSIGN, new AssignParselet()),
                new Infix(TokenType.LEFT_PAREN, new CallParselet()),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left)),
                new Infix(TokenType.MINUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left)),
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left)),
                new Infix(TokenType.SLASH, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Left)),
                new Infix(TokenType.CARET, new BinaryOperatorParselet(Precedence.PRODUCT, InfixType.Right))
            };

            var actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);

            const string expected = "(a = ((b + (c * (d ^ e))) - (f / g)))";
            Assert.AreEqual(expected, actual); //Fails               
        }
    }
}
