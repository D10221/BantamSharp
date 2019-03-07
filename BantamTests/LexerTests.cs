using System;
using System.Linq;
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
            var lexer = new Lexer("a + b - abc", Parser.punctuators);

            var a = lexer.Next();
            Assert.AreEqual("a", a.Text);
            var plus = lexer.Next();
            Assert.AreEqual("+", plus.Text);
            var b = lexer.Next();
            Assert.AreEqual("b", b.Text);
            var m = lexer.Next();
            Assert.AreEqual("-", m.Text);

            //This Lexer Can't handle Words
            /*var abc = lexer.Next();
            Assert.Equals("abc", abc.GetText());*/
        }

        [TestMethod]
        public void Test2()
        {
            const string expression = "((-a) * b)";
            var a = new Lexer(expression, Parser.punctuators);
            var splitted = expression.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray();
            foreach (var c in splitted)
            {
                Assert.AreEqual(a.Next().Text, c.ToString());
            }
        }

        [TestMethod]
        public void InputTextTest()
        {
            ILexer<TokenType> lexer = new Lexer("a+b", Parser.punctuators);
            Assert.AreEqual("a+b", lexer.Text);
        }
    }
}
