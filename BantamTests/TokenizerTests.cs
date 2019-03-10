using System.Linq;
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void Test1()
        {
            var tokenizer = Tokenizer.From(Parser.Punctuators);
            var tokenFactory = TokenFactory.From(
                Parser.Punctuators.Reverse()
            );
            var tokens = tokenizer
                //TODO: Solve = == != ! = 
                .Tokenize("a == 1", tokenFactory)
                .ToArray();

            Assert.AreEqual(
                TokenType.NAME,
                tokens[0].TokenType
            );
            Assert.AreEqual(
                TokenType.EQUALS,
                tokens[1].TokenType
            );
        }
    }
}