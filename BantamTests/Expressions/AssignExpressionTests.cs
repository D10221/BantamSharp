using Bantam.Expressions;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests.Expressions
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
            var actual = builder.ToString();
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
            var actual = builder.ToString();
            Assert.AreEqual("(a = b())", actual);
        }

        [TestMethod]
        public void AssignExpressionTest3()
        {
            const string name = "a";
            ISimpleExpression right = new FakeExpression("x");
            var expresison = new AssignExpression(name, right);
            var builder = new FakeBuilder();
            expresison.Print(builder);
            var actual = builder.ToString();
            Assert.AreEqual("(a = x)", actual);
        }

        

       
    }
}
