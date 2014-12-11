namespace SimpleParser
{
    public interface IPrefixParselet
    {
        ISimpleExpression Parse(IParser parser, IToken token);
    }
}