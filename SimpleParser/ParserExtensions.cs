namespace SimpleParser
{
    public static class ParserExtensions {
         // Not Used
        public static bool IsMatch<TTokenType>(this IParser<TTokenType> parser, TTokenType expected)
        {
            var token = parser.LookAhead();
            var result = Equals(token.TokenType, expected);
            parser.Consume();
            return result;;
        }
        // Not used
        public static IToken<TTokenType> ConsumeNextTokenType<TTokenType>(this IParser<TTokenType> parser, TTokenType expected)
        {
            var token = parser.LookAhead();
            if (!Equals(token.TokenType, expected))
            {
                throw new ParseException($"Expected token {expected} and found {token.TokenType}");
            }
            return parser.Consume();
        }
    }
}