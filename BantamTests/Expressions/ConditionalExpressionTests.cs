using Bantam.Expressions;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests.Expressions
{
    [TestClass]
    public class ConditionalExpressionTests
    {
       
        [TestMethod]
        public void ConditionalExpressionTest()
        {
            ISimpleExpression condition = new FakeExpression("x");
            ISimpleExpression then =  new FakeExpression("y");
            ISimpleExpression @else =  new FakeExpression("z");
            var expresion = new ConditionalExpression(condition, then, @else);
            var _builder = new FakeBuilder();
            expresion.Print(_builder);
            var actual = _builder.Build();
            Assert.AreEqual("(x ? y : z)", actual);
        }
    }
}