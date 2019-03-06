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
            const string expression = "a+(b+c)";

            string actual = Parser.Parse(expression);

            const string expected = "(a+(b+c))";

            Assert.AreEqual(expected, new Regex("\\s").Replace(actual.Trim(), ""));            
        }

        // Unary and binary predecence.
        [TestMethod]
        public void TestMethod1()
        {
            const string expression = "-a * b";
            string actual = Parser.Parse(expression);

            const string expected = "((-a) * b)";
            Assert.AreEqual(expected, actual); //Fails           
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "!a + b";
            string actual = Parser.Parse(expression);

            const string expected = "((!a) + b)";
            Assert.AreEqual(expected, actual); //Fails     
        }

        [TestMethod]
        public void TestMethod3()
        {
            const string expression = "~a ^ b";
            string actual = Parser.Parse(expression);

            const string expected = "((~a) ^ b)";
            Assert.AreEqual(expected, actual); //Fails     
        }


        [TestMethod]
        public void TestMethod4()
        {
            const string expression = "-a!";
            string actual = Parser.Parse(expression);
            const string expected = "(-(a!))";
            Assert.AreEqual(expected, actual); //Fails   
        }

        [TestMethod]
        public void TestMethod5()
        {
            const string expression = "!a!";
            string actual = Parser.Parse(expression);
            const string expected = "(!(a!))";
            Assert.AreEqual(expected, actual); //Fails   
        }

        // Binary(int) Precedence.
        //test("a = b + c * d ^ e - f / g", "(a = ((b + (c * (d ^ e))) - (f / g)))");
        [TestMethod]
        public void BinaryPrecedenceTest()
        {
            const string expression = "(a = ((b + (c * (d ^ e))) - (f / g)))";
            var actual = Parser.Parse(expression);
            const string expected = "(a = ((b + (c * (d ^ e))) - (f / g)))";
            Assert.AreEqual(expected, actual); //Fails               
        }
    }
}
