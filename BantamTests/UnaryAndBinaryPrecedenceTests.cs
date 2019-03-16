using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace BantamTests
{
    [TestClass] //Unary and binary predecence
    public class UnaryAndBinaryPrecedenceTests
    {
        [TestMethod]
        public void TestMethod()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("a+(b+c)"));
            Assert.AreEqual(
                expected: "(a+(b+c))",
                new Regex("\\s").Replace(actual.Trim(), ""));
        }

        // Unary and binary predecence.
        [TestMethod]
        public void TestMethod1()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("-a * b"));
            Assert.AreEqual(expected: "((-a)*b)", actual); //Fails           
        }

        [TestMethod]
        public void TestMethod2()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("!a + b"));
            Assert.AreEqual(expected: "((!a)+b)", actual); //Fails     
        }

        [TestMethod]
        public void TestMethod3()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("~a ^ b"));
            Assert.AreEqual(expected: "((~a)^b)", actual); //Fails     
        }

        [TestMethod]
        public void TestMethod4()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("-a!"));
            Assert.AreEqual(expected: "(-(a!))", actual); //Fails   
        }

        [TestMethod]
        public void TestMethod5()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("!a!"));
            Assert.AreEqual(expected: "(!(a!))", actual); //Fails   
        }

        // Binary(int) Precedence.
        //test("a = b + c * d ^ e - f / g", "(a = ((b + (c * (d ^ e))) - (f / g)))");
        [TestMethod]
        public void BinaryPrecedenceTest()
        {
            var actual = Printer.Create().Print(
                ParserFactory.Create()("(a = ((b + (c * (d ^ e))) - (f / g)))"));
            Assert.AreEqual(expected: "(a=((b+(c*(d^e)))-(f/g)))", actual); //Fails               
        }
    }
}
