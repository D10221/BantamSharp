using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass] //?
    public class UnaryPrecedenceTests
    {
        [TestMethod]
        public void UnaryPrecedenceTest2()
        {
            const string expression = "a!!!"; ;
            var x = Parser.Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("(((a!)!)!)", x);
        }

        [TestMethod]
        public void UnaryPrecedenceTest1()
        {
            const string expression = "~!-+a";
            var actual = Parser.Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("(~(!(-(+a))))", actual);
        }
    }
}
