using System;
using System.Linq;
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ILexer = SimpleParser.ILexer<Bantam.TokenType, char>;

namespace BantamTests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void NextTest()
        {
            var lexer = new Lexer("a + b - abc", Parser.TokenConfig);

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
        public void Test2()
        {
            const string expression = "((-a) * b)";
            var a = new Lexer(expression, Parser.TokenConfig);
            var splitted = expression.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray();
            foreach (var c in splitted)
            {
                Assert.AreEqual(a.Next().GetText(), c.ToString());
            }
        }

        [TestMethod]
        public void InputTextTest()
        {
            ILexer lexer = new Lexer("a+b", Parser.TokenConfig);
            Assert.AreEqual("a+b", lexer.InputText);
        }
    }
}
