using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class PostfixExpressionTests
    {
        [TestMethod]
        public void Test1()
        {
            var expression = new PostfixExpression(new NameExpression("A"), "?");
            var builder = new Builder();
            expression.Print(builder);
            var actual = builder.Build();
            var expected = "(A?)";
            Assert.AreEqual(expected, actual);
        }
    }
}
