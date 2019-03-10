using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Bantam;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleParser;

namespace BantamTests
{
    [TestClass]
    public class TokenizerTests
    {
        enum TokenType
        {
            None,
            Assign,
            Equals,
            Number,
            Name
        }
        class TokenFactory : ITokenFactory<TokenType>
        {
            IDictionary<string, TokenType> _punctuators;
            public TokenFactory(IDictionary<string, TokenType> punctuators)
            {
                _punctuators = punctuators;
            }

            public IToken<TokenType> GetToken(string x)
            {
                foreach (var kv in _punctuators)
                {
                    if (x == kv.Key)
                    {
                        return Token.From(kv.Value, x);
                    }
                }
                if (new Regex("^[a-zA-z_]+$").IsMatch(x))
                {
                    return Token.From(TokenType.Name, x);
                }
                if (new Regex("^\\d+$").IsMatch(x))
                {
                    return Token.From(TokenType.Number, x);
                }
                return Token.Empty(default(TokenType), x);
            }
        }
        [TestMethod]
        public void Test1()
        {
            var punctuators = new Dictionary<TokenType, string>{

                {TokenType.Assign, "="},
                {TokenType.Equals, "=="}
            };

            var tokenizer = Tokenizer.From(punctuators);

            var factory = new TokenFactory(punctuators.Reverse());

            var tokens = tokenizer
                //TODO: Solve = == != ! = 
                .Tokenize("a == 1", factory)
                .ToArray();

            Assert.AreEqual(
                TokenType.Name,
                tokens[0].TokenType
            );
            Assert.AreEqual(
                TokenType.Equals,
                tokens[1].TokenType
            );
            Assert.AreEqual(
              TokenType.Number,
              tokens[2].TokenType
          );
        }
    }
}