namespace SimpleParser
{
    public enum ParseletType
    {
        None,
        Prefix,
        Infix
    }
    public interface IParselet<TTokenType>
    {
        int Precedence { get; }
        ParseletType ParseletType { get; }
        TTokenType TokenType { get; }
        ISimpleExpression Parse(IParser<TTokenType> parser, IToken<TTokenType> token, ISimpleExpression left);
    }
}