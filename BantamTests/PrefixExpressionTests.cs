using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class PrefixExpressionTests
    {
        [TestMethod]
        public void PrefixExpressionTest()
        {
            var exression = new PrefixExpression(Token.From(TokenType.NONE, "-"), NameExpression.From("A"));
            var builder = new Builder();
            exression.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(-A)", actual);
        }
    }
}
