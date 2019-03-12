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
            var e = Parser.Parse("");
            Assert.AreEqual("",
                actual: Printer.Default.Print(e)
                );
        }
        [TestMethod]
        public void Test2()
        {
            var e = Parser.Parse("");
            Assert.IsInstanceOfType(e,
                typeof(EmptyExpression<TokenType>)
                );
        }
         [TestMethod]
        public void Test3()
        {
            var e = (EmptyExpression<TokenType>)Parser.Parse("");
            Assert.AreEqual(
                TokenType.NONE,
                actual: e.Token.TokenType
                );
        }
    }
}
