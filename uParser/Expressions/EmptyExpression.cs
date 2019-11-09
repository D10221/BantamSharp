namespace uParser
{
    public class EmptyExpression : ISimpleExpression
    {
        public static EmptyExpression Default = new EmptyExpression();
        public Token Token { get; } = new Token(default, null);

        public override string ToString()
        {
            return $"{Token}";
        }
    }
}