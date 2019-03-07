
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
            ISimpleExpression right = new NameExpression("b");
            var expresison = new AssignExpression(name, right);
            var builder = new Builder();
            expresison.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(a = b)", actual);
        }

        [TestMethod]
        public void AssignExpressionTest2()
        {
            const string name = "a";
            ISimpleExpression right = new CallExpression(new NameExpression("b"), null);
            var expresison = new AssignExpression(name, right);
            var builder = new Builder();
            expresison.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(a = b())", actual);
        }

        [TestMethod]
        public void AssignExpressionTest3()
        {
            const string name = "a";
            ISimpleExpression right = new FakeExpression("x");
            var expresison = new AssignExpression(name, right);
            var builder = new Builder();
            expresison.Print(builder);
            var actual = builder.Build();
            Assert.AreEqual("(a = x)", actual);
        }
    }
}
