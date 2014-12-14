﻿using Bantam.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests.Expressions
{
    [TestClass]
    public class CallExpressionTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var nameExpression = new NameExpression("a");
            var exp = new CallExpression(nameExpression, null);
            var _builder = new Builder();
            exp.Print(_builder);
            string actual = _builder.ToString();
            Assert.AreEqual("a()", actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var nameExpression = new NameExpression("");
            var exp = new CallExpression(nameExpression, null);
            var _builder = new Builder();
            exp.Print(_builder);
            string actual = _builder.ToString();
            Assert.AreEqual("()", actual);
        }
    }
}