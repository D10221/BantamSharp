
namespace SimpleParser
{
    public interface IParselet<TTokenType, TTokenBase>
    {
        ISimpleExpression<TTokenBase> Parse(IParser<TTokenType, TTokenBase> parser, IToken<TTokenType> token);
    }
}