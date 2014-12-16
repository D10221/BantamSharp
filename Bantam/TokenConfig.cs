using System;
using System.Collections.Generic;
using System.Linq;
using ITokenConfig = SimpleParser.ITokenConfig<Bantam.TokenType, char>;

namespace Bantam
{
    public class TokenConfig : ITokenConfig
    {
        public TokenConfig()
        {
            TokenTypes = new[]
            {
                TokenType.LEFT_PAREN, TokenType.RIGHT_PAREN, TokenType.COMMA, TokenType.ASSIGN, TokenType.PLUS,
                TokenType.MINUS, TokenType.ASTERISK, TokenType.SLASH, TokenType.CARET, TokenType.TILDE,
                TokenType.BANG, TokenType.QUESTION, TokenType.COLON, TokenType.NAME, TokenType.EOF
            }.Select(
                t => new Tuple<TokenType, char>(t, Punctuator(t)));
        }

        /// <summary>
        /// If the TokenType represents a punctuator (i.e. a token that can split an identifier like '+', this will get its text.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        public  char Punctuator( TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.LEFT_PAREN: return '(';
                case TokenType.RIGHT_PAREN: return ')';
                case TokenType.COMMA: return ',';
                case TokenType.ASSIGN: return '=';
                case TokenType.PLUS: return '+';
                case TokenType.MINUS: return '-';
                case TokenType.ASTERISK: return '*';
                case TokenType.SLASH: return '/';
                case TokenType.CARET: return '^';
                case TokenType.TILDE: return '~';
                case TokenType.BANG: return '!';
                case TokenType.QUESTION: return '?';
                case TokenType.COLON: return ':';
                default: return default(char);
            }
        }

        public  bool IsValidPunctuator( char c)
        {
            var reverse = Punctuators.ToDictionary(p => p.Value, p => p.Key);
            TokenType pp;
            var ok = reverse.TryGetValue(c, out pp);
            return ok;
        }

        public  readonly IDictionary<TokenType, char> Punctuators = new Dictionary<TokenType, char>
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

        public IEnumerable<Tuple<TokenType, char>> TokenTypes { get; private set; }
    }
}