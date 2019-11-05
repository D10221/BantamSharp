using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using System.Collections.Generic;

namespace BantamTests
{
    [TestClass]
    public class AssignParseletTests
    {
        

        [TestMethod]
        public void Test1()
        {
            var parse = ParserFactory.Create();
            var e = parse("A=a");
            Assert.AreEqual("(A=a)", Printer.Create().Print(e));
        }      
    }
}
