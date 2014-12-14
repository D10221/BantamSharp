namespace SimpleParser
{
    public interface IParser
    {       
        ISimpleExpression ParseExpression();
        bool IsMatch(TokenType expected);
        IToken Consume(TokenType expected);
        IToken Consume();
    }
}