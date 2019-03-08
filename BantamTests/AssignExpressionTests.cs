
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
            const string name = "a";            
            var e = new AssignExpression(name, new NameExpression("b"));
            var builder = new Builder();
            e.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(a=b)", actual);
        }

        [TestMethod]
        public void AssignExpressionTest2()
        {
            const string name = "a";
            ISimpleExpression right = new FunctionCallExpression(new NameExpression("b"), null);
            var expresison = new AssignExpression(name, right);
            var builder = new Builder();
            expresison.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(a=b())", actual);
        }
    }
}
