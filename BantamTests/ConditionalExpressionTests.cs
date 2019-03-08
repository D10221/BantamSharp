
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class ConditionalExpressionTests
    {
        [TestMethod]
        public void ConditionalExpressionTest()
        {
            ISimpleExpression condition = new NameExpression("x");
            ISimpleExpression then = new NameExpression("y");
            ISimpleExpression @else = new NameExpression("z");
            var expresion = new ConditionalExpression(condition, then, @else);
            var _builder = new Builder();
            expresion.Print(_builder);
            var actual = _builder.ToString();
            Assert.AreEqual("(x?y:z)", actual);
        }
    }
}