namespace SimpleParser
{
    public static class LexerExtensions {
         // Not Used
        public static bool IsMatch<TTokenType>(this ILexer<IToken<TTokenType>> lexer, TTokenType expected)
        {
            var token = lexer.LookAhead();
            var result = Equals(token.TokenType, expected);
            lexer.Consume();
            return result;;
        }
        // Not used
        public static IToken<TTokenType> ConsumeNextTokenType<TTokenType>(this ILexer<IToken<TTokenType>> lexer, TTokenType expected)
        {
            var token = lexer.LookAhead();
            if (!Equals(token.TokenType, expected))
            {
                throw new ParseException($"Expected token {expected} and found {token.TokenType}");
            }
            return lexer.Consume();
        }
    }
}