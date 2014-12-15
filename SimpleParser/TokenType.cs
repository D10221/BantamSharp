using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser
{
    public enum TokenType
    {      
        NONE,
        LEFT_PAREN,
        RIGHT_PAREN,
        COMMA,
        ASSIGN,
        PLUS,
        MINUS,
        ASTERISK,
        SLASH,
        CARET,
        TILDE,
        BANG,
        QUESTION,
        COLON,
        NAME,
        EOF,             
    }
    //
    public class TokenTypes
    {
        //TokenTypes that are explicit punctuators.
        public  readonly IEnumerable<Tuple<TokenType, char>> Punctuators;

        public static readonly TokenType[] Values =
        {
            TokenType.LEFT_PAREN,
            TokenType.RIGHT_PAREN,
            TokenType.COMMA,
            TokenType.ASSIGN,
            TokenType.PLUS,
            TokenType.MINUS,
            TokenType.ASTERISK,
            TokenType.SLASH,
            TokenType.CARET,
            TokenType.TILDE,
            TokenType.BANG,
            TokenType.QUESTION,
            TokenType.COLON,
            TokenType.NAME,
            TokenType.EOF
        };

        TokenTypes()
        {
            Punctuators = Values.Select(t => new Tuple<TokenType, Char> (t, TokenConfig.Punctuator(t)));
        }

        public TokenTypes(ITokenConfig<char> tokenConfig)
        {
            TokenConfig = tokenConfig;
        }

        private ITokenConfig<char> TokenConfig { get; set; }
    }
}