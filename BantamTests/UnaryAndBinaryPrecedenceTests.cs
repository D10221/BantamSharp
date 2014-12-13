using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass] //Unary and binary predecence
    public class UnaryAndBinaryPrecedenceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
           // Assert.AreEqual(Parse( "-a * b"),"((-a) * b)"); //Fails
            Assert.AreEqual(Parse("-a * b"), "(-(a * b))"); 
            Assert.AreEqual(Parse("(-a) * b"), "((-a) * b)");
            /*Assert.AreEqual(Parse("!a + b"), "((!a) + b)");
            Assert.AreEqual(Parse("~a ^ b"), "((~a) ^ b)");
            Assert.AreEqual(Parse("-a!"), "(-(a!))");
            Assert.AreEqual(Parse("!a!"), "(!(a!))");*/
        }

        /// BinaryPrecedence.
            //test("a = b + c * d ^ e - f / g", "(a = ((b + (c * (d ^ e))) - (f / g)))");

        [TestMethod]
        public void BinaryPrecedenceTest()
        {
            Assert.AreEqual("(a = ((b + (c * (d ^ e))) - (f / g)))", 
                Parse("a = b + c * d ^ e - f / g"));                             
        }

        public static string Parse(string source)
        {
            var lexer = new Lexer(source);
            var parser = new BantamParser(lexer);
            var result = parser.ParseExpression();
            var builder = new Builder();
            result.Print(builder);
            var actual = builder.ToString();
            return actual;
        }
    }
}
