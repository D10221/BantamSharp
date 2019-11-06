namespace SimpleParser
{
    public static class TokenExtensions
    {
        public static void Expect<TTokenType>(this IToken<TTokenType> actual, IToken<TTokenType> expected)
        {
            if (actual == null || !Equals(actual, expected))
            {
                throw new ParseException($"Expected token {expected} and found {actual}");
            }
        }
        public static void Expect<TTokenType>(this IToken<TTokenType> actual, TTokenType expected)
        {
            if (actual == null || !Equals(actual.TokenType, expected))
            {
                throw new ParseException($"Expected token {expected} and found {(actual == null ? default(TTokenType): actual.TokenType)}");
            }
        }
        public static void ExpectValue<TTokenType>(this IToken<TTokenType> actual, object expected)
        {
            if (actual == null || !Equals(actual.Value, expected))
            {
                throw new ParseException($"Expected token {expected} and found {actual?.Value}");
            }
        }
    }
}