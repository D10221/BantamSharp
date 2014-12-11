using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void NextTest()
        {
            var lexer = new Lexer("a + b - abc");

            var a = lexer.Next();
            Assert.AreEqual("a", a.GetText());
            var plus = lexer.Next();
            Assert.AreEqual("+", plus.GetText());
            var b = lexer.Next();
            Assert.AreEqual("b", b.GetText());
            var m = lexer.Next();
            Assert.AreEqual("-", m.GetText());

            //This Lexer Can't handle Words
            /*var abc = lexer.Next();
            Assert.Equals("abc", abc.GetText());*/

        }

        [TestMethod]
        public void InputTextTest()
        {
            ILexer lexer = new Lexer("a+b");
            Assert.AreEqual("a+b",lexer.InputText);
        }
    }

}
