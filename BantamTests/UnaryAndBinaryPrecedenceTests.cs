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
            // Actual:<(((-a)*b)-(a*b))>
            Assert.AreEqual(expected: "((-a)*b)", actual); //Fails           
        }

        [TestMethod]
        public void TestMethod2()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("!a + b"));
            //Assert.AreEqual failed. Expected:<((!a)+b)>. Actual:<((!a)+(b!))>
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
            // Assert.AreEqual failed. Expected:<(-(a!))>. Actual:<((-(a!))-(a!))>
            Assert.AreEqual(expected: "(-(a!))", actual); //Fails   
        }

        [TestMethod]
        public void TestMethod5()
        {
            string actual = Printer.Create().Print(
                ParserFactory.Create()("!a!"));
            // Assert.AreEqual failed. Expected:<(!(a!))>. Actual:<(!((a!)!))>. 
            Assert.AreEqual(expected: "(!(a!))", actual); //Fails   
        }
                
        [TestMethod]
        public void BinaryPrecedenceTest()
        {
            var actual = Printer.Create().Print(
                ParserFactory.Create()("(a = ((b + (c * (d ^ e))) - (f / g)))"));
            // Assert.AreEqual failed. Expected:<(a=((b+(c*(d^e)))-(f/g)))>. Actual:<(a=((b+(c*(d^e)))-(f/g)))((a=((b+(c*(d^e)))-(f/g))))>
            Assert.AreEqual(expected: "(a=((b+(c*(d^e)))-(f/g)))", actual); //Fails               
        }
    }
}
