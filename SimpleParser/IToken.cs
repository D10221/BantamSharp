namespace SimpleParser
{
    public interface IToken
    {
        TokenType TokenType { get; }
        string GetText();
        string ToString();
        bool HasValue { get; }
        bool IsEmpty { get; }
    }
}