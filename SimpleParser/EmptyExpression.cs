namespace SimpleParser
{
    public class EmptyExpression<T> : ISimpleExpression<T>
    {
        public IToken<T> Token {get;} = SimpleParser.Token.Empty<T>();
        

        public void Print(IBuilder builder)
        {
            // return;
        }
    }
}