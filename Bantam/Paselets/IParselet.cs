using SimpleParser;

namespace Bantam.Paselets
{
    //TODO
    public interface IParselet<TTokenType, TokenBase>
    {
        ISimpleExpression Parse(IParser<TokenBase> parser, IToken<TTokenType> token);
    }
}