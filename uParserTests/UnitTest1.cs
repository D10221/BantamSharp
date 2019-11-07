using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace uParserTests
{
    [TestClass]
    public class Tests
    {
        /**
        * Parses the given chunk of code and verifies that it matches the expected
        * pretty-printed result.
        */
        void AreEqual(string source, string expected, [CallerMemberName] string caller = null)
        {
            var parse = Bantam.BantamParser();
            var expr = parse(source);
            var builder = Printer.Create();
            string actual = builder.Print(expr);
            Assert.AreEqual(expected, actual, false, caller);            
        }
        [TestMethod]
        public void Names()
        {
            AreEqual("a", "a");
        }
        [TestMethod]
        public void GroupingVsFunctionCall()
        {
            // Grouping vs Function Call            
            AreEqual("((a))", "((a))");
        }
        [TestMethod]
        public void Grouping()
        {                        
            AreEqual("(!a)!", expected: "(!a)!");
        }
        [TestMethod]
        public void Grouping2()
        {         
            AreEqual("a + (b + c) + d", "((a+(b+c))+d)");            
        }
        [TestMethod]
        public void Grouping3()
        {         
            AreEqual("a ^ (b + c)", "(a^(b+c))");            
        }
        [TestMethod]
        public void FunctionCall()
        {
            AreEqual("a()", "a()");
            AreEqual("a(b)", "a(b)");         
        }
        [TestMethod]
        public void FunctionCall1()
        {            
            AreEqual("a(b, c)", "a(b,c)");
        }
        [TestMethod]
        public void FunctionCall2()
        {
            AreEqual("a(b) + c(d)", "(a(b)+c(d))");
            AreEqual("a(b ? c : d, e + f)", "a((b?c:d),(e+f))");
        }
        [TestMethod]
        public void FunctionChain()
        {
            AreEqual("a(b)(c)", "a(b)(c)");
        }
        [TestMethod]
        public void Test1()
        {
            AreEqual("~a", "~a");
        }
        [TestMethod]
        public void UnaryPrecedence()
        {
            // Unary precedence.
            AreEqual("a!!!", "(((a!)!)!)");
            AreEqual("~!-+a", "(~(!(-(+a))))");
        }

        [TestMethod]
        public void UnaryAndBinaryPrecedence()
        {
            // Unary and binary predecence.
            AreEqual("-a * b", "((-a)*b)");
            AreEqual("!a + b", "((!a)+b)");
            AreEqual("~a ^ b", "((~a)^b)");
            AreEqual("-a!", "(-(a!))");
            AreEqual("!a!", "(!(a!))");
        }
        [TestMethod]
        public void BinaryPrecedence()
        {
            // Binary precedence.
            AreEqual("a = b + c * d ^ e - f / g", "(a=((b+(c*(d^e)))-(f/g)))");
        }
        [TestMethod]
        public void BinaryAssociativity()
        {
            // Binary associativity.
            AreEqual("a = b = c", "(a=(b=c))");
            AreEqual("a + b - c", "((a+b)-c)");
            AreEqual("a * b / c", "((a*b)/c)");
            AreEqual("a ^ b ^ c", "(a^(b^c))");
        }
        [TestMethod]
        public void ConditionalOperator()
        {
            // Conditional operator.
            AreEqual("a ? b : c ? d : e", "(a?b:(c?d:e))");
            AreEqual("a ? b ? c : d : e", "(a?(b?c:d):e)");
            AreEqual("a + b ? c * d : e / f", "((a+b)?(c*d):(e/f))");
        }
    }
}
