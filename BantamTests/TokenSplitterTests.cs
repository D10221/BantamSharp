using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace TokenSplitterTests
{
    [TestClass]
    public class TokenSplitterTests
    {
        static string Join(IEnumerable<string> input)
        {
            return input.Aggregate((a, b) => a + ";" + b);
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
    }
}
