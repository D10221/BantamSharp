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
            Exception ex = null;
            try
            {
                // '\' is not known token 
                // tokenizer/tokenfactory: expection
                var e = Parser.Parse("A \\");
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
