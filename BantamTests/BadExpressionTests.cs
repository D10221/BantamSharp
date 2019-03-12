using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using System;

namespace BantamTests
{
    /// <summary>
    /// Test NON working expressions
    /// </summary>
    [TestClass]
    public class BadExpressionTests
    {
        [TestMethod]
        public void DoesntWork1()
        {
            var parse = ParserFactory.Create();
            Exception ex = null;
            try
            {
                // '\' is not known token 
                // tokenizer/tokenfactory: exception
                var e = parse("A \\");
                Assert.AreEqual("",
                    actual: Printer.Default.Print(e)
                    );

            }
            catch (Exception e)
            {
                ex = e;
            }
            Assert.IsInstanceOfType(ex, typeof(ParseException));
        }
    }
}
