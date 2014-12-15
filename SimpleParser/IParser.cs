namespace SimpleParser
{
    public interface IParser
    {       
        ISimpleExpression ParseExpression(Precedence precedence=Precedence.ZERO);
        bool IsMatch(TokenType expected);
        IToken Consume(TokenType expected);
        IToken Consume();
    }
}