namespace SimpleParser
{
    public interface IParser<TTokenType>
    {       
        ISimpleExpression ParseExpression(Precedence precedence=Precedence.ZERO);
        bool IsMatch(TTokenType expected);
        IToken<TTokenType> Consume(TTokenType expected);
        IToken<TTokenType> Consume();
    }
}