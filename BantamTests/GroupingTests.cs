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
            var parse = ParserFactory.Create();
            var e = parse("(!a)!");
            var s = e.ToString();
            var sufix = (e as PostfixExpression);
            Assert.IsNotNull(sufix);

            var prefix = (sufix.Left as PrefixExpression);
            Assert.IsNotNull(prefix);

            var name = (prefix.Right as NameExpression);
            Assert.IsNotNull(name);

            name.Token.ExpectValue("a");
            
            Assert.AreEqual(
                expected,
                actual: Printer.Create().Print(e));
        }
        [TestMethod]
        public void GroupingTest31()
        {
            var parse = ParserFactory.Create();
            var e = parse("((a))");                        
            var name = (e as NameExpression);
            // is Function Call !
            Assert.IsNotNull(name);
            var s = name.ToString();    
            Assert.AreEqual("a", s)        ;
            name.Token.ExpectValue("a");            
            const string expected = "a";
            Assert.AreEqual(
                expected,
                actual: Printer.Create().Print(e));
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
