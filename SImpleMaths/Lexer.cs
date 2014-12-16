using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleParser;
using IParser = SimpleParser.IParser<SimpleMaths.TokenType,string>;
using IPrefixParselet = SimpleParser.Parselets.IPrefixParselet<SimpleMaths.TokenType,string>;
using IToken = SimpleParser.IToken<SimpleMaths.TokenType>;
using InfixParselet = SimpleParser.Parselets.InfixParselet<SimpleMaths.TokenType,string>;
using ILexer = SimpleParser.ILexer<SimpleMaths.TokenType,string>;

namespace SimpleMaths
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
        public Lexer(String text, TokenConfig tokenConfig)
        {
            TokenConfig = tokenConfig;
            //SPlit into possible tokens using tokenConfig
            InputText = Regex.Split(text, @"\s+");
           
            _enumerator = InputText.ToArray().Cast<string>().GetEnumerator();
        }

        public TokenConfig TokenConfig { get; set; }

        private IToken TryGetPunctuator(string c)
        {
            var token = TokenConfig.TokenTypes
                .GetTokenType(c)
                .Extract(tt => new Token<TokenType>(tt.TknType(), AsString(tt)));
            return token;
        }

        private static string AsString(Tuple<TokenType, string> tt)
        {
            return tt.Value();
        }
        //TODO: rename it
        private IToken TryGetLetter(string c)
        {            
            var input = c.ToString(CultureInfo.InvariantCulture);
            return LooksLikeLetter(input) ? new Token<TokenType>( TokenType.NAME,input) : Token<TokenType>.Empty();
        }

        private static bool LooksLikeLetter(string input)
        {
            bool isMatch = new Regex(@"\d+").IsMatch(input);//|| new Regex(@"\w").IsMatch(input);
            return isMatch;
        }

        private class Iteration
        {
            public Iteration(bool ok, bool eof, string c)
            {
                Ok = ok;Eof = eof;Char = c;
            }

            public string Char { get; private set; }

            public bool Eof { get; private set; }

            public bool Ok { get; private set; }

            public override string ToString()
            {
                return Char.ToString(CultureInfo.InvariantCulture);
            }
        }

        readonly Func<IEnumerator<string>, Iteration> moveNext = enumerator =>
        {
            var ok = enumerator.MoveNext();
            var eof = !ok;
            return new Iteration(ok, eof,ok ? enumerator.Current : default(string));
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

        public IEnumerable<string> InputText { get; private set; }

        private readonly IEnumerator<string> _enumerator;
    }
}