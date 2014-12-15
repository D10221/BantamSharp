using Bantam.Expressions;
using BantamTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests.Expressions
{
    [TestClass]
    public class NameExpressionTests
    {
        [TestMethod]
        public void NameExpressionTest()
        {            
            var expresion = new NameExpression("aa");
            var _builder = new FakeBuilder();
            expresion.Print(_builder);
            var actual = _builder.Build();
            Assert.AreEqual("aa", actual);
        }
        
        
        [TestMethod]
        public void NameExpressionTest1()
        {            
            var expresion = new NameExpression("a");
            var _builder = new FakeBuilder();
            expresion.Print(_builder);
            var actual = _builder.Build();
            Assert.AreEqual("a", actual);
        }
        
        [TestMethod]
        public void NameExpressionTest2()
        {            
            var expresion = new NameExpression("");
            var _builder = new FakeBuilder();
            expresion.Print(_builder);
            var actual = _builder.Build();
            Assert.AreEqual("", actual);
        }
        
        [TestMethod]
        public void NameExpressionTest3()
        {            
            var expresion = new NameExpression("whatever you want to put here\n");
            var _builder = new FakeBuilder();
            expresion.Print(_builder);
            var actual = _builder.Build();
            Assert.AreEqual("whatever you want to put here\n", actual);
        }
    }
}
