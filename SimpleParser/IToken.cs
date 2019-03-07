namespace SimpleParser
{
    public interface IToken<TTokenType>
    {
        TTokenType TokenType { get; }
        object Value { get; }
        bool IsEmpty { get; }
    }
}