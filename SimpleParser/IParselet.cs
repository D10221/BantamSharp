namespace SimpleParser
{
    public interface IParselet
    {
        ISimpleExpression Parse(IParser parser, IToken token);
    }
}