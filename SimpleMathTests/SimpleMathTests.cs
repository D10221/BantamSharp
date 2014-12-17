﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMaths;
using SimpleParser;

namespace SimpleMathTests
{
    [TestClass]
    public class SimpleMathTests
    {
      
        [TestMethod]
        public void SimpleMathTest()
        {
            Assert.AreEqual("040", Compile("20 + 20"));
            Assert.AreEqual("02", Compile("1 + 1"));
            Assert.AreEqual("01", Compile("1 /  1"));
            Assert.AreEqual("05", Compile("10 / 2"));

            Assert.AreEqual("00", Compile("10 / 0"));

            Assert.AreEqual("08", Compile("2 * 4"));
            Assert.AreEqual("00", Compile("0 * 5"));
            Assert.AreEqual("00", Compile("5 * 0"));
            Assert.AreEqual("08",Compile("4 + 2 * 2"));
            Assert.AreEqual("05",Compile("4 + 2 /  2"));
        }

        private static string Compile(string textExpression)
        {
            var tokens = new TokenConfig();
            ILexer<TokenType, string> lexer = new Lexer(textExpression, tokens);
            var parser = new Parser<TokenType, string>(lexer, tokens.ParserMap);
            var builder = new Builder();
            var expression = parser.ParseExpression();
            expression.Print(builder);
            var actual = builder.Build();
            return actual;
        }
    }
}