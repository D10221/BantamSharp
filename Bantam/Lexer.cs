using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.RegularExpressions;
using SimpleParser;
using IToken = SimpleParser.IToken<Bantam.TokenType>;
using ILexer = SimpleParser.ILexer<Bantam.TokenType, char>;

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
    public class Lexer: ILexer
    {
        /// <summary>
        /// Creates a new Lexer to tokenize the given string.
        /// @param text String to tokenize.
        /// </summary>
        public Lexer(string text, TokenConfig tokenConfig)
        {
            TokenConfig = tokenConfig;
            _text = text.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray();
            _enumerator = text.ToCharArray().Cast<Char>().GetEnumerator();
        }

        public TokenConfig TokenConfig { get; set; }

        private IToken TryGetPunctuator(char c)
        {
            var token = TokenConfig.TokenTypes
                .GetTokenType(c)
                .Extract(tt => new Token<TokenType>(tt.TknType(), AsString(tt)));
            return token;
        }

        private static string AsString(Tuple<TokenType, char> tt)
        {
            return tt.Value().ToString();
        }

        private IToken TryGetLetter(char c)
        {            
            var input = c.ToString();
            return LooksLikeLetter(input) ? new Token<TokenType>( TokenType.NAME,input) : Token<TokenType>.Empty();
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
                Ok = ok;Eof = eof;Char = c;
            }

            public char Char { get; private set; }

            public bool Eof { get; private set; }

            public bool Ok { get; private set; }

            public override string ToString()
            {
                return Char.ToString();
            }
        }

        readonly Func<IEnumerator<Char>, Iteration> moveNext = enumerator =>
        {
            var ok = enumerator.MoveNext();
            var eof = !ok;
            return new Iteration(ok, eof,ok ? enumerator.Current : default(Char));
        };

        public IToken Next()
        {
            var token = Token<TokenType>.Empty();            

            var eof = false;                        

            for (var current = moveNext(_enumerator);current.Ok; current = moveNext(_enumerator))
            {
                eof = current.Eof;
                token = TryGetPunctuator(current.Char);
                if (token != null) break;
                
                token = TryGetLetter(current.Char);
                if (token.HasValue ) break;                
            }
            
            if (eof) return new Token<TokenType>(TokenType.EOF, "");

            return token ;                                
        }

        public IEnumerable<char> InputText
        {
            get
            {
                return _text.Select(a=> a.ToString()).Aggregate((a, b) => a + b);
            }
        }
        private readonly IEnumerable<char> _text;
        private readonly IEnumerator<char> _enumerator;
    }
}