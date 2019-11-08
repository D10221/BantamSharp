using System.Collections.Generic;

namespace uParserTests
{
    public static class Punctuators
    {
        public static IDictionary<TokenType, string> Values = new Dictionary<TokenType, string>
        {
            {TokenType.PARENT_LEFT , "("},
            {TokenType.PARENT_RIGHT , ")"},
            {TokenType.COMMA , ","},
            {TokenType.ASSIGN , "="},
            {TokenType.PLUS , "+"},
            {TokenType.MINUS , "-"},
            {TokenType.ASTERISK , "*"},
            {TokenType.SLASH , "/"},
            {TokenType.CARET , "^"},
            {TokenType.TILDE , "~"},
            {TokenType.BANG , "!"},
            {TokenType.QUESTION , "?"},
            {TokenType.COLON , ":"}
        };
        public static IDictionary<string, TokenType> Reverse => Values.Reverse();
    }
}