using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class EmptyExpressionTests
    {
        [TestMethod]
        public void Test1()
        {
            var e = ParserFactory.Create()("");
            Assert.AreEqual("",
                actual: Printer.Create().Print(e)
                );
        }
        [TestMethod]
        public void Test2()
        {
            var e = ParserFactory.Create()("");
            Assert.IsInstanceOfType(e,
                typeof(EmptyExpression<TokenType>)
                );
        }
         [TestMethod]
        public void Test3()
        {
            var e = (EmptyExpression<TokenType>)ParserFactory.Create()("");
            Assert.AreEqual(
                TokenType.NONE,
                actual: e.Token.TokenType
                );
        }
    }
}
