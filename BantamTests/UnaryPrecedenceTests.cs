using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BantamTests
{
    [TestClass] //?
    public class UnaryPrecedenceTests
    {
        [TestMethod]
        public void UnaryPrecedenceTest1()
        {
            const string expression = "~!-+a";
            var s = Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("(~(!(-(+a))))", s);
        }
        
        [TestMethod]
        public void UnaryPrecedenceTest2()
        {
            const string expression = "a!!!";
            var s = Parse(expression);
            //NOTE: the extra ()
            Assert.AreEqual("(((a!)!)!)", s);
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
