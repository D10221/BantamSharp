using Bantam;
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
            exression.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(-A)", actual);
        }
        [TestMethod]
        public void Test2()
        {
            var e = Parser.Parse("@a");
            var prefix = e as PrefixExpression;
            var token = prefix?.Token as Token<TokenType>;
            Assert.AreEqual(
                TokenType.AT,
                actual: token?.TokenType
            );
            Assert.AreEqual("a", (prefix.Right.Token as Token<TokenType>)?.Value);
            Assert.AreEqual(
                "@",
                actual: token?.Value
            );
            Assert.AreEqual("@a", actual: Printer.Default.Print(e));
        }
        
        //[TestMethod]
        //public void Test3(){
        //    // Parse this expression ? 
        //    var e = Parser.Parse("set @a=+b");
        //     Assert.AreEqual("set(@a,(+b))", actual: Printer.Default.Print(e));
        //}
    }
}
