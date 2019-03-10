namespace SimpleParser
{
    public interface ITokenFactory<TokenType>
    {
        IToken<TokenType> GetToken(string x);
    }
}