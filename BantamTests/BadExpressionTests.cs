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
        [TestMethod]
        public void DoesntWork2()
        {
            var parse = ParserFactory.Create();
            // Parser doesnt find 'NEXT' 'B' ...'C'
            // Parser can't parse NAME NAME ....                                                
            var e = parse(@"A B C");
            var actual = Printer.Default.Print(e);
            Assert.AreEqual("A", actual);
        }        
    }
}
