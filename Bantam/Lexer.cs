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
    public class Lexer : ILexer<TokenType, char>
    {
        private readonly IDictionary<TokenType, char> _tokenTypes;
        private readonly IDictionary<char, TokenType> _charTypes;

        /// <summary>
        /// Creates a new Lexer to tokenize the given string.
        /// @param text String to tokenize.
        /// </summary>
        public Lexer(string text, IDictionary<TokenType, char> tokenTypes)
        {
            _tokenTypes = tokenTypes;
            _charTypes = _tokenTypes.ToDictionary(x => x.Value, x => x.Key);
            _text = text.ToCharArray().Where(c => !char.IsWhiteSpace(c) && c != Char.MinValue).ToArray();
            _enumerator = _text.GetEnumerator();
        }

        private IToken<TokenType> TryGetPunctuator(char c)
        {
            if (!_charTypes.TryGetValue(c, out var t))
            {
                return Token<TokenType>.Empty();
            }
            return Token.New(t, c);
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

        private class Iteration
        {
            public Iteration(bool ok, bool eof, char c)
            {
                Ok = ok; Eof = eof; Char = c;
            }

            public char Char { get; private set; }

            public bool Eof { get; private set; }

            public bool Ok { get; private set; }

            public override string ToString()
            {
                return Char.ToString();
            }
        }

        private readonly Func<IEnumerator<char>, Iteration> _moveNext = enumerator =>
        {
            var ok = enumerator.MoveNext();
            var eof = !ok;
            return new Iteration(ok, eof, ok ? enumerator.Current : default(char));
        };

        public IToken<TokenType> Next()
        {
            for (var current = _moveNext(_enumerator); current.Ok; current = _moveNext(_enumerator))
            {
                if (current.Eof)
                {
                    return new Token<TokenType>(TokenType.EOF, "");
                }
                else
                {
                    {   // Punct, operations 
                        var token = TryGetPunctuator(current.Char);
                        if (token != null && token.HasValue)
                        {
                            return token;
                        }
                    }
                    {  // Values, letters numbers 
                        var token = TryGetLetter(current.Char);
                        if (token != null && token.HasValue)
                        {
                            return token;
                        }
                    }
                    throw new Exception($"Invalid Character:'{current}'");
                }
            }

            return Token<TokenType>.Empty();
        }

        public IEnumerable<char> InputText
        {
            get
            {
                return _text.Select(a => a.ToString()).Aggregate((a, b) => a + b);
            }
        }
        private readonly IEnumerable<char> _text;
        private readonly IEnumerator<char> _enumerator;
    }
}