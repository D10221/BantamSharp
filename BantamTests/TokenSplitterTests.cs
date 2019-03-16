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
            var input = "\na\rb\n\rc d";
            string[] demiliters = new string[0];
            var tokens = new TokenSplitter(demiliters)
                .Split(input)
                .ToArray();
            // ...
            {
                ITokenSource x = tokens[0];
                Assert.AreEqual("a", x.Value);
                Assert.AreEqual(1, x.Line);
                Assert.AreEqual(0, x.Column);
            }
            // ....
            {
                ITokenSource x = tokens[1];
                Assert.AreEqual("b", x.Value);
                Assert.AreEqual(2, x.Line);
                Assert.AreEqual(0, x.Column);
            }
            // ....
            {
                ITokenSource x = tokens[2];
                Assert.AreEqual("c", x.Value);
                Assert.AreEqual(3, x.Line);
                Assert.AreEqual(0, x.Column, $"Expected {x} column to be {0} but got ${x.Column}");
            }
            // ....
            {
                ITokenSource x = tokens[3];
                Assert.AreEqual("d", x.Value);
                Assert.AreEqual(3, x.Line);
                Assert.AreEqual(2, x.Column);
            }

        }
    }
}
