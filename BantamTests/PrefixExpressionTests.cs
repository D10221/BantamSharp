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
        // [TestMethod]
        // public void Test2()
        // {
        //     var e = Parser.Parse("@a");
        //     var prefix = (PrefixExpression)e;
        //     var token = (Token<TokenType>)prefix.Token;
        //     Assert.AreEqual(
        //         TokenType.AT,
        //         actual: token.TokenType
        //     );
        //     Assert.AreEqual("a", ((Token<TokenType>)prefix.Right.Token).Value);
        //     Assert.AreEqual(
        //         "@",
        //         actual: token.Value
        //     );
        //     Assert.AreEqual("(@a)", actual: Printer.Default.Print(e));
        // }
        // [TestMethod]
        // public void Test3(){
        //     var e = Parser.Parse("@a=+b");
        //      Assert.AreEqual("(@a)=+b", actual: Printer.Default.Print(e));
        // }
    }
}
