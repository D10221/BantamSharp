namespace SimpleParser
{
    public class EmptyExpression<T> : ISimpleExpression<T>
    {
        public IToken<T> Token { get; } = SimpleParser.Token.Empty<T>();

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}