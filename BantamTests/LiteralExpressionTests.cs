using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using System;

namespace BantamTests
{
    [TestClass]
    public class LiteralExpressionTests
    {
        [TestMethod]
        public void Test1()
        {
            var parse = ParserFactory.Create();
            // WARNING: Literal is Ambiguos, because of ("|'|`)                
            var e = parse(@"A LIKE ""%x%x""");
            var actual = Printer.Create().Print(e);
            Assert.AreEqual(@"(ALIKE""%x%x"")", actual);
        }
    }
}
