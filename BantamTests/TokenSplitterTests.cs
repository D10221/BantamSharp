using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace TokenSplitterTests
{
    [TestClass]
    public class TokenSplitterTests
    {
        static string Join(IEnumerable<ITokenSource> input)
        {
            return input.Select(x => x.Value).Aggregate((a, b) => a + ";" + b);
        }
        [TestMethod]
        public void TestMethod1()
        {
            var input = "a=b==c";
            var actual = Join(new TokenSplitter(
                new[] { "=", "==" }
            ).Split(input));
            Assert.AreEqual("a;=;b;==;c", actual);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var input = "!=";
            var delimiters = new[] { "!=" };
            var actual = Join(
                new TokenSplitter(
                delimiters, TokenSplitterOptions.IncludeEmpty
            ).Split(input)
            );
            Assert.AreEqual("!=", actual);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var input = " ";
            var delimiters = new[] { " " };
            var actual = Join(new TokenSplitter(
                delimiters, TokenSplitterOptions.IncludeEmpty
            ).Split(input));
            Assert.AreEqual(" ", actual);
        }
        [TestMethod]
        public void TestMethod4()
        {
            var input = " ";
            var delimiters = new[] { " " };
            var actual = Join(
                new TokenSplitter(
                delimiters, TokenSplitterOptions.IncludeEmpty
            ).Split(input)
            );
            Assert.AreEqual(" ", actual);
        }
        [TestMethod]
        public void TestMethod5()
        {
            var input = "  ";
            var delimiters = new[] { " " };
            var actual = Join(
                new TokenSplitter(
                    delimiters, TokenSplitterOptions.IncludeEmpty
            ).Split(input)
            );
            Assert.AreEqual(" ; ", actual);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var input = "x=a=b==c && z!= y";
            var delimiters = new[] { "=", "==", "!=", "&&" };
            var actual = Join(new TokenSplitter(
                delimiters, TokenSplitterOptions.IncludeEmpty
            ).Split(input));
            Assert.AreEqual("x;=;a;=;b;==;c;;&&;;z;!=;;y", actual);
        }

        [TestMethod]
        public void LineDelimiters()
        {
            var input = "\na\rb\n\rc\r\n";
            string[] demiliters = new string[0];
            var tokens = new TokenSplitter(demiliters)
                .Split(input)
                .ToArray();
            Assert.AreEqual(
                "\n", tokens[0]?.ToString(), "Expected \\n"
            );
            Assert.AreEqual(
               "a", tokens[1]?.ToString()
           );
            Assert.AreEqual(
                "\r", tokens[2]?.ToString(), 
                "expected: \\r"
            );
            Assert.AreEqual(
                "b", tokens[3]?.ToString()
            );
            Assert.AreEqual(
                "\n\r", tokens[4]?.ToString()
 
            );
            Assert.AreEqual(
                "c", tokens[5]?.ToString()
            );
            Assert.AreEqual(
                "\r\n", tokens[6]?.ToString()
            );
        }
    }
}
