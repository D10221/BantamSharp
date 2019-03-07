using SimpleParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bantam
{
    /// <summary>
    /// A very primitive lexer. Takes a string and splits it into a series of
    // Tokens. Operators and punctuation are mapped to unique keywords. Names,
    // which can be any series of letters, are turned into NAME tokens. All other
    // characters are ignored (except to separate names). Numbers and strings are
    // not supported. This is really just the bare minimum to give the parser
    // something to work with.
    /// </summary>
    public class Lexer : ILexer<TokenType>
    {
        private readonly IDictionary<char, TokenType> _punctuators;
        private readonly IEnumerable<IToken<TokenType>> _tokens;
        private readonly IEnumerator<IToken<TokenType>> _enumerator;

        /// <summary>
        /// Creates a new Lexer to tokenize the given string.
        /// @param text String to tokenize.
        /// </summary>
        public Lexer(string text, IDictionary<TokenType, char> tokenTypes)
        {
            _punctuators = tokenTypes.ToDictionary(x => x.Value, x => x.Key);
            _tokens = text.ToCharArray().Where(c => !char.IsWhiteSpace(c) && c != Char.MinValue).Select(Tokenize);
            _enumerator = _tokens.GetEnumerator();
        }
        IToken<TokenType> Tokenize(char x)
        {

            var token = TryGetPunctuator(x);
            if (token.HasValue)
            {
                return token;
            }
            token = TryGetLetter(x);
            if (token.HasValue)
            {
                return token;
            }
            throw new Exception($"Invalid Token:'{x}'");
        }
        private IToken<TokenType> TryGetPunctuator(char c)
        {
            if (!_punctuators.TryGetValue(c, out var t))
            {
                return Token<TokenType>.Empty();
            }
            return Token.New(t, c.ToString());
        }

        private IToken<TokenType> TryGetLetter(char c)
        {
            var input = c.ToString();
            return LooksLikeLetter(input) ? new Token<TokenType>(TokenType.NAME, input) : Token<TokenType>.Empty();
        }

        private static bool LooksLikeLetter(string input)
        {
            var regex = new Regex(@"\w");
            bool isMatch = regex.IsMatch(input);
            return isMatch;
        }

        public IToken<TokenType> Next()
        {
            if (_enumerator.MoveNext())
                return _enumerator.Current;
            return Token<TokenType>.Empty();
        }

        public string Text
        {
            get
            {
                return _tokens.Select(a => a.ToString()).Aggregate((a, b) => a + b);
            }
        }
    }
}