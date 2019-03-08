
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class AssignExpressionTests
    {
        [TestMethod]
        public void AssignExpressionTest()
        {
            var e = new AssignExpression(new NameExpression("a"), new NameExpression("b"));
            var builder = new Builder();
            e.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(a=b)", actual);
        }

        [TestMethod]
        public void AssignExpressionTest2()
        {
            var token = Token.From(TokenType.NONE, "b");
            ISimpleExpression right = new FunctionCallExpression(new NameExpression(token), null);
            var expresison = new AssignExpression(new NameExpression("a"), right);
            var builder = new Builder();
            expresison.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(a=b())", actual);
        }
    }
}
