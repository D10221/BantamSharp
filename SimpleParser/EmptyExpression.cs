namespace SimpleParser
{
    public class EmptyExpression<T> : ISimpleExpression
    {
        public object Token {get;} = SimpleParser.Token.Empty<T>();

        public void Print(IBuilder builder)
        {
            // return;
        }
    }
}