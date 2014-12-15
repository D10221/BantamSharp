using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SimpleParser;

namespace BantamTests.Support
{
    public class FakeLexer : ILexer
    {
        private readonly string _input;
        
        private char[] _chars;
        
        private int _index;

        private IDictionary<TokenType,char> _punctuators = new Dictionary<TokenType, char>
        {
            {TokenType.LEFT_PAREN,'('},
            {TokenType.RIGHT_PAREN,')'},
            {TokenType.COMMA,','},
            {TokenType.ASSIGN,'='},
            {TokenType.PLUS,'+'},
            {TokenType.MINUS,'-'},
            {TokenType.ASTERISK,'*'},
            {TokenType.SLASH,'/'},
            {TokenType.CARET,'^'},
            {TokenType.TILDE,'~'},
            {TokenType.BANG,'!'},
            {TokenType.QUESTION,'?'},
            {TokenType.COLON,':'},
        };

        private IEnumerable<Token> _tokens;

        private IEnumerator<Token> _enumerator;

        public FakeLexer(string input)
        {
            _input = input;
            _index = 0;
            _chars = _input.ToCharArray();
            _tokens = _chars.Select(c => new Token(GetTokenType(c), AsText(c)));
            _enumerator = _tokens.GetEnumerator();
        }

        private static string AsText(char c)
        {
            return c.ToString(CultureInfo.InvariantCulture);
        }

        private TokenType GetTokenType(char c)
        {
            if (Char.IsWhiteSpace(c)) return TokenType.NONE;
            //FIXME:
            if (char.IsLetterOrDigit(c)) return TokenType.NAME;
            
            var found = _punctuators.FirstOrDefault(p => p.Value == c);
            if (found.Value != default(char)) return found.Key;

            throw new LexerException(String.Format("Don't know what to do with {0}", AsText(c)));
        }

       
        public IToken Next()
        {
            if(_enumerator.MoveNext())
            return _enumerator.Current;
            return Token.Empty();
        }

        public string InputText
        {
            get { return _input; }
        }
    }
}
