﻿using Bantam.Paselets;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using Prefix = System.Tuple<SimpleParser.TokenType, SimpleParser.IPrefixParselet>;
using Infix = System.Tuple<SimpleParser.TokenType, SimpleParser.InfixParselet>;

namespace BantamTests
{
    [TestClass]
    public class FunctionCallTests
    {
        // Function call.
        [TestMethod]
        public void TestMethod1()
        {            
            const string expression = "a()";
            var parsed = TestParser.Factory.CreateNew(new[]
            {
                new Prefix(TokenType.NAME, new NameParselet()),                
            }, new[] { new Infix(TokenType.LEFT_PAREN, new CallParselet()) }).Parse(expression);
            Assert.AreEqual("a()",parsed);
        }
        
        [TestMethod]
        public void TestMethod2()
        {           
            const string expression = "a(b)";
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes=
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet())
            };

            var parsed = TestParser.Factory.CreateNew(prefixes,infixes).Parse(expression);
            Assert.AreEqual("a(b)",parsed);
        }

        [TestMethod]
        public void TestMethod3()
        {            
            const string expression = "a(b, c)";
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet())
            };

            var parsed = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            Assert.AreEqual("a(b, c)", parsed);
        }
        
        [TestMethod]
        public void TestMethod4()
        {           
            const string expression = "a(b)(c)";
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet()),
            };

            Infix[] infixes =
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet()),
                new Infix(TokenType.PLUS,new BinaryOperatorParselet(Precedence.SUM,InfixType.Left))
            };

            var parsed = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            Assert.AreEqual(expression, parsed);
        }
        
        [TestMethod]
        public void TestMethod5()
        {
           const string expression = "a(b) + c(d)";
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet()),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM,InfixType.Left))
            };

            var parsed = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            Assert.AreEqual("(a(b) + c(d))", parsed);
        }
        
        [TestMethod]
        public void TestMethod6()
        {
            const string expression = "a(b ? c : d, e + f)";
            Prefix[] prefixes =
            {
                new Prefix(TokenType.NAME, new NameParselet())
            };

            Infix[] infixes =
            {
                new Infix(TokenType.LEFT_PAREN,new CallParselet()),
                new Infix(TokenType.QUESTION, new ConditionalParselet()),
                new Infix(TokenType.PLUS, new BinaryOperatorParselet(Precedence.SUM, InfixType.Left))
            };

            var parsed = TestParser.Factory.CreateNew(prefixes, infixes).Parse(expression);
            Assert.AreEqual("a((b ? c : d), (e + f))", parsed);
        }
    }
}