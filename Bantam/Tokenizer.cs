using SimpleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bantam
{
    public static class TokenizerExtensions
    {
        public static IEnumerable<IToken<TokenType>> Tokenize(this IDictionary<TokenType, char> punctuators, string text)
        {
            return new Tokenizer(punctuators).Tokenize(text);
        }
    }
    /// <summary>
    /// Convert text to tokens
    /// </summary>
    class Tokenizer
    {
        private readonly IDictionary<char, TokenType> _punctuators;
        
        public Tokenizer(IDictionary<TokenType, char> tokenTypes)
        {
            _punctuators = tokenTypes.ToDictionary(x => x.Value, x => x.Key);            
        }

        public IEnumerable<IToken<TokenType>> Tokenize(string text)
        {
            return text.ToCharArray().Where(c => !char.IsWhiteSpace(c) && c != char.MinValue).Select(Tokenize);
        }

        private IToken<TokenType> Tokenize(char x)
        {
            var token = TryGetPunctuator(x);
            if (!token.IsEmpty)
            {
                return token;
            }
            token = TryGetLetter(x);
            if (!token.IsEmpty)
            {
                return token;
            }
            throw new Exception($"Invalid Token:'{x}'");
        }

        private IToken<TokenType> TryGetPunctuator(char c)
        {
            if (!_punctuators.TryGetValue(c, out var t))
            {
                return Token.Empty(default(TokenType));
            }
            return Token.New(t, c.ToString());
        }

        private IToken<TokenType> TryGetLetter(char c)
        {
            var input = c.ToString();
            return LooksLikeLetter(input) ? new Token<TokenType>(TokenType.NAME, input) : Token.Empty<TokenType>();
        }

        private static bool LooksLikeLetter(string input)
        {
            var regex = new Regex(@"\w");
            bool isMatch = regex.IsMatch(input);
            return isMatch;
        }
    }
}