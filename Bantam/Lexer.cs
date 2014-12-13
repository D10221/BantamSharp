using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleParser;

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
        public Lexer(String text)
        {
            mIndex = 0;
            _text = text.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray();
            _enumerator = text.ToCharArray().Cast<Char>().GetEnumerator();
        }

        private IToken TryGetPunctuator(char c)
        {
            var token = TokenTypes.Punctuators
                .TryGet(c)
                .Extract(tt => Token.New(tt.TknType(), tt.Char()));
            return token;
        }
        
        private IToken TryGetLetter(char c)
        {            
            var input = c.ToString(CultureInfo.InvariantCulture);
            return LooksLikeLetter(input) ? new Token( TokenType.NAME,input) : Token.Empty();
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
                return Char.ToString(CultureInfo.InvariantCulture);
            }
        }

        readonly Func<IEnumerator<Char>, Iteration> moveNext = (enumerator) =>
        {
            var ok = enumerator.MoveNext();
            var eof = !ok;
            return new Iteration(ok, eof,ok ? enumerator.Current : default(Char));
        };

        public IToken Next()
        {
            var token = Token.Empty();            

            var eof = false;                        

            for (var current = moveNext(_enumerator);current.Ok; current = moveNext(_enumerator))
            {
                eof = current.Eof;
                token = TryGetPunctuator(current.Char);
                if (token != null) break;
                
                token = TryGetLetter(current.Char);
                if (token.HasValue ) break;                
            }
            
            if (eof) return new Token(TokenType.EOF, "");

            return token ;                                
        }

        public string InputText
        {
            get
            {
                return _text.Select(a=> a.ToString(CultureInfo.InvariantCulture)).Aggregate((a, b) => a + b);
            }
        }
        private readonly IEnumerable<char> _text;
        private int mIndex;
        private readonly IEnumerator<char> _enumerator;
    }
}