using System;
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class GroupingTests
    {
               
        
        [TestMethod]
        public void GroupingTest1()
        {
            Assert.AreEqual(
                expected: "((a+(b+c))+d)",
                actual: Printer.Create().Print(ParserFactory.Create()("a + (b + c) + d")));
        }

        [TestMethod]
        public void GroupingTest2()
        {
            Assert.AreEqual(
                expected: "(a^(b+c))",
                actual: Printer.Create().Print(ParserFactory.Create()("a ^ (b + c)")));
        }

        [TestMethod]
        public void GroupingTest3()
        {
            const string expected = "((!a)!)";
            Assert.AreEqual(
                expected,
                actual: Printer.Create().Print(ParserFactory.Create()("(!a)!")));
        }
        [TestMethod]
        public void GroupingTest4()
        {
            const string expected = "a";
            Assert.AreEqual(
                expected,
                actual: Printer.Create().Print(ParserFactory.Create()("a")));
        }
    }
}
