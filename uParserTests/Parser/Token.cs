namespace uParserTests
{
    public class Token
    {
        public Token(TokenType tokenType = default, object value = null){
            TokenType = tokenType;
            Value = value;
        }
        public TokenType TokenType { get;  }
        public object Value { get; }

        public override string ToString() {
            return Value?.ToString();
        }
    }
}