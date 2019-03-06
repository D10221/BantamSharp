using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.InfixParselet<Bantam.TokenType, char>>;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.IParselet<Bantam.TokenType, char>>;

namespace BantamTests
{
    [TestClass]
    public class BinaryAssociativityTests
    {
        IDictionary<TokenType, char> tokenTypes = new Dictionary<TokenType, char>
            {
                { TokenType.LEFT_PAREN, '('},
                { TokenType.RIGHT_PAREN, ')'},
                { TokenType.COMMA, ','},
                { TokenType.ASSIGN, '='},
                { TokenType.PLUS, '+'},
                { TokenType.MINUS, '-'},
                { TokenType.ASTERISK, '*'},
                { TokenType.SLASH, '/'},
                { TokenType.CARET, '^'},
                { TokenType.TILDE, '~'},
                { TokenType.BANG, '!'},
                { TokenType.QUESTION, '?'},
                { TokenType.COLON, ';'},
                { TokenType.NAME, default(char)},
                { TokenType.EOF, default(char)}
            };

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

            string actual = Parser.Parse(expression);
            const string expected = "(a = (b = c))";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "a + b - c";
            string actual = Parser.Parse(expression);
            const string expected = "((a + b) - c)";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // BinaryAssociativity.
            // _expectationses.AddExpectation("a ^ b ^ c", "(a ^ (b ^ c))");
            const string expression = "a * b / c";
            string actual = Parser.Parse(expression);
            const string expected = "((a * b) / c)";
            Assert.AreEqual(expected, actual); //Fails           
        }

        [TestMethod]
        public void TestMethod4()
        {
            const string expression = "a ^ b ^ c";
            var actual = Parser.Parse(expression);
            const string expected = "(a ^ (b ^ c))";
            Assert.AreEqual(expected, actual); //Fails           
        }
    }
}
