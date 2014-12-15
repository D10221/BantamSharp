namespace SimpleParser
{
    public interface IPrefixParselet<TTokenType>
    {
        ISimpleExpression Parse(IParser<TTokenType> parser, IToken<TTokenType> token);
    }
}