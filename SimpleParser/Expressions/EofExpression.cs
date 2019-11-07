namespace SimpleParser
{
    public class EofExpression<TTokenType> : ISimpleExpression<TTokenType>
    {
        public IToken<TTokenType> Token { get; }

        public EofExpression(IToken<TTokenType> token)
        {
            Token = token;
        }
        public override string ToString()
        {
            return $"{Token}";
        }

    }
}