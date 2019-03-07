using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass]
    public class GroupingTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string expression = "a + (b + c) + d";
            string actual = Parser.Parse(expression);
            const string expected = "((a + (b + c)) + d)";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string expression = "a ^ (b + c)";
            string actual = Parser.Parse(expression);
            const string expected = "(a ^ (b + c))";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            const string expression = "(!a)!";
            string actual = Parser.Parse(expression);
            const string expected = "((!a)!)";
            Assert.AreEqual(expected, actual);
        }
    }
}
