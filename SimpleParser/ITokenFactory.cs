namespace SimpleParser
{
    public interface ITokenFactory<TokenType>
    {
        IToken<TokenType> GetPunctuator(string x);
        IToken<TokenType> GetName(string x);
    }
}