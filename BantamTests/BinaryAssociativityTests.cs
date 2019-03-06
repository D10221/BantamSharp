﻿using Bantam;


using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.InfixParselet<Bantam.TokenType, char>>;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.IParselet<Bantam.TokenType, char>>;

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
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.PLUS, new BinaryOperatorParselet((int) Precedence.SUM, InfixType.Left,tokenConfig)),
                new Infix(TokenType.MINUS, new BinaryOperatorParselet((int) Precedence.SUM, InfixType.Left,tokenConfig)),
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
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.ASTERISK, new BinaryOperatorParselet((int) Precedence.PRODUCT, InfixType.Left,tokenConfig)),
                new Infix(TokenType.SLASH, new BinaryOperatorParselet((int)Precedence.PRODUCT, InfixType.Left,tokenConfig)),
            };

            string actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "((a * b) / c)";
            Assert.AreEqual(expected, actual); //Fails           
        }

        [TestMethod]
        public void TestMethod4()
        {
            const string expression = "a ^ b ^ c";
            var tokenConfig = new TokenConfig();
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.CARET, new BinaryOperatorParselet((int) Precedence.PRODUCT, InfixType.Right,tokenConfig)),
            };

            var actual = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            const string expected = "(a ^ (b ^ c))";
            Assert.AreEqual(expected, actual); //Fails           
        }
    }
}
