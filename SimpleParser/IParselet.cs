
namespace SimpleParser
{
    public interface IParselet<TTokenType>
    {
        int Precedence { get; }
        ParseletType ParseletType { get; }
        TTokenType TokenType { get; }
        ISimpleExpression Parse(IParser<TTokenType> parser, IToken<TTokenType> token, ISimpleExpression left);
    }
}