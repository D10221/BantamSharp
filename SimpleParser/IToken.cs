namespace SimpleParser
{
    //TOkenType is the current enum 
    public interface IToken<TTokenType>
    {
        TTokenType TokenType { get; }
        string GetText();
        string ToString();
        bool HasValue { get; }
        bool IsEmpty { get; }
    }
}