using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using System;

namespace BantamTests
{
    [TestClass]
    class LiteralExpressionTests
    {
        [TestMethod]
        public void Test1()
        {
            var parse = ParserFactory.Create();
            Exception ex = null;
            try
            {                                                
                // And Right Side shoudl bea LITERAL Expression ? Or NAME or Function ?
                // WARNING: Literal is Ambiguos, because of ("|'|`)                
                var e = parse(@"A LIKE ""%x%x""");
                var actual = Printer.Default.Print(e);
                Assert.AreEqual(@"(A LIKE ""%x%x"")", actual);

            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.IsInstanceOfType(ex, typeof(ParseException));
        }
    }
}
