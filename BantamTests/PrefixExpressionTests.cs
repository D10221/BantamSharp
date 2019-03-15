﻿using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class PrefixExpressionTests
    {
        [TestMethod]
        public void Test1()
        {
            var exression = new PrefixExpression(Token.From(TokenType.NONE, "-"), NameExpression.From("A"));
            var builder = new Builder();
            exression.Visit(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(-A)", actual);
        }
        [TestMethod]
        public void Test2()
        {
            var e = ParserFactory.Create()("@a");
            var prefix = e as PrefixExpression;
            var token = prefix?.Token as Token<TokenType>;
            Assert.AreEqual(
                TokenType.AT,
                actual: token?.TokenType
            );
            Assert.AreEqual("a", (prefix.Right.Token as Token<TokenType>)?.Value?.ToString());
            Assert.AreEqual(
                "@",
                actual: token?.Value?.ToString()
            );
            var actual = Printer.Default.Print(e);
            Assert.AreEqual("(@a)", actual);
        }
        
        //[TestMethod]
        //public void Test3(){
        //    // Parse this expression ? 
        //    var e = Parser.Parse("set @a=+b");
        //     Assert.AreEqual("set(@a,(+b))", actual: Printer.Default.Print(e));
        //}
    }
}
