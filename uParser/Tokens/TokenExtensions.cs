namespace uParser
{
    public static class TokenExtensions
    {
        public static void Expect<TTokenType>(this Token actual, Token expected)
        {
            if (actual == null || !Equals(actual, expected))
            {
                throw new ParseException($"Expected token {expected} and found {actual}");
            }
        }
        public static void Expect(this Token actual, TokenType expected)
        {
            if (actual == null || !Equals(actual.TokenType, expected))
            {
                throw new ParseException($"Expected token {expected} and found {(actual == null ? default : actual.TokenType)}");
            }
        }
        public static void ExpectValue<TTokenType>(this Token actual, object expected)
        {
            if (actual == null || !Equals(actual.Value, expected))
            {
                throw new ParseException($"Expected token {expected} and found {actual?.Value}");
            }
        }
    }
}