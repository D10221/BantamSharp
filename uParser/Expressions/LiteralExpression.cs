namespace uParser
{
    public class LiteralExpression : ISimpleExpression
    {
        public Token Token { get; }

        public LiteralExpression(Token token)
        {
            Token = token;
        }

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}