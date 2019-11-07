using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

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
            Assert.ThrowsException<ParseException>(() =>
            {
                // '\' is not known token 
                // tokenizer/tokenfactory: exception
                var parse = ParserFactory.Create();
                parse("A \\");
            });
        }
        [TestMethod]
        public void DoesntWork2()
        {
            Assert.ThrowsException<ParseException>(() =>
            {
                // Parselet Throws?
                var parse = ParserFactory.Create();
                parse("=");
            });
        }
         [TestMethod]
        public void DoesntWork3()
        {
            // Group Or Function 
            Assert.ThrowsException<ParseException>(() =>
            {
                // Parselet Throws?
                var parse = ParserFactory.Create();
                parse("()");
            });
        }
    }
}
