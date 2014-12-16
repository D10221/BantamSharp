using System;
using Bantam;
using Bantam.Expressions;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseException = SimpleParser.ParseException<Bantam.TokenType>;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;
using Prefix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>>;
using Infix = System.Tuple<Bantam.TokenType, SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>>;
using ParserConfig = SimpleParser.ParserConfig<Bantam.TokenType, char>;
using ParserMap = SimpleParser.ParserMap<Bantam.TokenType, char>;
using IParserMap = SimpleParser.IParserMap<Bantam.TokenType, char>;
using Parser = SimpleParser.Parser<Bantam.TokenType, char>;
using IBuilder = SimpleParser.IBuilder<char>;
using ISimpleExpression = SimpleParser.Expressions.ISimpleExpression<char>;
using IParser = SimpleParser.IParser<Bantam.TokenType, char>;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<Bantam.TokenType, char>;
using InfixParselet = SimpleParser.Parselets.InfixParselet<Bantam.TokenType, char>;

namespace BantamTests.Expressions
{
    [TestClass]
    public class OperatorExpressionTests
    {
        [TestMethod]
        public void OperatorExpressionTest()
        {
            var left = new FakeExpression("x");
            var right =  new FakeExpression("y");
            const TokenType @operator = TokenType.PLUS;
            var tokenConfig = new TokenConfig();
            var expression = new OperatorExpression(tokenConfig,left,@operator,right);
            IBuilder builder = new FakeBuilder();
            expression.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(x + y)", actual);
        }
        
        [TestMethod]
        public void OperatorExpressionTest2()
        {
            Exception ex = null;
            try
            {
                var tokenConfig = new TokenConfig();
                var left = new FakeExpression("x");
                var right =  new FakeExpression("y");
                const TokenType @operator = TokenType.NONE;
                var expression = new OperatorExpression(tokenConfig,left,@operator,right);
                //Just to stop the compiler warning becasue is not used 
                Assert.IsNotNull(expression);

            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.IsNotNull(ex as ParseException);      
           
        }
    }
}
