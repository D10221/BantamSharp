namespace Bantam
{
    public interface IToken
    {
        TokenType GetTokenType();
        string GetText();
        string ToString();
        bool HasValue { get; }
        bool IsEmpty { get; }
    }
}