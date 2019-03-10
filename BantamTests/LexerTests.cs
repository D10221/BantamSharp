using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;
using System.Linq;

namespace BantamTests
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void NextTest()
        {
            var lexer = Lexer.From(
                Tokenizer.From(Parser.Punctuators).Tokenize(
                    "a + b - abc",
                    TokenFactory.From(Parser.Punctuators.Reverse()
                    )));

            var a = lexer.Next();
            Assert.AreEqual("a", a.ToString());
            var plus = lexer.Next();
            Assert.AreEqual("+", plus.ToString());
            var b = lexer.Next();
            Assert.AreEqual("b", b.ToString());
            var m = lexer.Next();
            Assert.AreEqual("-", m.ToString());

            //This Lexer Can handle Words
            var abc = lexer.Next();
            Assert.AreEqual("abc", abc.ToString());
        }

        [TestMethod]
        public void Test2()
        {
            const string text = "((-a) * b)";
            var lexer = Lexer.From(Tokenizer.From(Parser.Punctuators).Tokenize(text, TokenFactory.From(Parser.Punctuators.Reverse())));
            var splitted = text.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray();
            foreach (var c in splitted)
            {
                Assert.AreEqual(lexer.Next().ToString(), c.ToString());
            }
        }

        [TestMethod]
        public void InputTextTest()
        {
            var text = "a+b";
            var lexer = Lexer.From(
                tokens: Tokenizer.From(Parser.Punctuators).Tokenize(text, tokenFactory: TokenFactory.From(Parser.Punctuators.Reverse())));
            Assert.AreEqual("a+b", lexer.ToString());
        }
    }
}
